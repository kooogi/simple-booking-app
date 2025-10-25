using Microsoft.Data.SqlClient;

public interface IRoomRepository
{
  List<Room> GetAvailableRooms(int questsNumber, DateTime startDate, DateTime endDate);
  void RoomRegistration(int roomNumber, int roomCapacity, int roomPrice);
  void RoomUpdate(int oldRoomNumber, int newRoomNumber, int roomCapacity, int roomPrice);
  void RoomRemoval(int roomId);
  List<Room> RoomList();
}

public class RoomRepository : IRoomRepository
{
  private readonly string _connectionString;
  public RoomRepository(string connectionString)
  {
    _connectionString = connectionString;
  }

  public List<Room> GetAvailableRooms(int questsNumber, DateTime startDate, DateTime endDate)
  {
    var availableRooms = new List<Room>();
    using (var connection = new SqlConnection(_connectionString))
    {
      connection.Open();
      const string query = @"
                SELECT *
                FROM Rooms
                WHERE RoomId NOT IN (
                    SELECT RoomId
                    FROM Reservations
                    WHERE (StartDate < @endDate AND EndDate > @startDate))
                    AND (Capacity >= @questsNumber AND Capacity <= @questsNumber+2)";
      using var command = new SqlCommand(query, connection);
      command.Parameters.AddWithValue("@questsNumber", questsNumber);
      command.Parameters.AddWithValue("@startDate", startDate);
      command.Parameters.AddWithValue("@endDate", endDate);
      using var reader = command.ExecuteReader();
      while (reader.Read())
      {
        var room = new Room
        {
          RoomId = reader.GetInt32(reader.GetOrdinal("RoomID")),
          RoomNumber = reader.GetInt32(reader.GetOrdinal("RoomNumber")).ToString(),
          Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
          PricePerNight = reader.GetDecimal(reader.GetOrdinal("PricePerNight"))
        };
        availableRooms.Add(room);
      }
    }
    return availableRooms;
  }
  public void RoomRegistration(int roomNumber, int roomCapacity, int roomPrice)
  {
    using (var connection = new SqlConnection(_connectionString))
    {
      const string query = "INSERT INTO Rooms (RoomNumber, Capacity, PricePerNight) VALUES (@roomNumber, @roomCapacity, @roomPrice)";

      connection.Open();
      SqlTransaction transaction = connection.BeginTransaction();

      try
      {
        using var command = new SqlCommand(query, connection, transaction);
        command.Parameters.AddWithValue("@roomNumber", roomNumber);
        command.Parameters.AddWithValue("@roomCapacity", roomCapacity);
        command.Parameters.AddWithValue("@roomPrice", roomPrice);
        command.ExecuteNonQuery();

        Console.WriteLine("Do You want to create room: " + roomNumber + " (Y/N)");
        string? confirm = Console.ReadLine();

        if (confirm?.ToUpper() == "Y")
        {
          transaction.Commit();
          Console.WriteLine("Transaction confirmed - room " + roomNumber + " created");
        }
        else
        {
          transaction.Rollback();
          Console.WriteLine("Transaction canceled");
        }
      }
      catch (Exception ex)
      {
        transaction.Rollback();
        Console.WriteLine("Error: " + ex.Message);
      }
    }
  }

  public void RoomUpdate(int oldRoomNumber, int newRoomNumber, int roomCapacity, int roomPrice)
  {
    using (var connection = new SqlConnection(_connectionString))
    {
      const string query = "UPDATE Rooms SET RoomNumber = @newRoomNumber, Capacity = @roomCapacity, PricePerNight = @roomPrice WHERE RoomNumber = @oldRoomNumber;";

      connection.Open();
      SqlTransaction transaction = connection.BeginTransaction();

      try
      {
        using var command = new SqlCommand(query, connection, transaction);
        command.Parameters.AddWithValue("@oldRoomNumber", oldRoomNumber);
        command.Parameters.AddWithValue("@newRoomNumber", newRoomNumber);
        command.Parameters.AddWithValue("@roomCapacity", roomCapacity);
        command.Parameters.AddWithValue("@roomPrice", roomPrice);
        command.ExecuteNonQuery();

        Console.WriteLine("Do You want to update room: " + oldRoomNumber + " (Y/N)");
        string? confirm = Console.ReadLine();

        if (confirm?.ToUpper() == "Y")
        {
          transaction.Commit();
          Console.WriteLine("Transaction confirmed - room " + oldRoomNumber + " updated");
        }
        else
        {
          transaction.Rollback();
          Console.WriteLine("Transaction canceled");
        }

      }
      catch (Exception ex)
      {
        transaction.Rollback();
        Console.WriteLine("Error: " + ex.Message);
      }
    }
  }
  public void RoomRemoval(int roomId)
  {
    using (var connection = new SqlConnection(_connectionString))
    {
      const string query = "DELETE FROM Rooms WHERE RoomId=@roomId";

      connection.Open();
      SqlTransaction transaction = connection.BeginTransaction();
      try
      {
        using var command = new SqlCommand(query, connection, transaction);
        command.Parameters.AddWithValue("@roomId", roomId);
        command.ExecuteNonQuery();

        Console.WriteLine("Do You want to delete room: " + roomId + " (Y/N)");
        string? confirm = Console.ReadLine();

        if (confirm?.ToUpper() == "Y")
        {
          transaction.Commit();
          Console.WriteLine("Transaction confirmed - room " + roomId + " deleted");
        }
        else
        {
          transaction.Rollback();
          Console.WriteLine("Transaction canceled");
        }
      }
      catch (Exception ex)
      {
        transaction.Rollback();
        Console.WriteLine("Error: " + ex.Message);
      }
    }
  }
  public List<Room> RoomList()
  {
    var allRooms = new List<Room>();
    using (var connection = new SqlConnection(_connectionString))
    {
      connection.Open();
      const string query = @"SELECT RoomId, RoomNumber, Capacity, PricePerNight FROM Rooms";

      using var command = new SqlCommand(query, connection);

      using var reader = command.ExecuteReader();
      while (reader.Read())
      {
        var room = new Room
        {
          RoomId = reader.GetInt32(reader.GetOrdinal("RoomId")),
          RoomNumber = reader.GetInt32(reader.GetOrdinal("RoomNumber")).ToString(),
          Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
          PricePerNight = reader.GetDecimal(reader.GetOrdinal("PricePerNight"))
        };

        allRooms.Add(room);
      }
    }
    return allRooms;
  }
}