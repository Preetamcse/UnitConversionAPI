using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using UnitConversionAPI.Models;
using UnitConversionAPI.Services;

namespace UnitConversionAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConversionController(ConversionService conversionService) : ControllerBase
{
    [HttpPost("convert")]
    public IActionResult Convert([FromBody] ConversionRequest request)
    {
        var (success, result, error) = conversionService.Convert(request.Value, request.FromUnit, request.ToUnit);

        if (!success) return BadRequest(new { error });

        return Ok(new { request.Value, request.FromUnit, request.ToUnit, result });
    }
}