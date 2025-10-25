# 🏨 Simple Booking App

[![.NET CI/CD Pipeline](https://github.com/kooogi/simple-booking-app/actions/workflows/dotnet.yml/badge.svg)](https://github.com/kooogi/simple-booking-app/actions/workflows/dotnet.yml)
[![.NET Version](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Tests](https://img.shields.io/badge/tests-13%20passing-success)](tests/TESTING_GUIDE.md)
[![License](https://img.shields.io/badge/license-Educational-blue)](LICENSE)

A professional console-based room reservation system built with C# and SQL Server, featuring clean architecture, comprehensive testing, and automated CI/CD pipeline. Demonstrates enterprise-level design patterns and best practices.

## 📋 Table of Contents

- [Key Highlights](#key-highlights)
- [Features](#features)
- [Technologies](#technologies)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Database Setup](#database-setup)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [Project Structure](#project-structure)
- [Usage](#usage)
- [Architecture](#architecture)
- [Testing](#testing)
- [CI/CD Pipeline](#cicd-pipeline)
- [Security](#security-features)
- [What I Learned](#what-i-learned)
- [Future Enhancements](#future-enhancements)

## 🌟 Key Highlights

- ✅ **Clean Architecture** - Repository Pattern, Dependency Injection, SOLID principles
- ✅ **Automated Testing** - 13 unit and integration tests with 100% pass rate
- ✅ **CI/CD Pipeline** - GitHub Actions with build, test, and code quality checks
- ✅ **Security First** - Parameterized SQL queries, transaction-based operations
- ✅ **Professional Documentation** - Comprehensive README and testing guide
- ✅ **Modern C#** - .NET 10.0 with nullable reference types and latest features

## ✨ Features

### Room Management

- ✅ Create new rooms with custom capacity and pricing
- ✅ Edit existing room details
- ✅ Delete rooms from the system
- ✅ View all rooms in the database
- ✅ Check room availability for specific dates

### Reservation Management

- ✅ Create reservations for available rooms
- ✅ Edit reservation dates
- ✅ Delete reservations
- ✅ View all reservations with room details

### User Experience

- 🎨 Interactive arrow-key navigation menu
- ✅ Transaction-based operations with user confirmation
- ⚠️ Error handling for database operations
- 🔒 SQL injection prevention with parameterized queries

## 🛠 Technologies

### Core Stack

- **Language:** C# / .NET 10.0
- **Database:** Microsoft SQL Server (LocalDB/Express/Full)
- **Architecture:** Repository Pattern, Dependency Injection, Layered Architecture

### NuGet Packages

- **Data Access:**
  - Microsoft.Data.SqlClient (6.1.2)
- **Configuration:**
  - Microsoft.Extensions.Configuration (9.0.9)
  - Microsoft.Extensions.Configuration.Binder (9.0.9)
  - Microsoft.Extensions.Configuration.Json (9.0.9)
- **Dependency Injection:**
  - Microsoft.Extensions.DependencyInjection (9.0.9)
- **Testing:**
  - xUnit (3.0.0)
  - Moq (4.20.72)
  - Microsoft.NET.Test.Sdk (17.14.1)

### Development Tools

- **.editorconfig** - Consistent code formatting (2-space indentation)
- **GitHub Actions** - Automated CI/CD pipeline
- **dotnet format** - Code style enforcement

## 📦 Prerequisites

Before running this application, ensure you have the following installed:

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) or higher
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or full version)
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) (optional, for database management)

## 🚀 Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/kooogi/simple-booking-app.git
   cd simple-booking-app
   ```

2. **Restore NuGet packages:**
   ```bash
   cd src/App
   dotnet restore
   ```

## 🗄️ Database Setup

### Step 1: Create the Database

Run the SQL scripts in order:

1. **Create the database:**

   ```bash
   # Using SQLCMD (Windows)
   sqlcmd -S localhost -i sql/create_database.sql

   # Or execute in SQL Server Management Studio
   ```

2. **Create the tables:**

   ```bash
   sqlcmd -S localhost -d SimpleBookingAppDB -i sql/create_table.sql
   ```

3. **Generate dummy data (optional):**
   ```bash
   sqlcmd -S localhost -d SimpleBookingAppDB -i sql/generate_dummy_data.sql
   ```

### Database Schema

**Rooms Table:**

```sql
CREATE TABLE Rooms (
    RoomId INT PRIMARY KEY IDENTITY(1,1),
    RoomNumber INT NOT NULL UNIQUE,
    Capacity INT NOT NULL,
    PricePerNight DECIMAL(18, 2) NOT NULL
);
```

**Reservations Table:**

```sql
CREATE TABLE Reservations (
    ReservationId INT PRIMARY KEY IDENTITY(1,1),
    RoomId INT,
    GuestsNumber INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    CONSTRAINT FK_Reservations_Rooms_Cascade
    FOREIGN KEY (RoomId)
    REFERENCES Rooms(RoomId)
    ON DELETE CASCADE
);
```

## ⚙️ Configuration

1. **Update the connection string:**

   Edit `src/App/appsettings.json`:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=SimpleBookingAppDB;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

2. **Connection string options:**

   - **Windows Authentication (SQL Server Express):**

     ```
     Server=localhost\SQLEXPRESS;Database=SimpleBookingAppDB;Trusted_Connection=True;TrustServerCertificate=True;
     ```

   - **Windows Authentication (Default Instance):**

     ```
     Server=localhost;Database=SimpleBookingAppDB;Trusted_Connection=True;TrustServerCertificate=True;
     ```

   - **SQL Server Authentication:**

     ```
     Server=localhost;Database=SimpleBookingAppDB;User Id=your_username;Password=your_password;TrustServerCertificate=True;
     ```

   - **LocalDB:**
     ```
     Server=(localdb)\\mssqllocaldb;Database=SimpleBookingAppDB;Trusted_Connection=True;
     ```

## 🎮 Running the Application

1. **Navigate to the project directory:**

   ```bash
   cd src/App
   ```

2. **Run the application:**

   ```bash
   dotnet run
   ```

3. **Navigate the menu:**
   - Use **Arrow Keys** (↑/↓) to navigate
   - Press **Enter** to select
   - Press **Escape** to go back

## 📁 Project Structure

```
simple-booking-app/
├── src/
│   └── App/
│       ├── Data/                      # Data Access Layer
│       │   ├── ReservationRepository.cs
│       │   └── RoomRepository.cs
│       ├── Models/                    # Data Models
│       │   ├── Reservation.cs
│       │   └── Room.cs
│       ├── Modules/                   # UI Modules
│       │   ├── MainModule.cs
│       │   ├── ReservationModule.cs
│       │   └── RoomModule.cs
│       ├── Services/                  # Business Logic Layer
│       │   ├── ReservationService.cs
│       │   └── RoomService.cs
│       ├── Utils/                     # Utility Classes
│       │   ├── ConfigProvider.cs
│       │   └── ConsoleMenu.cs
│       ├── Program.cs                 # Entry Point
│       ├── appsettings.json          # Configuration
│       └── App.csproj                # Project File
├── sql/                              # Database Scripts
│   ├── create_database.sql
│   ├── create_table.sql
│   └── generate_dummy_data.sql
├── tests/                            # Test Suite
│   ├── TESTING_GUIDE.md
│   └── BookingApp.Tests/
│       ├── DatabaseTestHelper.cs     # Integration test helper
│       ├── ModelTests.cs             # Model unit tests
│       ├── RoomServiceTests.cs       # Service unit tests
│       ├── ReservationServiceTests.cs # Service unit tests
│       ├── RoomRepositoryTests.cs    # Integration tests
│       ├── UnitTest1.cs              # Placeholder test
│       ├── appsettings.json
│       └── BookingApp.Tests.csproj
└── README.md
```

## 📖 Usage

### Main Menu

When you start the application, you'll see:

```
=== Main Menu ===

  > Reservation Management Module
    Room Management Module
    Exit
```

### Room Management

1. **Create Room:**

   - Enter room number, capacity, and price per night
   - Confirm the transaction (Y/N)

2. **Edit Room:**

   - Select room from the list
   - Provide new room details
   - Confirm the update (Y/N)

3. **Delete Room:**

   - Select the room to delete from the list
   - Confirm the deletion (Y/N)
   - Note: Deletes associated reservations (CASCADE)

4. **Check Available Rooms:**

   - Enter number of guests
   - Provide check-in and check-out dates (dd.MM.yyyy format)
   - View available rooms matching criteria

5. **List All Rooms:**
   - View all rooms with details

### Reservation Management

1. **Create Reservation:**

   - Check available rooms (enter guests, check-in, check-out dates)
   - Select room from available options
   - Confirm the reservation (Y/N)

2. **Edit Reservation:**

   - Select reservation from the list
   - Provide new check-in and check-out dates
   - Confirm the update (Y/N)

3. **Delete Reservation:**

   - Select reservation to delete from the list
   - Confirm the deletion (Y/N)

4. **List Reservations:**
   - View all existing reservations with room details

## 🏗️ Architecture

### Design Patterns

- **Repository Pattern:** Data access abstraction
- **Dependency Injection:** Service management
- **Modular Design:** Separation of concerns
- **Transaction Pattern:** Database consistency

### Layered Architecture

```
┌─────────────────────────────┐
│   Presentation Layer        │
│   (Modules - UI)            │
├─────────────────────────────┤
│   Business Logic Layer      │
│   (Services)                │
├─────────────────────────────┤
│   Data Access Layer         │
│   (Repositories)            │
├─────────────────────────────┤
│   Database                  │
│   (SQL Server)              │
└─────────────────────────────┘
```

### Key Components

- **ConfigProvider:** Manages dependency injection and configuration
- **ConsoleMenu:** Reusable interactive menu component
- **Repositories:** Handle all database operations with transactions
- **Services:** Contain business logic and user input handling
- **Modules:** Manage UI flow and user interactions

## 🔐 Security Features

- ✅ Parameterized SQL queries (prevents SQL injection)
- ✅ Transaction-based operations (ensures data consistency)
- ✅ User confirmation for destructive operations
- ✅ Error handling with rollback support
- ✅ Cascade delete for referential integrity

## 💡 What I Learned

Building this project taught me several valuable software engineering concepts:

### Architecture & Design

- **Repository Pattern** - Separating data access from business logic for maintainability
- **Dependency Injection** - Using IoC containers for loose coupling and testability
- **SOLID Principles** - Single responsibility, interface segregation, dependency inversion
- **Layered Architecture** - Proper separation of concerns across presentation, business, and data layers

### Development Practices

- **Unit Testing** - Writing testable code with mocking frameworks (Moq)
- **Integration Testing** - Testing database operations with real connections
- **CI/CD** - Automating build, test, and quality checks with GitHub Actions
- **Code Quality** - Using EditorConfig and dotnet format for consistent code style

### Technical Skills

- **ADO.NET** - Raw SQL execution with parameterized queries
- **Transaction Management** - Ensuring data consistency with SQL transactions
- **Configuration Management** - Using appsettings.json and IConfiguration
- **Error Handling** - Proper exception handling and user feedback

### Database

- **SQL Server** - Creating databases, tables, and relationships
- **Foreign Keys** - CASCADE delete constraints for referential integrity
- **Complex Queries** - Subqueries for availability checking with date overlaps

### Key Takeaways

- Clean architecture makes code easier to test and maintain
- Automated testing catches bugs early and enables confident refactoring
- CI/CD pipelines ensure code quality before it reaches production
- Security (parameterized queries) must be built in from the start, not added later

## 🧪 Testing

The project includes a comprehensive test suite with both unit and integration tests:

### Running Tests

```bash
cd tests/BookingApp.Tests
dotnet test
```

### Test Coverage

- **Unit Tests (10):** Model validation, service logic with mocking
- **Integration Tests (3):** Repository operations with real database
- **Total Tests:** 13 passing tests

### Test Structure

- **ModelTests:** Validates Room and Reservation model properties
- **RoomServiceTests:** Tests business logic with mocked dependencies
- **ReservationServiceTests:** Verifies reservation workflows
- **RoomRepositoryTests:** Integration tests with actual database operations
- **DatabaseTestHelper:** Utility class for integration test setup and cleanup

For detailed testing information, see [TESTING_GUIDE.md](tests/TESTING_GUIDE.md).

## 🚀 CI/CD Pipeline

This project includes a complete CI/CD pipeline using GitHub Actions.

### Pipeline Jobs

1. **Build** - Compiles the solution in Release mode with caching
2. **Test** - Runs all 13 tests with code coverage reporting
3. **Code Quality** - Performs 4 automated checks:
   - Code formatting validation (dotnet format)
   - Security vulnerability scanning
   - Code analysis (warnings as errors)
   - Outdated package detection
4. **PR Comments** - Automated test reports on pull requests
5. **Release** - Publishes artifacts on main branch merges

### Running the Pipeline

The pipeline automatically triggers on:

- Push to `main` branch
- Pull requests to `main` branch
- Manual workflow dispatch

### Local Quality Checks

Run the same checks locally:

```bash
# Format code
dotnet format simple-booking-app.sln

# Build
dotnet build simple-booking-app.sln --configuration Release

# Run tests
dotnet test tests/BookingApp.Tests/BookingApp.Tests.csproj
```

See [.github/workflows/README.md](.github/workflows/README.md) for detailed pipeline documentation.

## 🚀 Future Enhancements

### Planned Improvements

- [ ] **Async/Await** - Convert database operations to async for better performance
- [ ] **Entity Framework Core** - Replace ADO.NET with modern ORM
- [ ] **Logging** - Add ILogger for debugging and monitoring
- [ ] **API Layer** - Add RESTful API endpoints with ASP.NET Core
- [ ] **Docker Support** - Containerize application for easy deployment

### Feature Ideas

- [ ] User authentication and authorization
- [ ] Email notifications for reservations
- [ ] Payment processing integration
- [ ] Reporting and analytics dashboard
- [ ] Mobile/web frontend

### Quality Improvements

- [ ] Increase test coverage to 90%+
- [ ] Add mutation testing
- [ ] Performance benchmarking
- [ ] Code coverage badges

## 👤 Author

**kooogi**

- GitHub: [@kooogi](https://github.com/kooogi)
- Portfolio Project: Built to demonstrate professional software engineering skills

## � Acknowledgments

- Built with best practices from Microsoft's official .NET documentation
- Testing patterns inspired by industry standards
- CI/CD pipeline following GitHub Actions best practices

## �📝 License

This project is created for educational purposes.

## 🤝 Contributing

This is a portfolio project. Feedback and suggestions are welcome via issues or pull requests!

### How to Contribute

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/improvement`)
3. Make your changes with tests
4. Run code formatting: `dotnet format simple-booking-app.sln`
5. Ensure all tests pass: `dotnet test`
6. Commit your changes (`git commit -m 'Add improvement'`)
7. Push to the branch (`git push origin feature/improvement`)
8. Open a Pull Request

---

**Note:** This project was developed as part of an internship application portfolio to demonstrate proficiency in C#, SQL Server, software architecture, testing, and DevOps practices.

**Tech Stack:** .NET 10.0 | C# | SQL Server | xUnit | Moq | GitHub Actions | Repository Pattern | Dependency Injection
