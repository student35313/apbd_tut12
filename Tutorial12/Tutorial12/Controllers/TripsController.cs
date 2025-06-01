using Microsoft.AspNetCore.Mvc;
using Tutorial12.DTOs;
using Tutorial12.Services;
using Tutorial9.Exceptions;

namespace Tutorial12.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly IDBService _dbService;

    public TripsController(IDBService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await _dbService.GetTripsAsync(page, pageSize);
            return Ok(result);
        }
        catch (BadHttpRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal server error" });
        }
    }
    
    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> RegisterClient(int idTrip,  RegisterClientToTripDTO dto)
    {
        try
        {
            await _dbService.RegisterClientToTripAsync(idTrip, dto);
            return Ok("Client registered successfully.");
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
        catch (BadHttpRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal server error" });
        }
    }
}