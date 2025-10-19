using RainTrackingApi.Models.Domain;
using RainTrackingApi.Models.DTO;

namespace RainTrackingApi.Services.Interfaces
{
    public interface IRainLogService
    {
        Task<List<UserRainLog>> GetAllAsync();

        Task<List<UserRainLog>> GetByUserIdAsync(string userId, bool? isRaining = null);

        Task<UserRainLog> CreateAsync(CreateUserRainLogModel createRainLog);
    }
}
