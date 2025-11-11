using NotificationApi.Models;

namespace NotificationApi.Models.Responses;

public class NotificationResponse
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Channel { get; set; } = "in-app";
    public int Priority { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }

    public static NotificationResponse FromEntity(Notification entity) => new NotificationResponse
    {
        Id = entity.Id,
        UserId = entity.UserId,
        Title = entity.Title,
        Message = entity.Message,
        Channel = entity.Channel,
        Priority = entity.Priority,
        Status = entity.Status.ToString(),
        CreatedAtUtc = entity.CreatedAtUtc,
        UpdatedAtUtc = entity.UpdatedAtUtc
    };
}
