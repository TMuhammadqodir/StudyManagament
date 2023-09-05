using AutoMapper;
using StudyManagement.Data.IRepositories;
using StudyManagement.Domain.Configrations;
using StudyManagement.Domain.Entities.Grades;
using StudyManagement.Domain.Entities.Sciences;
using StudyManagement.Domain.Entities.StudentSciences;
using StudyManagement.Domain.Entities.Teachers;
using StudyManagement.Service.DTOs.Sciences;
using StudyManagement.Service.Exceptions;
using StudyManagement.Service.Extentions;
using StudyManagement.Service.Interfaces;
using StudyManagement.Service.Mappers;

namespace StudyManagement.Service.Services;

public class ScienceService: IScienceService
{
    private readonly IRepository<ScienceEntity> _scienceRepository;
    private readonly IRepository<TeacherEntity> _teacherRepository;
    private readonly IRepository<GradeEntity> _gradeRepository;
    private readonly IRepository<StudentScienceEntity> _studentScienceRepository;
    private readonly IMapper _mapper;

    public ScienceService(IRepository<ScienceEntity> scienceRepository, 
                          IRepository<TeacherEntity> teacherRepository,
                          IRepository<GradeEntity> gradeRepository,
                          IRepository<StudentScienceEntity> studentScienceRepository)
    {
        _scienceRepository = scienceRepository;
        _teacherRepository = teacherRepository;
        _gradeRepository = gradeRepository;
        _studentScienceRepository = studentScienceRepository;

        _mapper = new Mapper(new MapperConfiguration(
            cfg => cfg.AddProfile<MappingProfile>()));
    }

    public async Task<ScienceResultDTO> CreateAsync(ScienceCreationDTO dto)
    {
        var existTeacher = await _teacherRepository.GetAsync(t=> t.Id.Equals(dto.TeacherId))
            ?? throw new NotFoundException($"This teacher was not found with {dto.TeacherId}");

        var ScienceEntity = _mapper.Map<ScienceEntity>(dto);

        await _scienceRepository.CreateAsync(ScienceEntity);
        await _scienceRepository.SaveAsync();

        return _mapper.Map<ScienceResultDTO>(ScienceEntity);
    }

    public async Task<ScienceResultDTO> UpdateAsync(ScienceUpdateDTO dto)
    {
        var existScience = await _scienceRepository.GetAsync(g => g.Id.Equals(dto.Id))
           ?? throw new NotFoundException($"This science was not found with {dto.Id}");

        var existTeacher = await _teacherRepository.GetAsync(t => t.Id.Equals(dto.TeacherId))
            ?? throw new NotFoundException($"This teacher was not found with {dto.TeacherId}");
     
        _mapper.Map(dto, existScience);

        _scienceRepository.Update(existScience);
        await _scienceRepository.SaveAsync();

        return _mapper.Map<ScienceResultDTO>(existScience);
    }


    public async Task<bool> DeleteAsync(long id)
    {
        var existScience = await _scienceRepository.GetAsync(g => g.Id.Equals(id))
           ?? throw new NotFoundException($"This science was not found with {id}");

        _scienceRepository.Delete(existScience);
        await _scienceRepository.SaveAsync();

        return true;
    }

    public async Task<ScienceResultDTO> GetByIdAsync(long id)
    {
        var existScience = await _scienceRepository.GetAsync(g => g.Id.Equals(id), new string[] { "Teacher", "StudentSciences" })
           ?? throw new NotFoundException($"This science was not found with {id}");

        return _mapper.Map<ScienceResultDTO>(existScience);
    }

    public async Task<IEnumerable<ScienceResultDTO>> GetAllAsync(PaginationParams @params)
    {
        var sciences = _scienceRepository.GetAll(null, true, new string[] {"Teacher", "StudentSciences" }).ToPaginate(@params);

        return _mapper.Map<IEnumerable<ScienceResultDTO>>(sciences);
    }

    public async Task<ScienceResultDTO> GetByScienceOfHighgGrade(long id)
    {
        var grade = _gradeRepository.GetAll(null, true, new string[] { "StudentScience" }).ToList()
            .Where(g => g.StudentScience.StudentId.Equals(id)).MaxBy(g => g.Grade);

        if (grade is null)
            throw new NotFoundException($"This Science was not found");

        var science = await _scienceRepository.GetAsync(s => s.Id.Equals(grade.StudentScience.ScienceId));

        return _mapper.Map<ScienceResultDTO>(science);
    }

    public async Task<ScienceResultDTO> GetByHighestGradePointAverage()
    {
        var studentScience = _studentScienceRepository.GetAll(null, true, new string[] { "Student", "Science", "Grade" }).ToList();

        float maxGradePoindAverage = 0f;

        ScienceEntity result = new ScienceEntity();

        for(int i=0; i<studentScience.Count; i++)
        {
            float grades = 0f;
            int count = 0;

            for(int j=0; j< studentScience.Count; j++)
            {
                if (studentScience[i].ScienceId == studentScience[j].ScienceId)
                {
                    if (studentScience[j].Grade is not null)
                    {
                        grades += studentScience[j].Grade.Grade;
                        count++;
                    }
                }
            }

            if (maxGradePoindAverage < grades / count)
            {
                maxGradePoindAverage = grades / count;

                result = studentScience[i].Science;
            }
        }

        return _mapper.Map<ScienceResultDTO>(result);
    }
}
