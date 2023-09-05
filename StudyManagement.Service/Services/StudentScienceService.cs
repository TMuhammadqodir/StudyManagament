using AutoMapper;
using StudyManagement.Data.IRepositories;
using StudyManagement.Domain.Configrations;
using StudyManagement.Domain.Entities.Sciences;
using StudyManagement.Domain.Entities.Students;
using StudyManagement.Domain.Entities.StudentSciences;
using StudyManagement.Service.DTOs.StudentSciences;
using StudyManagement.Service.Exceptions;
using StudyManagement.Service.Extentions;
using StudyManagement.Service.Mappers;

namespace StudyManagement.Service.Services;

public class StudentScienceService : IStudentScienceService
{
    private readonly IRepository<StudentScienceEntity> _studentScienceRepository;
    private readonly IRepository<StudentEntity> _studentRepository;
    private readonly IRepository<ScienceEntity> _scienceRepository;
    private readonly IMapper _mapper;

    public StudentScienceService(IRepository<StudentEntity> studentRepository,
                                 IRepository<ScienceEntity> scienceRepository,
                                 IRepository<StudentScienceEntity> studentScienceRepository)
    {
        _studentRepository = studentRepository;
        _scienceRepository = scienceRepository;
        _studentScienceRepository = studentScienceRepository;

        _mapper = new Mapper(new MapperConfiguration(
            cfg => cfg.AddProfile<MappingProfile>()));
    }

    public async Task<StudentScienceResultDTO> CreateAsync(StudentScienceCreationDTO dto)
    {
        var existStudent = await _studentRepository.GetAsync(s => s.Id.Equals(dto.StudentId))
            ?? throw new NotFoundException($"This student was not foun with {dto.StudentId}");

        var existScience = await _scienceRepository.GetAsync(s => s.Id.Equals(dto.ScienceId))
            ?? throw new NotFoundException($"This science was not foun with {dto.ScienceId}");

        var studentScienceEntity = _mapper.Map<StudentScienceEntity>(dto);

        await _studentScienceRepository.CreateAsync(studentScienceEntity);
        await _studentScienceRepository.SaveAsync();

        return _mapper.Map<StudentScienceResultDTO>(studentScienceEntity);
    }

    public async Task<StudentScienceResultDTO> UpdateAsync(StudentScienceUpdateDTO dto)
    {
        var existStudentScience = await _studentScienceRepository.GetAsync(g => g.Id.Equals(dto.Id))
           ?? throw new NotFoundException($"This studentScience was not found with {dto.Id}");

        var existStudent = await _studentRepository.GetAsync(s => s.Id.Equals(dto.StudentId))
            ?? throw new NotFoundException($"This student was not foun with {dto.StudentId}");

        var existScience = await _scienceRepository.GetAsync(s => s.Id.Equals(dto.ScienceId))
            ?? throw new NotFoundException($"This science was not foun with {dto.ScienceId}");

        _mapper.Map(dto, existStudentScience);

        _studentScienceRepository.Update(existStudentScience);
        await _studentScienceRepository.SaveAsync();

        return _mapper.Map<StudentScienceResultDTO>(existStudentScience);
    }


    public async Task<bool> DeleteAsync(long id)
    {
        var existStudentScience = await _studentScienceRepository.GetAsync(g => g.Id.Equals(id))
           ?? throw new NotFoundException($"This studentScience was not found with {id}");

        _studentScienceRepository.Delete(existStudentScience);
        await _studentScienceRepository.SaveAsync();

        return true;
    }

    public async Task<StudentScienceResultDTO> GetByIdAsync(long id)
    {
        var existStudentScience = await _studentScienceRepository.GetAsync(g => g.Id.Equals(id), new string[] { "Student", "Science", "Grade" })
           ?? throw new NotFoundException($"This studentScience was not found with {id}");

        return _mapper.Map<StudentScienceResultDTO>(existStudentScience);
    }

    public async Task<IEnumerable<StudentScienceResultDTO>> GetAllAsync(PaginationParams @params)
    {
        var studentSciences = _studentScienceRepository.GetAll(null, true, new string[] { "Student", "Science", "Grade" }).ToPaginate(@params);

        return _mapper.Map<IEnumerable<StudentScienceResultDTO>>(studentSciences);
    }
}
