using StudyManagement.Service.DTOs.Sciences;

namespace StudyManagement.Service.DTOs.Teachers;

public class TeacherResultDTO
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TelNumber { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<ScienceResultDTO> Sciences { get; set; }
}
