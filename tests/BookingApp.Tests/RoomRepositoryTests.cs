using Xunit;

namespace BookingApp.Tests;

public class RoomRepositoryTests : IDisposable
{
    private readonly DatabaseTestHelper _dbHelper;
    private readonly RoomRepository _repository;
    private readonly List<int> _testRoomNumbers = new();

    public RoomRepositoryTests()
    {
        _dbHelper = new DatabaseTestHelper();
        _repository = new RoomRepository(_dbHelper.ConnectionString);
    }

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

    [Fact]
    public void GetAvailableRooms_ShouldReturnRoomsMatchingCapacity()
    {
        int room1 = 9991;
        int room2 = 9992;
        int room3 = 9993;
        _testRoomNumbers.AddRange(new[] { room1, room2, room3 });

        _dbHelper.ExecuteNonQuery(@"
            INSERT INTO Rooms (RoomNumber, Capacity, PricePerNight) VALUES (@room1, 2, 100);
            INSERT INTO Rooms (RoomNumber, Capacity, PricePerNight) VALUES (@room2, 4, 150);
            INSERT INTO Rooms (RoomNumber, Capacity, PricePerNight) VALUES (@room3, 6, 200);",
            cmd =>
            {
                cmd.Parameters.AddWithValue("@room1", room1);
                cmd.Parameters.AddWithValue("@room2", room2);
                cmd.Parameters.AddWithValue("@room3", room3);
            }
        );

        var startDate = DateTime.Today.AddDays(10);
        var endDate = DateTime.Today.AddDays(12);
        var availableRooms = _repository.GetAvailableRooms(3, startDate, endDate);

        var roomNumbers = availableRooms.Select(r => int.Parse(r.RoomNumber)).ToList();
        Assert.Contains(room2, roomNumbers);
        Assert.DoesNotContain(room1, roomNumbers);
        Assert.DoesNotContain(room3, roomNumbers);
    }

    [Fact]
    public void GetAvailableRooms_ShouldExcludeBookedRooms()
    {
        int testRoom = 9994;
        _testRoomNumbers.Add(testRoom);

        _dbHelper.ExecuteNonQuery(
            "INSERT INTO Rooms (RoomNumber, Capacity, PricePerNight) VALUES (@roomNumber, 2, 100)",
            cmd => cmd.Parameters.AddWithValue("@roomNumber", testRoom)
        );

        var roomId = _dbHelper.ExecuteQuery(
            "SELECT RoomId FROM Rooms WHERE RoomNumber = @roomNumber",
            reader => reader.GetInt32(0),
            cmd => cmd.Parameters.AddWithValue("@roomNumber", testRoom)
        ).First();

        var bookedStart = new DateTime(2026, 1, 10);
        var bookedEnd = new DateTime(2026, 1, 15);
        
        _dbHelper.ExecuteNonQuery(@"
            INSERT INTO Reservations (RoomId, GuestsNumber, StartDate, EndDate) 
            VALUES (@roomId, 2, @start, @end)",
            cmd =>
            {
                cmd.Parameters.AddWithValue("@roomId", roomId);
                cmd.Parameters.AddWithValue("@start", bookedStart);
                cmd.Parameters.AddWithValue("@end", bookedEnd);
            }
        );

        var overlappingRooms = _repository.GetAvailableRooms(2, new DateTime(2026, 1, 12), new DateTime(2026, 1, 14));
        
        var roomNumbers = overlappingRooms.Select(r => int.Parse(r.RoomNumber)).ToList();
        Assert.DoesNotContain(testRoom, roomNumbers);

        var availableAfter = _repository.GetAvailableRooms(2, new DateTime(2026, 1, 20), new DateTime(2026, 1, 22));
        
        var availableNumbers = availableAfter.Select(r => int.Parse(r.RoomNumber)).ToList();
        Assert.Contains(testRoom, availableNumbers);
    }

    public void Dispose()
    {
        foreach (var roomNumber in _testRoomNumbers)
        {
            _dbHelper.CleanupTestRoom(roomNumber);
        }
    }
}
