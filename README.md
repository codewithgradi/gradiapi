# GradiApi

GradiApi is a .NET 10 Web API for managing personal profile information, experience history, and projects. It uses ASP.NET Core, Entity Framework Core, and PostgreSQL.

## Features

- Personal profile create, read, and update
- Experience list retrieval, insert, and update
- Project list retrieval, insert, and update
- Entity Framework Core with PostgreSQL
- OpenAPI/Swagger in development
- Global exception handling with Problem Details
- CORS policy for frontend integration

## Architecture

Client → API → Services → Repositories → PostgreSQL

```text
[ Client ]
    |
    v
[ GradiApi Web API ]
    |
    v
[ Service Layer ]
    |
    v
[ Repository Layer ]
    |
    v
[ PostgreSQL Database ]
```

### Data shape overview

```text
Personal
  ├── Projects[]
  ├── Experiences[]
  └── Socials
```

## Technologies

- .NET 10 (`net10.0`)
- ASP.NET Core Web API
- Entity Framework Core
- `Npgsql.EntityFrameworkCore.PostgreSQL`
- `DotNetEnv`
- `EFCore.NamingConventions`
- `Riok.Mapperly`

## Getting Started

### Prerequisites

- .NET 10 SDK
- PostgreSQL database
- Git (optional)

### Configuration

Update `appsettings.json` with your connection strings and frontend URLs.

Example:

```json
{
  "ConnectionStrings": {
    "Dev": "Host=localhost;Database=gradiapi_dev;Username=postgres;Password=yourpassword",
    "Prod": "Host=your-prod-host;Database=gradiapi_prod;Username=postgres;Password=yourpassword"
  },
  "OtherSettings": {
    "FrontendUrl": "https://localhost:3000",
    "FrontendUrlProd": "https://your-frontend.example.com",
    "BackendLiveApiLink": "https://your-api.example.com"
  },
  "Env": "dev"
}
```

The app reads `Env` and chooses the connection string:

- `dev` → `ConnectionStrings:Dev`
- `prod` → `ConnectionStrings:Prod`

### Running Locally

From the project root:

```powershell
cd c:\Users\User\Desktop\gradiapi
dotnet run
```

The app will start on the configured ASP.NET Core URLs. In development mode, OpenAPI is exposed automatically.

### Docker

Build and run with Docker:

```powershell
docker build -t gradiapi .
docker run -e Env=dev -p 5000:80 gradiapi
```

## API Endpoints

### Personal Profile

- `GET /api/personal/{id}` - get a personal profile by id
- `POST /api/personal` - create a new personal profile
- `PUT /api/personal/{id}` - update a personal profile by id

#### Request body example

```json
{
  "firstName": "Jane",
  "lastName": "Doe",
  "role": "Full Stack Developer",
  "location": "Berlin, Germany",
  "image": "https://example.com/profile.jpg",
  "bio": "Software developer focused on modern web experiences.",
  "hobbies": ["Cycling", "Photography"],
  "skills": ["API Design", "Testing", "CI/CD"],
  "programmingLanguages": ["C#", "TypeScript", "SQL"],
  "techStack": ["ASP.NET Core", "PostgreSQL", "React"],
  "socials": [
    {
      "link": "https://github.com/janedoe",
      "name": "GitHub"
    },
    {
      "link": "https://linkedin.com/in/janedoe",
      "name": "LinkedIn"
    }
  ]
}
```

#### Response example

```json
{
  "id": 1,
  "firstName": "Jane",
  "lastName": "Doe",
  "role": "Full Stack Developer",
  "location": "Berlin, Germany",
  "image": "https://example.com/profile.jpg",
  "bio": "Software developer focused on modern web experiences.",
  "hobbies": ["Cycling", "Photography"],
  "skills": ["API Design", "Testing", "CI/CD"],
  "programmingLanguages": ["C#", "TypeScript", "SQL"],
  "techStack": ["ASP.NET Core", "PostgreSQL", "React"],
  "socials": [
    {
      "link": "https://github.com/janedoe",
      "name": "GitHub"
    }
  ],
  "projects": [],
  "experiences": []
}
```

### Experience

- `GET /api/experience` - get all experience entries
- `POST /api/experience/{id}` - add a new experience entry for the profile with the specified id
- `PUT /api/experience/{id}` - update an experience entry by id

#### Request body example

```json
{
  "fromYear": 2021,
  "toYear": 2024,
  "company": "Acme Corp",
  "role": "Senior Backend Engineer",
  "currentlyHere": false
}
```

#### Response example

```json
{
  "id": 1,
  "fromYear": 2021,
  "toYear": 2024,
  "company": "Acme Corp",
  "role": "Senior Backend Engineer",
  "currentlyHere": false,
  "updatedAt": "2026-07-19T12:34:56Z"
}
```

### Project

- `GET /api/project` - get all projects
- `POST /api/project/{id}` - add a new project entry for the profile with the specified id
- `PUT /api/project/{id}` - update a project entry by id

#### Request body example

```json
{
  "title": "Portfolio Website",
  "problem": "Need a clean way to show developer work and skills.",
  "solution": "Built a responsive portfolio with ASP.NET Core and React.",
  "gitHub": "https://github.com/janedoe/portfolio",
  "liveDemo": "https://portfolio.example.com",
  "tools": ["ASP.NET Core", "Entity Framework Core", "TypeScript", "Tailwind CSS"]
}
```

#### Response example

```json
{
  "id": 1,
  "title": "Portfolio Website",
  "problem": "Need a clean way to show developer work and skills.",
  "solution": "Built a responsive portfolio with ASP.NET Core and React.",
  "gitHub": "https://github.com/janedoe/portfolio",
  "liveDemo": "https://portfolio.example.com",
  "tools": ["ASP.NET Core", "Entity Framework Core", "TypeScript", "Tailwind CSS"],
  "updatedAt": "2026-07-19T12:34:56Z"
}
```

## Data Model Summary

### Personal

- `Id` - integer
- `FirstName` - string
- `LastName` - string
- `Role` - string
- `Location` - string
- `Image` - string
- `Bio` - string
- `Hobbies` - string[]
- `Skills` - string[]
- `ProgrammingLanguages` - string[]
- `TechStack` - string[]
- `Socials` - list of objects
- `Projects` - list of project objects
- `Experiences` - list of experience objects

### Experience

- `Id` - integer
- `FromYear` - integer
- `ToYear` - integer
- `Company` - string
- `Role` - string
- `CurrentlyHere` - boolean
- `UpdatedAt` - datetime

### Project

- `Id` - integer
- `Title` - string
- `Problem` - string
- `Solution` - string
- `GitHub` - string
- `LiveDemo` - string
- `Tools` - string[]
- `UpdatedAt` - datetime

## Error Handling

The API uses global exception handling and returns standardized problem details for:

- `400 Bad Request`
- `401 Unauthorized`
- `404 Not Found`
- `409 Conflict`

## Notes

- `DotNetEnv` loads environment variables from local `.env` files if present.
- CORS is configured using frontend URLs defined in `OtherSettings`.
- EF Core uses snake_case naming for database objects.
- OpenAPI is enabled when running in Development mode.

---

