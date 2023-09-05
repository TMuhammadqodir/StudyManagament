using StudyManagement.Domain.Commons;
using StudyManagement.Domain.Entities.Sciences;

namespace StudyManagement.Domain.Entities.Teachers;

public class TeacherEntity : Person
{
    public ICollection<ScienceEntity> Sciences { get; set; }
}
