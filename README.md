# 🏨 Simple Booking App

A console-based room reservation system built with C# and SQL Server, featuring an interactive menu interface and complete CRUD operations for rooms and reservations.

## 📋 Table of Contents

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

- **Language:** C# 10.0 / .NET 10.0
- **Database:** Microsoft SQL Server
- **Libraries:**
  - Microsoft.Data.SqlClient (6.1.2)
  - Microsoft.Extensions.Configuration (9.0.9)
  - Microsoft.Extensions.DependencyInjection (9.0.9)

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
    RoomId INT FOREIGN KEY REFERENCES Rooms(RoomId),
    GuestsNumber INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL
);
```

## ⚙️ Configuration

1. **Update the connection string:**

   Edit `src/App/appsettings.json`:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=SimpleBookingAppDB;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

2. **Connection string options:**

   - **Windows Authentication:**

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
├── tests/                            # Unit Tests (TODO)
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

   - Enter the current room number
   - Provide new room details
   - Confirm the update (Y/N)

3. **Delete Room:**

   - Enter the room number to delete
   - Confirm the deletion (Y/N)

4. **Check Available Rooms:**

   - Enter number of guests
   - Provide check-in and check-out dates (dd.MM.yyyy format)
   - View available rooms matching criteria

5. **List All Rooms:**
   - View all rooms with details

### Reservation Management

1. **Create Reservation:**

   - First, check available rooms
   - Enter the room number to reserve
   - Confirm the reservation (Y/N)

2. **Edit Reservation:**

   - Enter reservation ID
   - Provide new check-in and check-out dates
   - Confirm the update (Y/N)

3. **Delete Reservation:**

   - Enter reservation ID to delete
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

## 🐛 Known Issues

- Input validation for negative numbers is limited
- Date validation (end date before start date) needs improvement
- Stack overflow risk with recursive menu calls (consider while loop)

## 🚀 Future Enhancements

- [ ] Add comprehensive input validation
- [ ] Implement unit tests
- [ ] Add user authentication
- [ ] Generate reservation invoices
- [ ] Export data to CSV/PDF
- [ ] Add search and filter capabilities
- [ ] Implement logging system
- [ ] Add configuration for different environments

## 👤 Author

**kooogi**

- GitHub: [@kooogi](https://github.com/kooogi)

## 📝 License

This project is created for educational purposes.

## 🤝 Contributing

This is a portfolio project. Feedback and suggestions are welcome!

---

**Note:** This project was developed as part of an internship application portfolio to demonstrate C#, SQL Server, and software architecture skills.
