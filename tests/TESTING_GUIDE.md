# 📚 Testing Guide for Simple Booking App

## 🎯 What Are Unit Tests?

**Unit tests** verify that individual pieces of code (units) work correctly in isolation.

### **Benefits:**

- ✅ Catch bugs early
- ✅ Document how code should behave
- ✅ Enable safe refactoring
- ✅ Improve code quality

---

## 🏗️ Test Structure (AAA Pattern)

Every test follows this pattern:

```csharp
[Fact]
public void MethodName_Scenario_ExpectedResult()
{
    // Arrange - Set up test data
    var input = 5;

    // Act - Call the method being tested
    var result = Calculator.Add(input, 3);

    // Assert - Verify the result
    Assert.Equal(8, result);
}
```

---

## 📦 What's Included

### **1. Test Project Setup**

```bash
cd tests
dotnet new xunit -n BookingApp.Tests
cd BookingApp.Tests
dotnet add reference ../../src/App/App.csproj
dotnet add package Moq
dotnet add package Microsoft.Extensions.Configuration
```

### **2. Test Files**

- `ModelTests.cs` - Unit tests for Room and Reservation models (4 tests)
- `RoomServiceTests.cs` - Unit tests for RoomService (2 tests)
- `ReservationServiceTests.cs` - Unit tests for ReservationService (1 test)
- `RoomRepositoryTests.cs` - **Integration tests** for RoomRepository (3 tests)
- `DatabaseTestHelper.cs` - Helper for integration tests with real database
- `UnitTest1.cs` - Placeholder test

**Total: 13 passing tests (10 unit + 3 integration)**

---

## 🧪 Test Examples Explained

### **Example 1: Simple Property Test (Unit Test)**

```csharp
[Fact]
public void Room_ShouldSetPropertiesCorrectly()
{
    var room = new Room
    {
        RoomId = 1,
        RoomNumber = "101",
        Capacity = 2,
        PricePerNight = 100.50m
    };

    Assert.Equal(1, room.RoomId);
    Assert.Equal("101", room.RoomNumber);
    Assert.Equal(2, room.Capacity);
    Assert.Equal(100.50m, room.PricePerNight);
}
```

**What it tests:** Creating a Room object sets all properties correctly.
**Type:** Unit test (no dependencies)

---

### **Example 2: Theory Test (Multiple Test Cases)**

```csharp
[Theory]
[InlineData(1, "Single occupancy")]
[InlineData(2, "Double occupancy")]
[InlineData(4, "Family room")]
public void Room_CapacityValues_ShouldBeValid(int capacity, string description)
{
    var room = new Room { Capacity = capacity };

    Assert.True(room.Capacity > 0, $"{description} should have positive capacity");
}
```

**What it tests:** Room capacity is always positive for different room types.
**Type:** Unit test with parameterized data
**Runs:** 3 times with different inputs.

---

### **Example 3: Mocking Dependencies (Unit Test)**

```csharp
[Fact]
public void ShowAllRooms_ShouldDisplayAllRooms_WhenRoomsExist()
{
    var mockRoomRepository = new Mock<IRoomRepository>();
    var testRooms = new List<Room>
    {
        new Room { RoomId = 1, RoomNumber = "101", Capacity = 2, PricePerNight = 100m }
    };

    mockRoomRepository.Setup(repo => repo.RoomList())
        .Returns(testRooms);

    var roomService = new RoomService(mockRoomRepository.Object);

    roomService.ShowAllRooms();

    mockRoomRepository.Verify(repo => repo.RoomList(), Times.Once);
}
```

**What it tests:** RoomService calls the repository to get rooms.
**Type:** Unit test with mocking
**Why mock?** We don't want to use a real database in unit tests!

---

### **Example 4: Integration Test (Real Database)**

```csharp
[Fact]
public void RoomList_ShouldReturnAllRooms()
{
    int testRoomNumber = 9999;
    _testRoomNumbers.Add(testRoomNumber);

    _dbHelper.ExecuteNonQuery(
        "INSERT INTO Rooms (RoomNumber, Capacity, PricePerNight) VALUES (@roomNumber, @capacity, @price)",
        cmd =>
        {
            cmd.Parameters.AddWithValue("@roomNumber", testRoomNumber);
            cmd.Parameters.AddWithValue("@capacity", 2);
            cmd.Parameters.AddWithValue("@price", 100.50m);
        }
    );

    var rooms = _repository.RoomList();

    var testRoom = rooms.FirstOrDefault(r => r.RoomNumber == testRoomNumber.ToString());
    Assert.NotNull(testRoom);
    Assert.Equal(2, testRoom.Capacity);
    Assert.Equal(100.50m, testRoom.PricePerNight);
}
```

**What it tests:** Repository can retrieve rooms from real database.
**Type:** Integration test (uses actual SQL Server)
**Why?** Verifies database queries work correctly.

---

## 🔍 Common Assert Methods

```csharp
// Equality
Assert.Equal(expected, actual);
Assert.NotEqual(expected, actual);

// Boolean
Assert.True(condition);
Assert.False(condition);

// Null checks
Assert.Null(value);
Assert.NotNull(value);

// Exceptions
var exception = Assert.Throws<ArgumentException>(() => method());
Assert.Contains("error message", exception.Message);

// Collections
Assert.Empty(collection);
Assert.NotEmpty(collection);
Assert.Contains(item, collection);
```

---

## 🚀 Running Tests

### **Run all tests:**

```bash
cd tests/BookingApp.Tests
dotnet test
```

### **Run with detailed output:**

```bash
dotnet test --logger "console;verbosity=detailed"
```

### **Run specific test:**

```bash
dotnet test --filter "FullyQualifiedName~RoomServiceTests"
```

### **Run and show coverage:**

```bash
dotnet test /p:CollectCoverage=true
```

---

## 📝 Test Naming Convention

```csharp
[Fact]
public void MethodName_Scenario_ExpectedResult()
```

**Examples:**

- `ShowAllRooms_ShouldDisplayAllRooms_WhenRoomsExist`
- `CreateReservation_ShouldThrowException_WhenRoomNotAvailable`
- `GetAvailableRooms_ShouldReturnEmptyList_WhenNoRoomsMatch`

---

## 🎓 What Each Test File Does

### **ModelTests.cs** (Unit Tests)

✅ Tests basic model properties
✅ Tests data validation rules
✅ Quick to run, no dependencies
✅ 4 tests (1 uses Theory with 3 data sets)

### **RoomServiceTests.cs** (Unit Tests)

✅ Tests business logic
✅ Uses mocks for dependencies
✅ Verifies method calls
✅ 2 tests with Moq framework

### **ReservationServiceTests.cs** (Unit Tests)

✅ Tests reservation logic
✅ Uses mocks for repository and room service
✅ Verifies interactions
✅ 1 test

### **RoomRepositoryTests.cs** (Integration Tests)

✅ Tests repository with **real database**
✅ Uses DatabaseTestHelper for setup/cleanup
✅ Tests availability queries and booking logic
✅ 3 integration tests
✅ Implements IDisposable for cleanup

### **DatabaseTestHelper.cs** (Test Utility)

✅ Reads connection string from appsettings.json
✅ Provides ExecuteNonQuery and ExecuteQuery methods
✅ Handles test data cleanup
✅ Used by integration tests

---

## 🔨 How to Add More Tests

### **1. Add a unit test:**

```csharp
[Fact]
public void YourMethod_Scenario_ExpectedResult()
{
    var service = new YourService();

    var result = service.YourMethod();

    Assert.NotNull(result);
}
```

### **2. Test with multiple inputs:**

```csharp
[Theory]
[InlineData(2, 100)]
[InlineData(4, 150)]
public void CalculatePrice_ShouldReturnCorrectPrice(int capacity, decimal expected)
{
    var room = new Room { Capacity = capacity };
    var price = room.CalculatePrice();
    Assert.Equal(expected, price);
}
```

### **3. Test for exceptions:**

```csharp
[Fact]
public void CreateRoom_ShouldThrowException_WhenCapacityIsNegative()
{
    var repository = new RoomRepository("connection");

    Assert.Throws<ArgumentException>(() =>
        repository.RoomRegistration(101, -1, 100)
    );
}
```

### **4. Add an integration test:**

```csharp
[Fact]
public void YourRepositoryMethod_ShouldWorkWithDatabase()
{
    var testId = 9999;
    _testIds.Add(testId);

    _dbHelper.ExecuteNonQuery(
        "INSERT INTO YourTable (Id, Name) VALUES (@id, @name)",
        cmd => {
            cmd.Parameters.AddWithValue("@id", testId);
            cmd.Parameters.AddWithValue("@name", "Test");
        }
    );

    var result = _repository.GetById(testId);

    Assert.NotNull(result);
    Assert.Equal("Test", result.Name);
}
```

---

## 💡 Best Practices

### ✅ **DO:**

- Write tests for business logic
- Use descriptive test names
- Test one thing per test
- Keep tests simple and readable
- Use mocks for external dependencies (unit tests)
- Clean up test data (integration tests)
- Use IDisposable for cleanup in integration tests

### ❌ **DON'T:**

- Test private methods directly
- Write tests that depend on each other
- Use real databases in **unit tests** (use integration tests instead)
- Test framework code (e.g., Entity Framework internals)
- Write tests that take a long time
- Leave test data in database after integration tests

---

## 🎯 What to Test in Your App

### **Priority 1 (Easy & Important):**

✅ Model property setters _(Done - ModelTests.cs)_
✅ Business logic validation _(Done - ModelTests.cs)_
✅ Date range calculations _(Done - ModelTests.cs)_

### **Priority 2 (Medium):**

✅ Service methods calling repositories _(Done - RoomServiceTests.cs, ReservationServiceTests.cs)_
✅ Data transformation logic
✅ Error handling

### **Priority 3 (Advanced):**

✅ Repository methods _(Done - RoomRepositoryTests.cs with real DB)_
⬜ Transaction rollback scenarios
⬜ Regex pattern matching
⬜ More CRUD integration tests (Create, Update, Delete)

---

## 📊 Test Coverage

### **Current Status:**

- **Total Tests:** 13 passing
- **Unit Tests:** 10 (77%)
- **Integration Tests:** 3 (23%)

### **Coverage Goal for Internship:**

- **40-60%** = Good
- **60-80%** = Very Good
- **80%+** = Excellent

**Focus on:** Business logic, not Console.WriteLine or database calls.

### **Test Types:**

| Type            | What                          | Examples                 | Speed     |
| --------------- | ----------------------------- | ------------------------ | --------- |
| **Unit**        | Test isolated code with mocks | ModelTests, ServiceTests | ⚡ Fast   |
| **Integration** | Test with real database       | RoomRepositoryTests      | 🐢 Slower |

---

## 🔧 Troubleshooting

### **Tests don't compile?**

```bash
dotnet restore
dotnet build
```

### **Can't find test classes?**

Make sure namespace matches:

```csharp
namespace BookingApp.Tests;
```

### **Moq not working?**

```bash
dotnet add package Moq --version 4.20.72
```

---

## 📚 Learn More

- **xUnit Docs:** https://xunit.net/
- **Moq Docs:** https://github.com/moq/moq4
- **Unit Testing Best Practices:** https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices

---

## ✅ Quick Start

1. **Navigate to test folder:**

   ```bash
   cd tests/BookingApp.Tests
   ```

2. **Run all tests:**

   ```bash
   dotnet test
   ```

3. **See output:**

   ```
   Passed!  - Failed:     0, Passed:    13, Skipped:     0, Total:    13
   ```

4. **Run with verbose output:**

   ```bash
   dotnet test --logger "console;verbosity=detailed"
   ```

5. **Integration tests require:**
   - SQL Server running (localhost\SQLEXPRESS)
   - SimpleBookingAppDB database created
   - Connection string in appsettings.json

---

## 🆕 What's Different from Standard Guides

This project demonstrates **real-world testing**:

1. ✅ **No comments in code** - Tests are self-documenting with clear names
2. ✅ **Mix of unit and integration tests** - Shows both approaches
3. ✅ **Real database integration** - Not just mocks
4. ✅ **Proper cleanup** - IDisposable pattern for test data
5. ✅ **DatabaseTestHelper** - Reusable test utilities
6. ✅ **Theory tests** - Parameterized testing with InlineData

## 📚 Learn More

- **xUnit Docs:** https://xunit.net/
- **Moq Docs:** https://github.com/moq/moq4
- **Unit Testing Best Practices:** https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
- **Integration Testing:** https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests

---

**Happy Testing! 🧪**
