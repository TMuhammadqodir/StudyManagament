using StudyManagement.Domain.Configrations;
using StudyManagement.Service.DTOs.StudentSciences;

public interface IStudentScienceService
{
    Task<StudentScienceResultDTO> CreateAsync(StudentScienceCreationDTO dto);
    Task<StudentScienceResultDTO> UpdateAsync(StudentScienceUpdateDTO dto);
    Task<bool> DeleteAsync(long id);
    Task<StudentScienceResultDTO> GetByIdAsync(long id);
    Task<IEnumerable<StudentScienceResultDTO>> GetAllAsync(PaginationParams @params);
}