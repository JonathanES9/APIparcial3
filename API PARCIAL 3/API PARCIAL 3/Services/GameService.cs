using API_PARCIAL_3.Data;
using API_PARCIAL_3.DataTransferObjects;
using API_PARCIAL_3.Models;
using Microsoft.EntityFrameworkCore;
using GameCore;

namespace API_PARCIAL_3.Services
{
    public class GameService : IGameService
    {
        private readonly GameDbContext _context;
        private readonly ILogger<GameService> _logger;
        public GameService(GameDbContext context, ILogger<GameService> logger)
        {
            _context = context;
            _logger = logger; 
        }
        //ENTIDAD PAYER JONI
        public async Task<RegisterPlayerResponse> RegisterPlayerAsync(RegisterPlayerRequest request)
        {
            _logger.LogInformation("Intento de registro para: {FirstName} {LastName}", request.FirstName, request.LastName);
            var existingPlayer = await _context.Players.FirstOrDefaultAsync(p =>
                p.FirstName.ToLower() == request.FirstName.ToLower() &&
                p.LastName.ToLower() == request.LastName.ToLower());


            if (existingPlayer != null)
            {
             
                _logger.LogWarning("Intento de registro fallido: El jugador {FirstName} {LastName} ya existe (PlayerId: {PlayerId}).", existingPlayer.FirstName, existingPlayer.LastName, existingPlayer.PlayerId); 

                throw new InvalidOperationException("El jugador ya se encuentra registrado.");
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
            _logger.LogInformation("Nuevo jugador registrado: {FirstName} {LastName}, (PlayerId: {PlayerId}).", newPlayer.FirstName, newPlayer.LastName, newPlayer.PlayerId); 
            var response = new RegisterPlayerResponse
            {
                PlayerId = newPlayer.PlayerId
            };

            return response;
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



        //parte Attempt
        public async Task<GuessNumberResponse> GuessNumberAsync(GuessNumberRequest request)
        {
            _logger.LogInformation("Procesando intento: GameId {GameId}, Número {AttemptedNumber}", request.GameId, request.AttemptedNumber);
            var game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == request.GameId);

            if (game == null)
            {
                _logger.LogWarning("Intento fallido: el juego {GameId} no existe.", request.GameId);
                throw new KeyNotFoundException($"El juego {request.GameId} no existe.");
            }

            if (game.IsFinished)
            {
                return new GuessNumberResponse
                {
                    Gameid = game.GameId,
                    AttemptedNumber = request.AttemptedNumber,
                    Message = $"El juego {game.GameId} ya ha finalizado."
                };
            }

            var number = request.AttemptedNumber;
            if (number.Length != 4 || number.Distinct().Count() != 4 || !number.All(char.IsDigit))
            {
                _logger.LogWarning("Número inválido: {AttemptedNumber}. Debe tener 4 dígitos numéricos únicos.", number);
                throw new ArgumentException("Formato inválido: el número debe tener 4 dígitos numéricos distintos, sin repeticiones.");
            }

            var result = Evaluator.ValidateAttempt(game.SecretNumber, request.AttemptedNumber);

            _logger.LogInformation("Evaluación completada: GameId {GameId}, Número {AttemptedNumber}, Resultado: {Message}", game.GameId, number, result.Message);

            var attemptEntity = new Attempt
            {
                Game = game,
                AttemptedNumber = number,
                Message = result.Message,
                AttemptedAt = DateTime.UtcNow
            };

            _context.Attempts.Add(attemptEntity);

            if (result.Fama == 4)
            {
                game.IsFinished = true;
                _logger.LogInformation("Juego finalizado por adivinanza correcta: GameId {GameId}", game.GameId);
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("Intento registrado exitosamente: GameId {GameId}, AttemptedNumber {AttemptedNumber}", game.GameId, number);

            return new GuessNumberResponse
            {
                Gameid = game.GameId,
                AttemptedNumber = number,
                Message = result.Message
            };
        }
    }
}    
    