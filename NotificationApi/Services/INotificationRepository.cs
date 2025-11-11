using NotificationApi.Models;

namespace NotificationApi.Services;

public interface INotificationRepository
{
    Task AddAsync(Notification entity);
    Task UpdateAsync(Notification entity);
    Task<Notification?> GetByIdAsync(Guid id);
    Task<IEnumerable<Notification>> GetByUserAsync(string userId);
}
