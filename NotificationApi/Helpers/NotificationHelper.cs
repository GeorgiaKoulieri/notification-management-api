using NotificationApi.Models;
using NotificationApi.Models.Requests;

namespace NotificationApi.Helpers;

public class NotificationHelper
{
    public List<string> Validate(CreateNotificationRequest request)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(request.UserId)) errors.Add("UserId is required.");
        if (string.IsNullOrWhiteSpace(request.Title)) errors.Add("Title is required.");
        if (string.IsNullOrWhiteSpace(request.Message)) errors.Add("Message is required.");
        if (request.Title?.Length > 140) errors.Add("Title max length is 140 characters.");
        return errors;
    }

    public Notification ToEntity(CreateNotificationRequest request) => new Notification
    {
        Id = Guid.NewGuid(),
        UserId = request.UserId!,
        Title = request.Title!,
        Message = request.Message!,
        Channel = request.Channel ?? "in-app",
        Priority = request.Priority ?? 0,
        Status = NotificationStatus.Pending,
        CreatedAtUtc = DateTime.UtcNow,
        UpdatedAtUtc = DateTime.UtcNow
    };

    public NotificationStatus ParseStatus(string status) =>
        Enum.TryParse<NotificationStatus>(status, true, out var parsed) ? parsed : NotificationStatus.Pending;
}
