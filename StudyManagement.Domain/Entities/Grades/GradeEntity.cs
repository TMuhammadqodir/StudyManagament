using StudyManagement.Domain.Commons;
using StudyManagement.Domain.Entities.StudentSciences;

namespace StudyManagement.Domain.Entities.Grades;

public class GradeEntity : Auditable
{
    public float Grade { get; set; }
    public long StudentScienceId { get; set; }
    public StudentScienceEntity StudentScience { get; set; }
}
