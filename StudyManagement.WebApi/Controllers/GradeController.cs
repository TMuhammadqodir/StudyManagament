using Microsoft.AspNetCore.Mvc;
using StudyManagement.Domain.Configrations;
using StudyManagement.Service.DTOs.Grades;
using StudyManagement.Service.Interfaces;

namespace StudyManagement.WebApi.Controllers;

public class GradeController : BaseController
{
    private readonly IGradeService _gradeService;

    public GradeController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [HttpPost("Post")]
    public async Task<IActionResult> PostAsync(GradeCreationDTO dto)
    {
        var result = await _gradeService.CreateAsync(dto);

        return Ok(result);
    }


    [HttpPut("Put")]
    public async Task<IActionResult> PutAsync(GradeUpdateDTO dto)
    {
        var result = await _gradeService.UpdateAsync(dto);

        return Ok(result );
    }


    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _gradeService.DeleteAsync(id);

        return Ok(result);
    }


    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _gradeService.GetByIdAsync(id);

        return Ok(result);
    }


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams @params)
    {
        var result = await _gradeService.GetAllAsync(@params);

        return Ok(result);
    }
}
