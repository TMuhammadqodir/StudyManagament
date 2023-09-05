﻿using StudyManagement.Service.DTOs.StudentSciences;

namespace StudyManagement.Service.DTOs.Students;

public class StudentResultDTO
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TelNumber { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int StudentRegNumber { get; set; }
}