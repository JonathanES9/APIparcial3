using API_PARCIAL_3.DataTransferObjects;

namespace API_PARCIAL_3.Services
{
    public interface IGameService
    {
   
        Task<RegisterPlayerResponse> RegisterPlayerAsync(RegisterPlayerRequest request);
        Task<StartGameResponse> StartGameAsync(StartGameRequest request);
        Task<GuessNumberResponse> GuessNumberAsync(GuessNumberRequest request);
    }
}
