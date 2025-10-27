using API_PARCIAL_3.Data;
using API_PARCIAL_3.DataTransferObjects;
using API_PARCIAL_3.Models;
using Microsoft.EntityFrameworkCore;    


namespace API_PARCIAL_3.Services
{
    public class GameService : IGameService
    {
        private readonly GameDbContext _context;
       
        public GameService(GameDbContext context)
        {
            _context = context;
        }

        public async Task<RegisterPlayerResponse> RegisterPlayerAsync(RegisterPlayerRequest request)
        {
         
            var existingPlayer = await _context.Players.FirstOrDefaultAsync(p =>
                p.FirstName.ToLower() == request.FirstName.ToLower() &&
                p.LastName.ToLower() == request.LastName.ToLower());

         
            if (existingPlayer != null)
            {
                return new RegisterPlayerResponse { PlayerId = existingPlayer.PlayerId };
            }

          
            var newPlayer = new Player
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                RegistrationDate = DateTime.Now 
            };

          
            _context.Players.Add(newPlayer);
           
            await _context.SaveChangesAsync();
            
            var response = new RegisterPlayerResponse
            {
                PlayerId = newPlayer.PlayerId 
            };

            return response;
        }

       
    }
}