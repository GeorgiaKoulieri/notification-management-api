# Notification Management API

A lightweight **.NET 8 Web API** built with a clean, layered architecture.  
It applies a structured design pattern (**Controller → Coordinator → Helper**) and leverages dependency injection, logging, and unit testing for maintainability and scalability.

The API enables creation, retrieval, and status updates of user notifications.  
It uses an **in-memory repository**, allowing it to run easily without any additional configuration.

---

## Tech Stack

- .NET 8 (C#)
- ASP.NET Core Web API
- xUnit (unit testing)
- In-memory data store (thread-safe)
- Swagger (for development)

---

## Project Structure

NotificationApi/
├── Controllers/
│ └── NotificationsController.cs
├── Coordinators/
│ └── NotificationCoordinator.cs
├── Helpers/
│ └── NotificationHelper.cs
├── Models/
│ ├── Notification.cs
│ ├── Requests/
│ │ └── CreateNotificationRequest.cs
│ └── Responses/
│ └── NotificationResponse.cs
├── Services/
│ ├── INotificationRepository.cs
│ └── InMemoryNotificationRepository.cs
├── Program.cs
└── NotificationApi.csproj



---

## How to Run

1. Install the [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
2. Open a terminal in the project folder and run:
   ```bash
   dotnet run --project NotificationApi/NotificationApi.csproj
3. The API starts by default at:

https://localhost:5001

or http://localhost:5000

4. Swagger UI is available in Development mode at /swagger.

Endpoints
Create a notification

http
POST /api/notifications

Content-Type: application/json

{
  "userId": "user1 ",
  "title": "Welcome",
  "message": "Thanks for joining!",
  "channel": "in-app",
  "priority": 1
}

Get notifications by user

http
GET /api/notifications/user1


Update notification status

http
PATCH /api/notifications/{id}/status/{status}
# status: Pending | Sent | Read | Failed

Tests
Run the test suite:

bash
dotnet test
