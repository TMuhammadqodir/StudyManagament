using AutoMapper;
using StudyManagement.Data.IRepositories;
using StudyManagement.Domain.Configrations;
using StudyManagement.Domain.Entities.StudentSciences;
using StudyManagement.Domain.Entities.Teachers;
using StudyManagement.Service.DTOs.Teachers;
using StudyManagement.Service.Exceptions;
using StudyManagement.Service.Extentions;
using StudyManagement.Service.Helpers;
using StudyManagement.Service.Interfaces;
using StudyManagement.Service.Mappers;

namespace StudyManagement.Service.Services;

public class TeacherService : ITeacherService
{
    private readonly IRepository<TeacherEntity> _teacherRepository;
    private readonly IRepository<StudentScienceEntity> _studentScienceRepository;
    private readonly IMapper _mapper;

    public TeacherService(IRepository<TeacherEntity> teacherRepository, IRepository<StudentScienceEntity> studentScienceRepository)
    {
        _teacherRepository = teacherRepository;
        _studentScienceRepository = studentScienceRepository;

        _mapper = new Mapper(new MapperConfiguration(
            cfg => cfg.AddProfile<MappingProfile>()));
    }

    public async Task<TeacherResultDTO> CreateAsync(TeacherCreationDTO dto)
    {
        var validTelNumber = TelNumberChecker.CheckNumber(dto.TelNumber);
 
        if (!validTelNumber)
            throw new CustomException($"Invalid tel number {dto.TelNumber}");

        var existTeacher = await _teacherRepository.GetAsync(s => s.Email.ToLower().Equals(dto.Email.ToLower()));
            
        if(existTeacher is not null)
            throw new AlreadyExistException($"This teacher already exist with {dto.Email}");

        var teacherEntity = _mapper.Map<TeacherEntity>(dto);
        await _teacherRepository.CreateAsync(teacherEntity);
        await _teacherRepository.SaveAsync();

        return _mapper.Map<TeacherResultDTO>(teacherEntity);
    }

    public async Task<TeacherResultDTO> UpdateAsync(TeacherUpdateDTO dto)
    {
        var existTeacher1 = await _teacherRepository.GetAsync(g => g.Id.Equals(dto.Id))
           ?? throw new NotFoundException($"This teacher was not found with {dto.Id}");
       
        if (!existTeacher1.TelNumber.Equals(dto.TelNumber))
        {
            var validTelNumber = TelNumberChecker.CheckNumber(dto.TelNumber);

            if (!validTelNumber)
                throw new CustomException($"Invalid tel number {dto.TelNumber}");

            var existTeacher = await _teacherRepository.GetAsync(s => s.Email.ToLower().Equals(dto.Email.ToLower()));

            if (existTeacher is not null)
                throw new AlreadyExistException($"This teacher already exist with {dto.Email}");

        }

        _mapper.Map(dto, existTeacher1);

        _teacherRepository.Update(existTeacher1);
        await _teacherRepository.SaveAsync();

        return _mapper.Map<TeacherResultDTO>(existTeacher1);
    }


    public async Task<bool> DeleteAsync(long id)
    {
         var existTeacher = await _teacherRepository.GetAsync(g => g.Id.Equals(id))
           ?? throw new NotFoundException($"This teacher was not found with {id}");

        _teacherRepository.Delete(existTeacher);
        await _teacherRepository.SaveAsync();

        return true;
    }

    public async Task<TeacherResultDTO> GetByIdAsync(long id)
    {
        var existTeacher = await _teacherRepository.GetAsync(g => g.Id.Equals(id), new string[] { "Sciences" })
           ?? throw new NotFoundException($"This teacher was not found with {id}");

        return _mapper.Map<TeacherResultDTO>(existTeacher);
    }

    public async Task<IEnumerable<TeacherResultDTO>> GetAllAsync(PaginationParams @params)
    {
        var teachers = _teacherRepository.GetAll(null, true, new string[] { "Sciences" }).ToPaginate(@params);

        return _mapper.Map<IEnumerable<TeacherResultDTO>>(teachers);
    }

    public async Task<IEnumerable<TeacherResultDTO>> GetByToAgeFromAge(int toAge, int fromAge)
    {
        var teachers = _teacherRepository.GetAll(s => (DateTime.UtcNow.Year - s.DateOfBirth.Year) >= toAge && (DateTime.UtcNow.Year - s.DateOfBirth.Year) <= fromAge, true, new string[] { "Sciences" });

        return _mapper.Map<IEnumerable<TeacherResultDTO>>(teachers);
    }

    public async Task<IEnumerable<TeacherResultDTO>> GetByBeelineTelNumber()
    {
        var teachers = _teacherRepository.GetAll(null, true, new string[] { "Sciences" });

        var result = _mapper.Map<IEnumerable<TeacherResultDTO>>(teachers)
            .Where(t => t.TelNumber[4] == '9' && (t.TelNumber[5] == '1' || t.TelNumber[5] == '0'));

        return result;
    }

    public async Task<IEnumerable<TeacherResultDTO>> GetByTeachersOfStudentsWithHighestGrade(int maxGrade)
    {
        var sciences = _studentScienceRepository.GetAll(null, true, new string[] { "Student", "Science", "Grade" }).ToList()
            .Where(s=>s.Grade is null? false: s.Grade.Grade>maxGrade).Select(s=> s.Science);

        var result = new List<TeacherEntity>();

        HashSet<long> teachers = new HashSet<long>();

        foreach(var science in sciences)
        {
            if (!teachers.Contains(science.TeacherId))
            {
                var teacher = await _teacherRepository.GetAsync(t => t.Id.Equals(science.TeacherId));
                result.Add(teacher);
                teachers.Add(science.TeacherId);
            }
        }

        return _mapper.Map<IEnumerable<TeacherResultDTO>>(result);
    }
}
