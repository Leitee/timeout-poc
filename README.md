# MyTimeoutApp - Fire and Forget with Timeout Functionality

This application demonstrates a "fire-and-forget" pattern with timeout capabilities using C# and ASP.NET Core.

## Project Overview

The application implements a fire-and-forget mechanism that:
- Starts an operation asynchronously
- Sets a timeout for the operation (default: 10 seconds)
- Executes a fallback action if the timeout is reached
- Allows external completion of the operation before the timeout

## Architecture

Built with .NET 8 using a Clean Architecture approach:
- **Controllers**: Handle HTTP requests
- **Services**: Core business logic
- **Handlers**: Command processing
- **Models**: Data structures

## Key Components

- `TimeoutService`: Manages timeout operations using TaskCompletionSource
- `MyCommandHandler`: Processes commands and initiates timeout operations
- `RequestController`: Exposes REST endpoints to start and complete operations

## How to Run

1. Build the project:
   ```
   dotnet build
   ```

2. Run the application:
   ```
   dotnet run
   ```

3. Use the test script to test the timeout functionality:
   ```
   .\test-timeout.ps1
   ```

## API Endpoints

- `POST /request/start`: Start a new timeout operation
- `POST /request/complete/{id}`: Manually complete an operation

## Use Cases

This pattern is useful for:
- Long-running operations that shouldn't block the main thread
- Operations that may or may not complete in time
- Systems requiring fallback actions when operations timeout 