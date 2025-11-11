namespace NotificationApi.Models;

public class Notification
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Channel { get; set; } = "in-app"; // in-app, email, sms
    public int Priority { get; set; }
    public NotificationStatus Status { get; set; } = NotificationStatus.Pending;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}

public enum NotificationStatus
{
    Pending,
    Sent,
    Read,
    Failed
}
