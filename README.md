# PA Event Notification

This project is a .NET 9 background service for sending event notifications via email.

## Project Structure
- `PA.EventNotification.Host/` - Main service project
- `EmailTemplates/` - HTML templates for emails
- `Services/` - Hosted/background services
- `Implementation/` - Email formatting and sending logic
- `Models/` - Data models
- `Abstracts/` - Interfaces and abstractions

## Running with Docker Compose

1. Build and run the service:
   ```sh
   docker compose up --build
   ```
2. The service will be available on port 8080 (adjust as needed).

## Configuration
- Edit `PA.EventNotification.Host/appsettings.json` for environment-specific settings.

## Development
- .NET 9 SDK required
- Run locally with:
   ```sh
   dotnet run --project PA.EventNotification.Host
   ```

## License
MIT
