using StudyManagement.Service.DTOs.Grades;
using StudyManagement.Service.DTOs.Sciences;
using StudyManagement.Service.DTOs.Students;
using StudyManagement.Service.DTOs.Teachers;

namespace StudyManagement.Service.DTOs.StudentSciences;

public class StudentScienceResultDTO
{
    public long Id { get; set; }
    public StudentResultDTO Student { get; set; }
    public ScienceResultDTO Science { get; set; }
    public GradeResultDTO Grade { get; set; }
}