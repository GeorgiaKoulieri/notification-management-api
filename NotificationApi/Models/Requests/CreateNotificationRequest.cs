namespace NotificationApi.Models.Requests;

public class CreateNotificationRequest
{
    public string? UserId { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
    public string? Channel { get; set; }
    public int? Priority { get; set; }
}
