using System.Data;
using Microsoft.Data.SqlClient;

public interface IRoomRepository
{
  List<Room> GetAvailableRooms(DateTime startDate, DateTime endDate);
}

public class RoomRepository : IRoomRepository
{
  private readonly string _connectionString;
  public RoomRepository(string connectionString)
  {
    _connectionString = connectionString;
  }

  public List<Room> GetAvailableRooms(DateTime startDate, DateTime endDate)
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
                    WHERE StartDate < @endDate AND EndDate > @startDate
                )";
      using var command = new SqlCommand(query, connection);
      command.Parameters.AddWithValue("@startDate", startDate);
      command.Parameters.AddWithValue("@endDate", endDate);
      using var reader = command.ExecuteReader();
      while (reader.Read())
      {
        var room = new Room
        {
          RoomId = reader.GetInt32(reader.GetOrdinal("RoomID")),
          RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber")),
          Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
        };
        availableRooms.Add(room);
      }
    }
    return availableRooms;
  }
}