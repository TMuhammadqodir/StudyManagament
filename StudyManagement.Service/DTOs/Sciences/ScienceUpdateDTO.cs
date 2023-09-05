namespace StudyManagement.Service.DTOs.Sciences;

public class ScienceUpdateDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long TeacherId { get; set; }
}
