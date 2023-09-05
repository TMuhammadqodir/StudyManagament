using AutoMapper;
using StudyManagement.Data.IRepositories;
using StudyManagement.Domain.Configrations;
using StudyManagement.Domain.Entities.Grades;
using StudyManagement.Domain.Entities.StudentSciences;
using StudyManagement.Service.DTOs.Grades;
using StudyManagement.Service.Exceptions;
using StudyManagement.Service.Extentions;
using StudyManagement.Service.Interfaces;
using StudyManagement.Service.Mappers;

namespace StudyManagement.Service.Services;

public class GradeService : IGradeService
{
    private readonly IRepository<GradeEntity> _gradeRepository;
    private readonly IRepository<StudentScienceEntity> _studentScienceRepository;
    private readonly IMapper _mapper;

    public GradeService(IRepository<GradeEntity> gradeRepository, IRepository<StudentScienceEntity> studentScienceRepository)
    {
        _gradeRepository = gradeRepository;
        _studentScienceRepository = studentScienceRepository;

        _mapper = new Mapper(new MapperConfiguration(
            cfg=> cfg.AddProfile<MappingProfile>()));
    }

    public async Task<GradeResultDTO> CreateAsync(GradeCreationDTO dto)
    {
        var existStudentScience = await _studentScienceRepository.GetAsync(t => t.Id.Equals(dto.StudentScienceId))
            ?? throw new NotFoundException($"This studentScience was not found with {dto.StudentScienceId}");

        var gradeEntity = _mapper.Map<GradeEntity>(dto);

        await _gradeRepository.CreateAsync(gradeEntity);
        await _gradeRepository.SaveAsync();

        return _mapper.Map<GradeResultDTO>(gradeEntity);
    }

    public async Task<GradeResultDTO> UpdateAsync(GradeUpdateDTO dto)
    {
        var existStudentScience = await _studentScienceRepository.GetAsync(t => t.Id.Equals(dto.StudentScienceId))
            ?? throw new NotFoundException($"This studentScience was not found with {dto.StudentScienceId}");

        var existGrade = await _gradeRepository.GetAsync(g=> g.Id.Equals(dto.Id))
           ?? throw new NotFoundException($"This grade was not found with {dto.Id}");

        _mapper.Map(dto, existGrade);

        _gradeRepository.Update(existGrade);
        await _gradeRepository.SaveAsync();

        return _mapper.Map<GradeResultDTO>(existGrade);
    }


    public async Task<bool> DeleteAsync(long id)
    {
        var existGrade = await _gradeRepository.GetAsync(g => g.Id.Equals(id))
           ?? throw new NotFoundException($"This grade was not found with {id}");

        _gradeRepository.Delete(existGrade);
        await _gradeRepository.SaveAsync();

        return true;
    }

    public async Task<GradeResultDTO> GetByIdAsync(long id)
    {
        var existGrade = await _gradeRepository.GetAsync(g => g.Id.Equals(id), new string[] { "StudentScience" })
           ?? throw new NotFoundException($"This grade was not found with {id}");

        return _mapper.Map<GradeResultDTO>(existGrade);
    }

    public async Task<IEnumerable<GradeResultDTO>> GetAllAsync(PaginationParams @params)
    {
        var grades = _gradeRepository.GetAll(null, true, new string[] { "StudentScience" }).ToPaginate(@params);

        return _mapper.Map<IEnumerable<GradeResultDTO>>(grades);
    }
}
