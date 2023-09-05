using Microsoft.AspNetCore.Mvc;
using StudyManagement.Domain.Configrations;
using StudyManagement.Service.DTOs.Teachers;
using StudyManagement.Service.Interfaces;
using StudyManagement.Service.Services;

namespace StudyManagement.WebApi.Controllers;

public class TeacherController : BaseController
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpPost("Post")]
    public async Task<IActionResult> PostAsync(TeacherCreationDTO dto)
    {
        var result = await _teacherService.CreateAsync(dto);

        return Ok(result);
    }


    [HttpPut("Put")]
    public async Task<IActionResult> PutAsync(TeacherUpdateDTO dto)
    {
        var result = await _teacherService.UpdateAsync(dto);

        return Ok(result);
    }


    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _teacherService.DeleteAsync(id);

        return Ok(result);
    }


    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _teacherService.GetByIdAsync(id);

        return Ok(result);
    }


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams @params)
    {
        var result = await _teacherService.GetAllAsync(@params);

        return Ok(result);
    }

    [HttpGet("GetByToAgeFromAge")]
    public async Task<IActionResult> GetByToAgeFromAge(int toAge = 0, int fromAge = 120)
    {
        var result = await _teacherService.GetByToAgeFromAge(toAge, fromAge);

        return Ok(result);
    }

    [HttpGet("GetByBeelineTelNumber")]
    public async Task<IActionResult> GetByBeelineTelNumber()
    {
        var result = await _teacherService.GetByBeelineTelNumber();

        return Ok(result);
    }

    [HttpGet("GetByTeachersOfStudentsWithHighestGrade")]
    public async Task<IActionResult> GetByTeachersOfStudentsWithHighestGrade(int maxGrade)
    {
        var result = await _teacherService.GetByTeachersOfStudentsWithHighestGrade(maxGrade);

        return Ok(result);
    }
}
