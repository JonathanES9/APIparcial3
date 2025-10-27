


namespace API_PARCIAL_3.DataTranferObjects
{
    public class GuessNumberResponse
    {
        //Response OK: { “gameid”: 1, “attemptedNumber”: 1234, “message”: “¡Felicidades! Has
        //adivinado el número.”}
        //Response NO OK: { “message”: “El juego 1 ya ha finalizado.” }
        public int gameid { get; set; }
        public int attemptedNumber { get; set; }
        public string message { get; set; } = string.Empty;

    }
}
