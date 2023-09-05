using Microsoft.AspNetCore.Mvc;
using StudyManagement.Domain.Configrations;
using StudyManagement.Service.DTOs.Students;
using StudyManagement.Service.Interfaces;

namespace StudyManagement.WebApi.Controllers;

public class StudentController : BaseController
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost("Post")]
    public async Task<IActionResult> PostAsync(StudentCreationDTO dto)
    {
        var result = await _studentService.CreateAsync(dto);

        return Ok(result);
    }


    [HttpPut("Put")]
    public async Task<IActionResult> PutAsync(StudentUpdateDTO dto)
    {
        var result = await _studentService.UpdateAsync(dto);

        return Ok(result);
    }


    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _studentService.DeleteAsync(id);

        return Ok(result);
    }


    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _studentService.GetByIdAsync(id);

        return Ok(result);
    }


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams @params)
    {
        var result = await _studentService.GetAllAsync(@params);

        return Ok(result);
    }

    [HttpGet("GetByToAgeFromAge")]
    public async Task<IActionResult> GetByToAgeFromAge(int toAge = 0, int fromAge = 120)
    {
        var result = await _studentService.GetByToAgeFromAge(toAge, fromAge);

        return Ok(result);
    }

    [HttpGet("GetByFirstNameOrLastName")]
    public async Task<IActionResult> GetByFirstNameOrLastName(string name)
    {
        var result = await _studentService.GetByFristNameOrLastName(name);

        return Ok(result);
    }

    [HttpGet("GetByBeelineTelNumber")]
    public async Task<IActionResult> GetByBeelineTelNumber()
    {
        var result = await _studentService.GetByBeelineTelNumber();

        return Ok(result);
    }


    [HttpGet("GetByToDayOfMonthFromDayOfMonth")]
    public async Task<IActionResult> GetByToDayOfMonthFromDayOfMonth(int toDay=1, int toMonth=1, int fromDay=28, int fromMonth=12)
    {
        var result = await _studentService.GetByToDayOfMonthFromDayOfMonth(toDay, toMonth, fromDay, fromMonth);

        return Ok(result);
    }
}
