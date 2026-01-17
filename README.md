# UserManagementAPI

A simple ASP.NET Core Web API for managing users.

## Features
- CRUD endpoints: GET, POST, PUT, DELETE
- Input validation for user data
- Middleware:
  - Request/response logging
  - Global error handling
  - Token-based authentication middleware (basic)

## How to run
1. Open the project folder in a terminal
2. Run:
   dotnet restore
   dotnet run

The API will start and print a localhost URL (example: http://localhost:5249).

## Testing
You can test endpoints using:
- Swagger UI (when enabled in Development)
- Postman
- The included `.http` file
