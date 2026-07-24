# ⚡ WyrdStack

> Self-Hosted Homelab Operations Platform & Control Plane

WyrdStack provides a unified, cross‑platform dashboard for viewing system information, managing resources, and organizing operational tasks.

---

## 🏗 Architecture & Tech Stack

| Component | Stack / Tools | Role |
| :--- | :--- | :--- |
| **Backend API** | ASP.NET Core Web API | Gateway & Telemetry Aggregator |
| **Native Client** | .NET MAUI | Cross-Platform App (Windows, Android, iOS) |
| **Web Client** | Blazor | Browser-Based Web Portal |
| **Data Contracts** | C# Class Library (`.Shared`) | Shared Request/Response DTOs |
| **Database** | PostgreSQL + EF Core | Persistent Data Store & Migrations |
| **Authentication** | ASP.NET Core Identity | Token Auth (`MapIdentityApi`) |
| **Containerization** | Docker / Docker Compose | Infrastructure Orchestration |

---

## 📂 Planned Project Structure

```text
WyrdStack/
├── src/
│   ├── WyrdStack.Api/         # REST API & Gateway
│   ├── WyrdStack.Maui/        # .NET MAUI Native Client
│   ├── WyrdStack.Blazor/      # Blazor Web Client
├── docker-compose.yml         # Container Orchestration
└── README.md
