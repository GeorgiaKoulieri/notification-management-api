using Microsoft.AspNetCore.Mvc;
using NotificationApi.Coordinators;
using NotificationApi.Models.Requests;
using NotificationApi.Models.Responses;

namespace NotificationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly NotificationCoordinator _coordinator;
    private readonly ILogger<NotificationsController> _logger;

    public NotificationsController(NotificationCoordinator coordinator, ILogger<NotificationsController> logger)
    {
        _coordinator = coordinator;
        _logger = logger;
    }

    /// <summary>
    /// Create a new notification for a user.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(NotificationResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateNotificationRequest request)
    {
        var result = await _coordinator.CreateNotificationAsync(request);
        if (!result.Success)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Validation failed",
                Detail = string.Join("; ", result.Errors)
            });
        }

        var location = Url.Action(nameof(GetByUser), new { userId = request.UserId });
        return Created(location ?? string.Empty, result.Data);
    }

    /// <summary>
    /// Get notifications by user id.
    /// </summary>
    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(IEnumerable<NotificationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUser([FromRoute] string userId)
    {
        var list = await _coordinator.GetNotificationsByUserAsync(userId);
        return Ok(list);
    }

    /// <summary>
    /// Update notification status
    /// </summary>
    [HttpPatch("{id:guid}/status/{status}")]
    [ProducesResponseType(typeof(NotificationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromRoute] string status)
    {
        var result = await _coordinator.UpdateStatusAsync(id, status);
        if (!result.Success || result.Data is null)
        {
            return NotFound(new ProblemDetails { Title = "Notification not found" });
        }
        return Ok(result.Data);
    }
}
