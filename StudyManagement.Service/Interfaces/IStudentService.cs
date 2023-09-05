using StudyManagement.Domain.Configrations;
using StudyManagement.Service.DTOs.Sciences;
using StudyManagement.Service.DTOs.Students;

namespace StudyManagement.Service.Interfaces;

public interface IStudentService
{
    Task<StudentResultDTO> CreateAsync(StudentCreationDTO dto);
    Task<StudentResultDTO> UpdateAsync(StudentUpdateDTO dto);
    Task<bool> DeleteAsync(long id);
    Task<StudentResultDTO> GetByIdAsync(long id);
    Task<IEnumerable<StudentResultDTO>> GetAllAsync(PaginationParams @params);
    Task<IEnumerable<StudentResultDTO>> GetByToAgeFromAge(int toAge, int fromAge);
    Task<IEnumerable<StudentResultDTO>> GetByFristNameOrLastName(string name);
    Task<IEnumerable<StudentResultDTO>> GetByBeelineTelNumber();
    Task<IEnumerable<StudentResultDTO>> GetByToDayOfMonthFromDayOfMonth(int toDay, int toMonth, int fromDay, int fromMonth);
}
