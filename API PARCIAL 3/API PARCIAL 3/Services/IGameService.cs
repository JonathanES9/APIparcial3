
using API_PARCIAL_3.DataTransferObjects;

namespace API_PARCIAL_3.Services
{
    public interface IGameService
    {
     
        Task<StartGameResponse> StartGameAsync(StartGameRequest request);
    }
}
