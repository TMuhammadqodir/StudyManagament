using Microsoft.AspNetCore.Mvc;
using StudyManagement.Domain.Configrations;
using StudyManagement.Service.DTOs.Sciences;
using StudyManagement.Service.Interfaces;
using StudyManagement.Service.Services;

namespace StudyManagement.WebApi.Controllers;

public class ScienceController : BaseController
{
    private readonly IScienceService _scienceService;

    public ScienceController(IScienceService scienceService)
    {
        _scienceService = scienceService;
    }

    [HttpPost("Post")]
    public async Task<IActionResult> PostAsync(ScienceCreationDTO dto)
    {
        var result = await _scienceService.CreateAsync(dto);

        return Ok(result);
    }


    [HttpPut("Put")]
    public async Task<IActionResult> PutAsync(ScienceUpdateDTO dto)
    {
        var result = await _scienceService.UpdateAsync(dto);

        return Ok(result);
    }


    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _scienceService.DeleteAsync(id);

        return Ok(result);
    }


    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _scienceService.GetByIdAsync(id);

        return Ok(result);
    }


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams @params)
    {
        var result = await _scienceService.GetAllAsync(@params);

        return Ok(result);
    }


    [HttpGet("GetByScienceOfHighGrade")]
    public async Task<IActionResult> GetByScienceOfHighGrade(long id)
    {
        var result = await _scienceService.GetByScienceOfHighgGrade(id);

        return Ok(result);
    }

    [HttpGet("GetByHighestGradePointAverage")]
    public async Task<IActionResult> GetByHighestGradePointAverage()
    {
        var result = await _scienceService.GetByHighestGradePointAverage();

        return Ok(result);
    }
}
