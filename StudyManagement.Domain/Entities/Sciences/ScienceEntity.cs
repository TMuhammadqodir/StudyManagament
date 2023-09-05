using StudyManagement.Domain.Commons;
using StudyManagement.Domain.Entities.StudentSciences;
using StudyManagement.Domain.Entities.Teachers;

namespace StudyManagement.Domain.Entities.Sciences;

public class ScienceEntity : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long TeacherId { get; set; }
    public TeacherEntity Teacher { get; set; }
    public ICollection<StudentScienceEntity> StudentSciences { get; set; }
}
