# LearningPlatform

## Installation Instructions

1. **Clone the repository:** git clone <repository-url>

3. **Set up User Secrets (for sensitive configuration):**
   - The project uses [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) to store sensitive data such as the OpenAI API key.
   - From the `BL` project directory, run:
     ```sh
     dotnet user-secrets set "OpenAISettings__ApiKey" "<your-openai-api-key>" --project "BL/BL.csproj"
     ```
   - Do **not** commit your API key or secrets to source control.

4. **Configure the database:**
   - Ensure the database file exists at the path specified in `appsettings.json` under `DefaultConnection`.
   - If needed, update the connection string or create a new database.

5. **Run the project:**

---

## Technologies Used

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 9
- SQL Server (LocalDB)
- OpenAI API
- Microsoft.Extensions.Configuration.UserSecrets
- System.Text.Json

---

## Assumptions

- The developer has .NET 8 SDK or later installed.
- The developer has access to SQL Server Express/LocalDB.
- The OpenAI API key is provided via User Secrets and not stored in any configuration file.
- The database file exists at the specified path or will be created by the developer.

---

## How to Run Locally

1. Set up all required secrets (especially `OpenAISettings__ApiKey`).
2. Ensure the database is accessible and the connection string is correct.
3. Run the project:
dotnet run --project LerningPlatform/LerningPlatform.csproj
4. The API will be available at : https://localhost:5001
(or as configured in the project)

