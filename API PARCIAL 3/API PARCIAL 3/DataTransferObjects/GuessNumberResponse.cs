
namespace API_PARCIAL_3.DataTransferObjects
{
    public class GuessNumberResponse
    {
        public int Gameid { get; set; }
        public string AttemptedNumber { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

    }
}
