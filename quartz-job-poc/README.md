# Quartz.NET Delayed Expiration POC

This project demonstrates how to use Quartz.NET to implement delayed expiration of documents in a .NET application using an in-memory store.

## Prerequisites

- .NET 8+

## Setup

1. Clone the repository
2. Run the application with `dotnet run`

## How It Works

This POC demonstrates a biometric identification system where identifications automatically expire after 2 minutes. The key components are:

1. **In-Memory Storage**: Stores identification documents in memory using a ConcurrentDictionary
2. **Quartz.NET Scheduling**: Schedules jobs to expire documents after a 2-minute delay
3. **ASP.NET Core API**: Allows creating and managing identifications

### Key Components

- `Identification.cs`: Document model with an `IsExpired` flag
- `InMemoryIdentificationRepository.cs`: Thread-safe in-memory storage
- `ExpireIdentificationJob.cs`: Quartz job that updates documents in the repository
- `IdentificationService.cs`: Service that creates identifications and schedules expiration jobs
- `IdentificationsController.cs`: API controller for managing identifications

## Testing the POC

1. Start the application with `dotnet run`
2. Access the Swagger UI at `https://localhost:7076/swagger/index.html`
3. Create a new identification using the POST endpoint
4. Check the identification status using the GET endpoint
5. Wait 2 minutes
6. Check the identification status again - it should now be expired

## API Endpoints

- `GET /api/identifications`: Get all identifications
- `GET /api/identifications/{id}`: Get a specific identification
- `POST /api/identifications`: Create a new identification

## Example POST Request

```json
{
  "name": "Test Identification"
}
```

## Implementation Details

When a new identification is created:

1. It's stored in memory with `IsExpired = false`
2. A Quartz job is scheduled to run 2 minutes later
3. When the job runs, it updates the document setting `IsExpired = true`

This demonstrates a clean way to implement delayed operations without polling or complex background tasks. 