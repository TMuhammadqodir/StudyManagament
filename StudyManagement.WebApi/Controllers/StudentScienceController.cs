using Microsoft.AspNetCore.Mvc;
using StudyManagement.Domain.Configrations;
using StudyManagement.Service.DTOs.StudentSciences;
using StudyManagement.Service.Interfaces;

namespace StudyManagement.WebApi.Controllers;

public class StudentScienceController: BaseController
{
    private readonly IStudentScienceService _studentScienceService;

    public StudentScienceController(IStudentScienceService studentScienceService)
    {
        _studentScienceService = studentScienceService;
    }

    [HttpPost("Post")]
    public async Task<IActionResult> PostAsync(StudentScienceCreationDTO dto)
    {
        var result = await _studentScienceService.CreateAsync(dto);

        return Ok(result);
    }


    [HttpPut("Put")]
    public async Task<IActionResult> PutAsync(StudentScienceUpdateDTO dto)
    {
        var result = await _studentScienceService.UpdateAsync(dto);

        return Ok(result);
    }


    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _studentScienceService.DeleteAsync(id);

        return Ok(result);
    }


    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _studentScienceService.GetByIdAsync(id);

        return Ok(result);
    }


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams @params)
    {
        var result = await _studentScienceService.GetAllAsync(@params);

        return Ok(result);
    }
}
