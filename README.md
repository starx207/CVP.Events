# CVP.Events

A Blazor Server web application for managing and displaying events with search, sorting, and pagination capabilities. Built with .NET Aspire for cloud-native development with Redis caching support.

## 🚀 Quick Start

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) or later
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for Redis container)
- A code editor (Visual Studio, VS Code, or Rider recommended)

### Running the Application with Aspire

1. **Clone the repository** (if not already done):

   ```powershell
   git clone https://github.com/starx207/CVP.Events.git
   cd CVP.Events
   ```

2. **Configure the API client secret**:

   ```powershell
   # Navigate to the main web project and set user secrets
   cd CVP.Events
   dotnet user-secrets set "EventsApi:ClientSecret" "the-client-secret"
   cd ..
   ```

3. **Run the application using the Aspire AppHost**:

   ```powershell
   # Navigate to the AppHost project and run
   cd CVP.Events.AppHost
   dotnet run
   ```

4. **Access the application**:
   - **Aspire Dashboard**: https://localhost:17169 (main dashboard for monitoring)
   - **Web Application**: Available through the Aspire dashboard
   - **Alternative HTTP**: http://localhost:15053

## 🛠️ Development

### Project Structure

```
CVP.Events/
├── CVP.Events/                 # Main Blazor Server web application
│   ├── Components/             # Blazor components and pages
│   │   ├── Pages/              # Page components (Home, CreateEvent)
│   │   └── Layout/             # Layout components (EventTable, PageSelect)
│   ├── Models/                 # View models and data models
│   ├── Services/               # Application services (CachedEventsApi)
│   ├── Validation/             # Custom validation attributes
│   └── wwwroot/                # Static files (CSS, JS, images)
├── CVP.Events.Api.Sdk/         # API SDK for external services (Refit)
├── CVP.Events.Contracts/       # Shared contracts and DTOs for the API
├── CVP.Events.AppHost/         # .NET Aspire AppHost for orchestration
├── CVP.Events.ServiceDefaults/ # Shared Aspire service configurations
└── CVP.Events.slnx             # Solution file
```

## 🌟 Features

- **Event Management**: Create and view events with titles, descriptions, and dates
- **Search & Filter**: Search events by title with real-time filtering
- **Sorting**: Sort events by various criteria (date, title, etc.)
- **Pagination**: Navigate through events with customizable page sizes
- **Interactive Components**: Blazor Server components with real-time updates
- **Redis Caching**: Improved performance with distributed caching
- **Cloud-Native Architecture**: Built with .NET Aspire for modern deployment scenarios
- **Observability**: Comprehensive telemetry and monitoring through Aspire Dashboard

## 🔧 Configuration

### Application Settings

Configuration files are located in the `CVP.Events` project:

- `appsettings.json` - Production settings
- `appsettings.Development.json` - Development-specific settings

### User Secrets

The project uses User Secrets for sensitive configuration. The API `clientSecret` will need to be added before running the project. To manage secrets:

```bash
# Set a secret
dotnet user-secrets set "EventsApi:ClientSecret" "the-client-secret"

# List all secrets
dotnet user-secrets list

# Remove a secret
dotnet user-secrets remove "EventsApi:ClientSecret"
```
---

_Built with ❤️ using Blazor Server, .NET 10, and .NET Aspire_
