# CVP.Events

A Blazor Server web application for managing and displaying events with search, sorting, and pagination capabilities.

## 🚀 Quick Start

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) or later
- A code editor (Visual Studio, VS Code, or Rider recommended)

### Running the Application

1. **Clone the repository** (if not already done):

   ```bash
   git clone https://github.com/starx207/CVP.Events.git
   cd CVP.Events
   ```

2. **Configure the API client secret**:

   ```bash
   # Add "EventsApi:ClientSecret" to either user secrets (recommended), or appsettings.json
   dotnet user-secrets set "EventsApi:ClientSecret" "the-client-secret"
   ```

3. **Build and run the application**:

   ```bash
   # Navigate to the main project directory
   cd CVP.Events

   # Restore dependencies and run
   dotnet run
   ```

4. **Access the application**:
   - HTTP: http://localhost:5282

## 🛠️ Development

### Project Structure

```
CVP.Events/
├── CVP.Events/               # Main Blazor Server web application
│   ├── Components/           # Blazor components and pages
│   │   ├── Pages/            # Page components (Home, CreateEvent)
│   │   └── Layout/           # Layout components (EventTable, PageSelect)
│   ├── Models/               # View models and data models
│   ├── Validation/           # Custom validation attributes
│   └── wwwroot/              # Static files (CSS, JS, images)
├── CVP.Events.Api.Sdk/       # API SDK for external services (Refit)
├── CVP.Events.Contracts/     # Shared contracts and DTOs for the API
└── CVP.Events.slnx           # Solution file
```

## 🌟 Features

- **Event Management**: Create and view events with titles, descriptions, and dates
- **Search & Filter**: Search events by title with real-time filtering
- **Sorting**: Sort events by various criteria (date, title, etc.)
- **Pagination**: Navigate through events with customizable page sizes
- **Interactive Components**: Blazor Server components with real-time updates

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

_Built with ❤️ using Blazor Server and .NET 10_
