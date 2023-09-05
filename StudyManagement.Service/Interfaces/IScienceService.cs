using StudyManagement.Domain.Configrations;
using StudyManagement.Service.DTOs.Sciences;
using System.Diagnostics;

namespace StudyManagement.Service.Interfaces;

public interface IScienceService
{
    Task<ScienceResultDTO> CreateAsync(ScienceCreationDTO dto);
    Task<ScienceResultDTO> UpdateAsync(ScienceUpdateDTO dto);
    Task<bool> DeleteAsync(long id);
    Task<ScienceResultDTO> GetByIdAsync(long id);
    Task<IEnumerable<ScienceResultDTO>> GetAllAsync(PaginationParams @params);
    Task<ScienceResultDTO> GetByScienceOfHighgGrade(long id);
    Task<ScienceResultDTO> GetByHighestGradePointAverage();
}
