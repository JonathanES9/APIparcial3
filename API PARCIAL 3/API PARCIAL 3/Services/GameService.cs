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
    // --- ¡ENTIDAD GAME JERE! ---
        public async Task<StartGameResponse> StartGameAsync(StartGameRequest request)
        {

            if (request.PlayerId <= 0)
            {
                throw new InvalidOperationException("El formato del PlayerId no es válido.");
            }


            var player = await _context.Players.FindAsync(request.PlayerId);
            if (player == null)
            {

                _logger.LogWarning("Intento de inicio de juego fallido: PlayerId {PlayerId} no encontrado.", request.PlayerId);

                throw new KeyNotFoundException("El jugador no está registrado.");
            }


            var activeGame = await _context.Games
                .FirstOrDefaultAsync(g => g.PlayerId == request.PlayerId && g.IsFinished == false);

            if (activeGame != null)
            {

                _logger.LogWarning("PlayerId {PlayerId} ya tiene un juego activo (GameId {GameId}). No se puede crear uno nuevo.", request.PlayerId, activeGame.GameId);

                throw new InvalidOperationException("Ya tienes un juego activo. Termínalo antes de empezar uno nuevo.");
            }

            string numeroSecreto = GenerateSecretNumber();
            _logger.LogInformation("Número secreto generado para nuevo juego.");

            var newGame = new Game
            {
                PlayerId = request.PlayerId,
                SecretNumber = numeroSecreto,
                CreateAt = DateTime.UtcNow,
                IsFinished = false
            };

            _context.Games.Add(newGame);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Nuevo juego (GameId {GameId}) creado para PlayerId {PlayerId}.", newGame.GameId, newGame.PlayerId);

            return new StartGameResponse
            {
                GameId = newGame.GameId,
                PlayerId = newGame.PlayerId,
                CreateAt = newGame.CreateAt
            };
        }
        private string GenerateSecretNumber()
        {
            var random = new Random();
            var digits = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var result = "";
            for (int i = 0; i < 4; i++)
            {
                int index = random.Next(digits.Count);
                result += digits[index];
                digits.RemoveAt(index);
            }
            return result;
        }
    }