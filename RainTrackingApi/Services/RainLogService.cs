using AutoMapper;
using RainTrackingApi.Models.Domain;
using RainTrackingApi.Models.DTO;
using RainTrackingApi.Repositories.Interfaces;
using RainTrackingApi.Services.Interfaces;

namespace RainTrackingApi.Services
{
    public class RainLogService : IRainLogService
    {
        private readonly IRainTrackingRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<RainLogService> _logger;

        public RainLogService(IRainTrackingRepository repository, IMapper mapper, ILogger<RainLogService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<UserRainLog>> GetAllAsync()
        {
            var rainLogs = await _repository.GetAllRainLogAsync();

            return rainLogs;
        }

        public async Task<List<UserRainLog>> GetByUserIdAsync(string userId, bool? isRaining = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("userId required", nameof(userId));

            var rainLogs = await _repository.GetRainLogsByUserIdentifierAsync(userId, isRaining);

            return rainLogs;
        }

        public async Task<UserRainLog> CreateAsync(CreateUserRainLogDto createRainLog)
        {
            if (string.IsNullOrWhiteSpace(createRainLog.UserIdentifier))
                throw new ArgumentException("userIdentifier required", nameof(createRainLog.UserIdentifier));

            var user = await _repository.GetUserByIdentifierAsync(createRainLog.UserIdentifier) 
                ?? await CreateUser(createRainLog.UserIdentifier);

            var rainLog = _mapper.Map<UserRainLog>(createRainLog);
            rainLog.User = user;
            rainLog.Timestamp = DateTime.UtcNow;

            try
            {
                await _repository.AddRainLogAsync(rainLog);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create user rain log.");
                throw;
            }

            return rainLog;
        }

        private async Task<User> CreateUser(string userIdentifier)
        {
            var user = new User
            {
                UserIdentifier = userIdentifier,
                DateCreated = DateTime.UtcNow
            };

            try
            {
                await _repository.AddUserAsync(user);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create user with identifier {UserIdentifier}", userIdentifier);
                throw;
            }

            return user;
        }
    }
}
