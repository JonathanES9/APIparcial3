using API_PARCIAL_3.DataTransferObjects;
using API_PARCIAL_3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_PARCIAL_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        
        private readonly ILogger<GameController> _logger;

        
        public GameController(IGameService gameService, ILogger<GameController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        //CONTROLLER - START GAME JERE
        [HttpPost("start")]
        public async Task<IActionResult> StartGame([FromBody] StartGameRequest request)
        {
            try
            {            
                var response = await _gameService.StartGameAsync(request);

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {                
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al intentar iniciar un juego para PlayerId {PlayerId}", request.PlayerId);
                
                return StatusCode(500, new { message = "Error interno del servidor." });
            }
        }

        
    }
}


    
