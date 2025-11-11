using NotificationApi.Models;
using System.Collections.Concurrent;

namespace NotificationApi.Services;

public class InMemoryNotificationRepository : INotificationRepository
{
    private readonly ConcurrentDictionary<Guid, Notification> _store = new();

    public Task AddAsync(Notification entity)
    {
        _store[entity.Id] = entity;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Notification entity)
    {
        _store[entity.Id] = entity;
        return Task.CompletedTask;
    }

    public Task<Notification?> GetByIdAsync(Guid id)
    {
        _store.TryGetValue(id, out var entity);
        return Task.FromResult(entity);
    }

    public Task<IEnumerable<Notification>> GetByUserAsync(string userId)
    {
        var list = _store.Values.Where(n => n.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase))
                                .OrderByDescending(n => n.CreatedAtUtc)
                                .AsEnumerable();
        return Task.FromResult(list);
    }
}
