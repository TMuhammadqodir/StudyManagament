using StudyManagement.Domain.Commons;
using StudyManagement.Domain.Entities.StudentSciences;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyManagement.Domain.Entities.Students;

public class StudentEntity : Person
{
    public int StudentRegNumber { get; set; }
    public ICollection<StudentScienceEntity> StudentSciences { get; set; }
}
