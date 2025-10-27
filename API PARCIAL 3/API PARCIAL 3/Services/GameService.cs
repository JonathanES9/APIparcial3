using API_PARCIAL_3.Data; 
using API_PARCIAL_3.DataTransferObjects; 
using API_PARCIAL_3.Models; 
using Microsoft.EntityFrameworkCore; 

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

        // --- ¡ENTIDAD GAME JERE! ---
        public async Task<StartGameResponse> StartGameAsync(StartGameRequest request)
        {
            // REQUISITO: "validar que el id enviado por el usuario tenga el formato correcto"
            // (ASP.NET ya lo valida al ser 'int'. Si fuera 0 o negativo, lo validamos aquí)
            if (request.PlayerId <= 0)
            {
                throw new InvalidOperationException("El formato del PlayerId no es válido.");
            }

            // REQUISITO: "validar que el usuario esté registrado"
            var player = await _context.Players.FindAsync(request.PlayerId);
            if (player == null)
            {
                // ¡LOG MANDATORIO!
                _logger.LogWarning("Intento de inicio de juego fallido: PlayerId {PlayerId} no encontrado.", request.PlayerId);
                // Lanzamos una excepción que el Controller convertirá en 404 (Not Found)
                throw new KeyNotFoundException("El jugador no está registrado.");
            }

            // REQUISITO: "validar si el jugador tiene un juego activo... no debería generar un nuevo juego"
            var activeGame = await _context.Games
                .FirstOrDefaultAsync(g => g.PlayerId == request.PlayerId && g.IsFinished == false);

            if (activeGame != null)
            {
                // ¡LOG MANDATORIO!
                _logger.LogWarning("PlayerId {PlayerId} ya tiene un juego activo (GameId {GameId}). No se puede crear uno nuevo.", request.PlayerId, activeGame.GameId);
                // Lanzamos excepción que el Controller convertirá en 400 (Bad Request)
                throw new InvalidOperationException("Ya tienes un juego activo. Termínalo antes de empezar uno nuevo.");
            }

            // REQUISITO: "generará... un número aleatorio de 4 digitos (numero secreto)"
            string numeroSecreto = GenerateSecretNumber();
            _logger.LogInformation("Número secreto generado para nuevo juego.");

            // REQUISITO: "persistiendo esta información en base de datos"
            var newGame = new Game
            {
                PlayerId = request.PlayerId,
                SecretNumber = numeroSecreto,
                CreateAt = DateTime.UtcNow, // Usar UtcNow es mejor práctica
                IsFinished = false
            };

            _context.Games.Add(newGame);
            await _context.SaveChangesAsync(); // Guarda en la BD

            // ¡LOG MANDATORIA!
            _logger.LogInformation("Nuevo juego (GameId {GameId}) creado para PlayerId {PlayerId}.", newGame.GameId, newGame.PlayerId);

            // REQUISITO: "devolver al usuario el id del juego..."
            // (Devolvemos el Response DTO completo)
            return new StartGameResponse
            {
                GameId = newGame.GameId,
                PlayerId = newGame.PlayerId,
                CreateAt = newGame.CreateAt
            };
        }


        // --- FUNCIÓN AUXILIAR para generar el número secreto ---
        // (4 dígitos sin repetir)
        private string GenerateSecretNumber()
        {
            var random = new Random();
            var digits = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var result = "";
            for (int i = 0; i < 4; i++)
            {
                int index = random.Next(digits.Count); // Elige un índice al azar
                result += digits[index];               // Añade el dígito al string
                digits.RemoveAt(index);                // Lo saca de la lista para no repetirlo
            }
            return result;
        }

       

    }
}