using AutoMapper;
using StudyManagement.Data.IRepositories;
using StudyManagement.Domain.Configrations;
using StudyManagement.Domain.Entities.Students;
using StudyManagement.Service.DTOs.Students;
using StudyManagement.Service.Exceptions;
using StudyManagement.Service.Extentions;
using StudyManagement.Service.Helpers;
using StudyManagement.Service.Interfaces;
using StudyManagement.Service.Mappers;

namespace StudyManagement.Service.Services;

public class StudentService : IStudentService
{
    private readonly IRepository<StudentEntity> _studentRepository;
    private readonly IMapper _mapper;

    public StudentService(IRepository<StudentEntity> studentRepository)
    {
        _studentRepository = studentRepository;

        _mapper = new Mapper(new MapperConfiguration(
            cfg => cfg.AddProfile<MappingProfile>()));
    }

    public async Task<StudentResultDTO> CreateAsync(StudentCreationDTO dto)
    {
        var validTelNumber = TelNumberChecker.CheckNumber(dto.TelNumber);

        if (!validTelNumber)
            throw new CustomException($"Invalid tel number {dto.TelNumber}");

        var existStudent = await _studentRepository.GetAsync(s => s.Email.ToLower().Equals(dto.Email.ToLower()));
            
        if(existStudent is not null)
            throw new AlreadyExistException($"This student already exist with {dto.Email}");

        var studentEntity = _mapper.Map<StudentEntity>(dto);
        await _studentRepository.CreateAsync(studentEntity);
        await _studentRepository.SaveAsync();

        return _mapper.Map<StudentResultDTO>(studentEntity);
    }

    public async Task<StudentResultDTO> UpdateAsync(StudentUpdateDTO dto)
    {
        var existStudent1 = await _studentRepository.GetAsync(g => g.Id.Equals(dto.Id))
           ?? throw new NotFoundException($"This student was not found with {dto.Id}");

        if (!existStudent1.TelNumber.Equals(dto.TelNumber))
        {
            var validTelNumber = TelNumberChecker.CheckNumber(dto.TelNumber);

            if (!validTelNumber)
                throw new CustomException($"Invalid tel number {dto.TelNumber}");

            var existStudent = await _studentRepository.GetAsync(s => s.Email.ToLower().Equals(dto.Email.ToLower()));

            if (existStudent is not null)
                throw new AlreadyExistException($"This student already exist with {dto.Email}");

        }

        _mapper.Map(dto, existStudent1);

        _studentRepository.Update(existStudent1);
        await _studentRepository.SaveAsync();

        return _mapper.Map<StudentResultDTO>(existStudent1);
    }


    public async Task<bool> DeleteAsync(long id)
    {
        var existStudent = await _studentRepository.GetAsync(g => g.Id.Equals(id))
            ?? throw new NotFoundException($"This student was not found with {id}");

        _studentRepository.Delete(existStudent);
        await _studentRepository.SaveAsync();

        return true;
    }

    public async Task<StudentResultDTO> GetByIdAsync(long id)
    {
        var existStudent = await _studentRepository.GetAsync(g => g.Id.Equals(id), new string[] { "StudentSciences" })
            ?? throw new NotFoundException($"This student was not found with {id}");

        return _mapper.Map<StudentResultDTO>(existStudent);
    }

    public async Task<IEnumerable<StudentResultDTO>> GetAllAsync(PaginationParams @params)
    {
        var students = _studentRepository.GetAll(null, true, new string[] { "StudentSciences" }).ToPaginate(@params);

        return _mapper.Map<IEnumerable<StudentResultDTO>>(students);
    }

    public async Task<IEnumerable<StudentResultDTO>> GetByToAgeFromAge(int toAge, int fromAge)
    {
        var students = _studentRepository.GetAll(s=> (DateTime.UtcNow.Year-s.DateOfBirth.Year)>=toAge && (DateTime.UtcNow.Year - s.DateOfBirth.Year) <= fromAge, true, new string[] { "StudentSciences" });

        return _mapper.Map<IEnumerable<StudentResultDTO>>(students);
    }

    public async Task<IEnumerable<StudentResultDTO>> GetByFristNameOrLastName(string name)
    {
        name = name.ToLower();

        var students = _studentRepository.GetAll(t=> t.FirstName.ToLower().Contains(name) || t.LastName.ToLower().Contains(name), true, new string[] { "StudentSciences" });

        return _mapper.Map<IEnumerable<StudentResultDTO>>(students);
    }

    public async Task<IEnumerable<StudentResultDTO>> GetByBeelineTelNumber()
    {
        var students = _studentRepository.GetAll(t => t.TelNumber[4] == '9' && (t.TelNumber[5] == '1' || t.TelNumber[5] == '0'), true);

        var result = _mapper.Map<IEnumerable<StudentResultDTO>>(students);

        return result;
    }

    public async Task<IEnumerable<StudentResultDTO>> GetByToDayOfMonthFromDayOfMonth(int toDay, int toMonth, int fromDay, int fromMonth)
    {
        var students = _studentRepository.GetAll(null, true, new string[] { "StudentSciences" });

        var result = _mapper.Map<IEnumerable<StudentResultDTO>>(students)
            .Where(t => (t.DateOfBirth - new DateTime(t.DateOfBirth.Year, toMonth, toDay).ToUniversalTime()).Days >= 0
                && (new DateTime(t.DateOfBirth.Year, fromMonth, fromDay).ToUniversalTime() - t.DateOfBirth).Days >= 0);

        return result;
    }
}
