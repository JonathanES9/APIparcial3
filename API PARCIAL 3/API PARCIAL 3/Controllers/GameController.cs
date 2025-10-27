using API_PARCIAL_3.DataTransferObjects; 
using API_PARCIAL_3.Services;          
using Microsoft.AspNetCore.Mvc;


namespace API_PARCIAL_3.Controllers
{
    [ApiController]
    [Route("api/game/v1")] 
    public class GameController : ControllerBase
    {
     
        private readonly IGameService _gameService;
        
        public GameController(IGameService gameService)
        {
            _gameService = gameService; 
        }

       
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterPlayerResponse), 200)] 
        [ProducesResponseType(typeof(string), 400)] 
        public async Task<IActionResult> Register([FromBody] RegisterPlayerRequest request)
        {
            
            var response = await _gameService.RegisterPlayerAsync(request);

            return Ok(response);
        }

      
    }
}
