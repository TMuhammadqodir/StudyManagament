using StudyManagement.Service.DTOs.StudentSciences;
using StudyManagement.Service.DTOs.Teachers;

namespace StudyManagement.Service.DTOs.Sciences;

public class ScienceResultDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TeacherResultDTO Teacher { get; set; }
    public ICollection<StudentScienceResultDTO> StudentSciences { get; set; }
}