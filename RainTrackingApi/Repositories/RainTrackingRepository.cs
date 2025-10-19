using Microsoft.EntityFrameworkCore;
using RainTrackingApi.Data;
using RainTrackingApi.Models.Domain;
using RainTrackingApi.Repositories.Interfaces;

namespace RainTrackingApi.Repositories
{
    public class RainTrackingRepository : IRainTrackingRepository
    {
        private readonly RainTrackingDbContext _dbContext;
        public RainTrackingRepository(RainTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserRainLog>> GetAllRainLogAsync()
        {
            return await _dbContext.UserRainLog
                .AsNoTracking()
                .Include(usr => usr.User)
                .OrderByDescending(usr => usr.Timestamp)
                .ToListAsync();
        }

        public async Task<List<UserRainLog>> GetRainLogsByUserIdentifierAsync(string userIdentifier, bool? isRaining = null)
        {
            var query = _dbContext.UserRainLog
                .AsNoTracking()
                .Include(r => r.User)
                .Where(r => r.User.UserIdentifier == userIdentifier);

            if (isRaining.HasValue)
            {
                query = query.Where(r => r.Rain == isRaining.Value);
            }

            return await query
                .OrderByDescending(r => r.Timestamp)
                .ToListAsync();
        }

        public async Task<User?> GetUserByIdentifierAsync(string userIdentifier)
        {
            if (string.IsNullOrWhiteSpace(userIdentifier))
                return null;

            return await _dbContext.User
                .FirstOrDefaultAsync(u => u.UserIdentifier == userIdentifier);
        }

        public Task AddUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _dbContext.User.Add(user);
            return Task.CompletedTask;
        }

        public Task AddRainLogAsync(UserRainLog rainLog)
        {
            if (rainLog == null) throw new ArgumentNullException(nameof(rainLog));
            _dbContext.UserRainLog.Add(rainLog);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
