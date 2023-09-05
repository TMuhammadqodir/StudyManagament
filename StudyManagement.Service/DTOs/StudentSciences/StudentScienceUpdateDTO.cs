namespace StudyManagement.Service.DTOs.StudentSciences;

public class StudentScienceUpdateDTO
{
    public long Id { get; set; }
    public long StudentId { get; set; }
    public long ScienceId { get; set; }
    public long? GradeId { get; set; }
}
