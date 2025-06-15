# PA Event Notification

This project is a .NET 9 cron/scheduler service that aggregates the community events happening in Jurong West region in Singapore for the current month.

## Configuration
- Edit `source/PA.EventNotification.Host/appsettings.json` for environment-specific settings.

## Development
- .NET 9 SDK required
- Run locally with:
   ```sh
   cd source && dotnet run --project PA.EventNotification.Host
   ```

## Building an image

To build and run the service in Docker, execute the following script from the project root:

```sh
./source/build-docker.sh <tagversion>
```
Replace `<tagversion>` with tag version required parameters for your environment.

## Disclaimer
All event information is sourced from the People's Association website. People's Association reserves the rights and information for all the events. This application only aggregates and emails event details for convenience.

## License
MIT
