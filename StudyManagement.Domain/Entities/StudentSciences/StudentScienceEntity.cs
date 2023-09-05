using StudyManagement.Domain.Commons;
using StudyManagement.Domain.Entities.Grades;
using StudyManagement.Domain.Entities.Sciences;
using StudyManagement.Domain.Entities.Students;

namespace StudyManagement.Domain.Entities.StudentSciences;

public class StudentScienceEntity : Auditable
{
    public long StudentId { get; set; }
    public StudentEntity Student { get; set; }
    public long ScienceId { get; set; }
    public ScienceEntity Science { get; set; }
    public long? GradeId { get; set; }
    public GradeEntity Grade { get; set; }
}
