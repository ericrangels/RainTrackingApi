using RainTrackingApi.Models.Domain;

namespace RainTrackingApi.Repositories.Interfaces
{
    public interface IRainTrackingRepository
    {
        Task<List<UserRainLog>> GetAllRainLogAsync();

        Task<List<UserRainLog>> GetRainLogsByUserIdentifierAsync(string userIdentifier);

        Task<User?> GetUserByIdentifierAsync(string userIdentifier);

        Task AddUserAsync(User user);

        Task AddRainLogAsync(UserRainLog rainLog);

        Task SaveChangesAsync();
    }
}
