using NotificationApi.Helpers;
using NotificationApi.Models;
using NotificationApi.Models.Requests;
using NotificationApi.Models.Responses;
using NotificationApi.Services;

namespace NotificationApi.Coordinators;

public class NotificationCoordinator
{
    private readonly NotificationHelper _helper;
    private readonly INotificationRepository _repo;
    private readonly ILogger<NotificationCoordinator> _logger;

    public NotificationCoordinator(NotificationHelper helper, INotificationRepository repo, ILogger<NotificationCoordinator> logger)
    {
        _helper = helper;
        _repo = repo;
        _logger = logger;
    }

    public async Task<Result<NotificationResponse>> CreateNotificationAsync(CreateNotificationRequest request)
    {
        var validationErrors = _helper.Validate(request);
        if (validationErrors.Any())
        {
            return Result<NotificationResponse>.Fail(validationErrors);
        }

        var entity = _helper.ToEntity(request);
        await _repo.AddAsync(entity);
        _logger.LogInformation("Notification created: {Id} for user {UserId}", entity.Id, entity.UserId);
        return Result<NotificationResponse>.Ok(NotificationResponse.FromEntity(entity));
    }

    public async Task<IEnumerable<NotificationResponse>> GetNotificationsByUserAsync(string userId)
    {
        var list = await _repo.GetByUserAsync(userId);
        return list.Select(NotificationResponse.FromEntity);
    }

    public async Task<Result<NotificationResponse>> UpdateStatusAsync(Guid id, string status)
    {
        var parsed = _helper.ParseStatus(status);
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) return Result<NotificationResponse>.Fail("Not found");

        entity.Status = parsed;
        entity.UpdatedAtUtc = DateTime.UtcNow;
        await _repo.UpdateAsync(entity);
        _logger.LogInformation("Notification {Id} status updated to {Status}", id, parsed);
        return Result<NotificationResponse>.Ok(NotificationResponse.FromEntity(entity));
    }
}

public class Result<T>
{
    public bool Success { get; private set; }
    public T? Data { get; private set; }
    public List<string> Errors { get; private set; } = new();

    public static Result<T> Ok(T data) => new Result<T> { Success = true, Data = data };
    public static Result<T> Fail(params string[] errors) => new Result<T> { Success = false, Errors = errors.ToList() };
    public static Result<T> Fail(IEnumerable<string> errors) => new Result<T> { Success = false, Errors = errors.ToList() };
}
