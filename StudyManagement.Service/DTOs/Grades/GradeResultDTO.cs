using StudyManagement.Service.DTOs.StudentSciences;

namespace StudyManagement.Service.DTOs.Grades;

public class GradeResultDTO
{
    public long Id { get; set; }
    public float Grade { get; set; }
    public StudentScienceResultDTO StudentScience { get; set; }
}