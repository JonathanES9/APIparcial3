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
        // También inyectamos un Logger para el controlador
        private readonly ILogger<GameController> _logger;

        // Inyectamos el servicio y el logger en el constructor
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
                // 1. Llama al "cerebro" (el servicio)
                var response = await _gameService.StartGameAsync(request);

                // 2. Si todo sale bien, devuelve 200 OK (Response OK)
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                // 3. Si el servicio lanzó "KeyNotFound" (Player no existe)
                // Devuelve 404 Not Found (Response NO OK)
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // 4. Si el servicio lanzó "InvalidOperation" (Juego ya activo, ID inválido)
                // Devuelve 400 Bad Request (Response NO OK)
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // 5. Para cualquier otro error inesperado (¡LOG MANDATORIO!)
                _logger.LogError(ex, "Error inesperado al intentar iniciar un juego para PlayerId {PlayerId}", request.PlayerId);
                // Devuelve 500 Internal Server Error (Response NO OK)
                return StatusCode(500, new { message = "Error interno del servidor." });
            }
        }

        
    }
}


    
