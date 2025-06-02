using Microsoft.AspNetCore.Mvc;
using Tutorial12.Services;
using Tutorial9.Exceptions;

namespace Tutorial12.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IDBService _dbService;

    public ClientsController(IDBService dbService)
    {
        _dbService = dbService;
    }

    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        try
        {
            await _dbService.DeleteClientAsync(idClient);
            return Ok("Client deleted successfully.");
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal server error" });
        }
    }
}