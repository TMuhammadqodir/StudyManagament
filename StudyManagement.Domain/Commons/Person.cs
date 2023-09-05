﻿namespace StudyManagement.Domain.Commons;

public abstract class Person : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TelNumber { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
}
