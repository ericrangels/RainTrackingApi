# Rain Tracking API

## Overview
Tracks daily rain data per user.

## Running Locally

1. Clone repo.
2. Run `docker-compose up --build`.
3. API available at `http://localhost:5000/api/data`.

## Endpoints

- **GET /api/data**  
  Requires `x-userId` header. Returns user's rain data.

- **POST /api/data**  
  Requires `x-userId` header. Body: `{ "rain": true|false }`.

## API Documentation

Visit `/swagger` for OpenAPI docs.

## Testing

Use tools like Postman or curl:
