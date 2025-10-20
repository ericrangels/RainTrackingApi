# RainTrackingApi

A HTTP REST API in C# ASP.NET Core Web (.NET 8) to record and query simple rain logs per user. 
The API stores users and their rain observations (timestamp, rain true/false, optional latitude/longitude) 
in a PostgreSQL database and exposes endpoints to create and retrieve logs. 
Swagger UI and example requests are included.

**Repository:** https://github.com/ericrangels/RainTrackingApi

---

## Features

- **POST** `/api/data` — create a rain log for a user (creates user if missing)
- **GET** `/api/data` — fetch a user's rain logs (supports optional `rain` query filter)
- Swagger UI with enhanced documentation and examples at `/swagger`
- Custom validation error schemas with detailed error messages
- Uses EF Core with `RainTrackingDbContext` (PostgreSQL)
- AutoMapper for DTO ↔ domain mappings
- Simple repository + service layering

---

## Prerequisites

**All platforms (Windows, macOS, Linux):**
- **Docker Desktop** 4.0+ or **Docker Engine** 20.10+
- **Docker Compose** 2.0+ (included with Docker Desktop)
- **Git** 2.25+ (for cloning and version control)

**Note:** All commands in this guide work on Windows, macOS, and Linux. Use Terminal on macOS/Linux or PowerShell/Command Prompt on Windows.

**What you DON'T need to install locally:**
- .NET 8 SDK (runs in Docker container)
- PostgreSQL (runs in Docker container)
- Entity Framework tools (installed in Docker container)

---

## Quick Start

### 1. Clone the repository

```bash
git clone https://github.com/ericrangels/RainTrackingApi.git
cd RainTrackingApi
```

### 2. Navigate to the API directory

```bash
cd RainTrackingApi
```

### 3. Start the services

```bash
docker-compose up --build
```

This will:
- Build the .NET 8 API Docker image using the included Dockerfile
- Start a PostgreSQL database container (`raindb`)
- Install Entity Framework tools in the container
- Expose the API on `http://localhost:5000`

### 4. Apply database migrations

Open a new terminal window and run:

```bash
docker-compose exec api sh -c "cd /src && dotnet ef database update"
```

This creates the necessary database tables (`User` and `UserRainLog`).

### 5. Access the API

- **Swagger UI:** http://localhost:5000/swagger
- **API Base:** http://localhost:5000/api/data

### 6. Stop the services

```bash
docker-compose down
```

To remove all data (including the database):

```bash
docker-compose down -v
```

---

## API Usage

All endpoints require the `x-userId` header.

### Create a rain log

```bash
curl -X POST http://localhost:5000/api/data \
  -H "Content-Type: application/json" \
  -H "x-userId: user-123" \
  -d '{
    "rain": true,
    "latitude": 51.5074,
    "longitude": -0.1278
  }'
```

**Response (201 Created):**

```json
{
  "timestamp": "2025-10-18T12:34:56Z",
  "rain": true,
  "userIdentifier": "user-123",
  "latitude": 51.5074,
  "longitude": -0.1278
}
```

### Get rain logs for a user

```bash
curl -H "x-userId: user-123" http://localhost:5000/api/data
```

**Optional filter by rain status:**

```bash
curl -H "x-userId: user-123" "http://localhost:5000/api/data?rain=true"
```

**Response (200 OK):**

```json
[
  {
    "timestamp": "2025-10-18T12:34:56Z",
    "rain": true,
    "userIdentifier": "user-123",
    "latitude": 51.5074,
    "longitude": -0.1278
  }
]
```

### Error Responses

The API returns detailed validation errors when requests fail validation:

```bash
# Example: Missing x-userId header
curl -X POST http://localhost:5000/api/data \
  -H "Content-Type: application/json" \
  -d '{"rain": true}'
```

**Error Response (400 Bad Request):**
```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "x-userId": [
      "The userIdentifier field is required.",
      "The field must contain a non-empty, non-whitespace value."
    ]
  },
  "traceId": "00-aee3f44db62908e94ed36f931970eaa1-46313aea3590f79c-00"
}
```

---

## Configuration

The `docker-compose.yml` uses these defaults:

- **PostgreSQL:**
  - Database: `raindb`
  - User: `rainuser`
  - Password: `rainpass`
  - Port: `5432`

- **API:**
  - Host port: `5000` (mapped to container port `8080`)
  - Container port: `8080` (.NET 8 default)
  - Connection string: `Host=db;Database=raindb;Username=rainuser;Password=rainpass`
  - Built from local Dockerfile (includes EF Core tools for migrations)

**Note:** The `docker-compose.yml` file uses the modern format without the deprecated `version` field, compatible with Docker Compose v1.27+ and v2.x.

---

## Troubleshooting

### Migrations fail with connection errors

Wait a few seconds for PostgreSQL to fully start, then retry:

```bash
docker-compose exec api sh -c "cd /src && dotnet ef database update"
```

### Docker Desktop not starting (macOS/Windows)

Ensure Docker Desktop is running before executing any `docker-compose` commands. On macOS, check that Docker Desktop is running in the menu bar.

### Port already in use

Change the port in `docker-compose.yml`:

```yaml
ports:
  - "5001:8080"  # Use port 5001 instead
```

### View logs

```bash
docker-compose logs api
docker-compose logs db
```

### Reset everything

```bash
docker-compose down -v
docker-compose up --build
docker-compose exec api sh -c "cd /src && dotnet ef database update"
```

---

## Project Structure

```
RainTrackingApi/
├── RainTrackingApi/
│   ├── Controllers/          # API endpoints
│   ├── Services/             # Business logic
│   ├── Repositories/         # Data access layer
│   ├── Models/
│   │   ├── Domain/           # Database entities
│   │   └── DTO/              # API request/response models
│   ├── Data/                 # EF Core DbContext
│   ├── Migrations/           # Database migrations
│   ├── Swagger/              # Swagger examples & config
│   ├── Program.cs            # Application startup
│   ├── Dockerfile            # Docker build configuration
│   └── docker-compose.yml    # Docker services configuration
├── RainTrackingApi.Tests/    # Unit tests
└── README.md                 # This file
```

---

## License

MIT License - see repository for details.