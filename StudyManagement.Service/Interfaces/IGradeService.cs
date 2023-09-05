using StudyManagement.Domain.Configrations;
using StudyManagement.Service.DTOs.Grades;

namespace StudyManagement.Service.Interfaces;

public interface IGradeService
{
    Task<GradeResultDTO> CreateAsync(GradeCreationDTO dto);
    Task<GradeResultDTO> UpdateAsync(GradeUpdateDTO dto);
    Task<bool> DeleteAsync(long id);
    Task<GradeResultDTO> GetByIdAsync(long id);
    Task<IEnumerable<GradeResultDTO>> GetAllAsync(PaginationParams @params);
}
