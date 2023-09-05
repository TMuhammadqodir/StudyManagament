using StudyManagement.Domain.Configrations;
using StudyManagement.Service.DTOs.Students;
using StudyManagement.Service.DTOs.Teachers;

namespace StudyManagement.Service.Interfaces;

public interface ITeacherService
{
    Task<TeacherResultDTO> CreateAsync(TeacherCreationDTO dto);
    Task<TeacherResultDTO> UpdateAsync(TeacherUpdateDTO dto);
    Task<bool> DeleteAsync(long id);
    Task<TeacherResultDTO> GetByIdAsync(long id);
    Task<IEnumerable<TeacherResultDTO>> GetAllAsync(PaginationParams @params);
    Task<IEnumerable<TeacherResultDTO>> GetByToAgeFromAge(int toAge, int fromAge);
    Task<IEnumerable<TeacherResultDTO>> GetByBeelineTelNumber();
    Task<IEnumerable<TeacherResultDTO>> GetByTeachersOfStudentsWithHighestGrade(int maxGrade);
}
