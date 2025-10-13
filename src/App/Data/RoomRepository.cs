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
      const string query = "SELECT Room.Id, Room.Number, Room.Capacity, Booking.IsAvailable, Booking.StartDate, Booking.EndDate FROM Room INNER JOIN Booking ON Room.Id=Booking.RoomId;";
      using var command = new SqlCommand(query, connection);

      using var reader = command.ExecuteReader();
      while (reader.Read())
      {
        var room = new Room
        {
          Id = reader.GetInt32(reader.GetOrdinal("RoomID")),
          Number = reader.GetString(reader.GetOrdinal("RoomNumber")),
          Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
        };
        availableRooms.Add(room);
      }
    }
    return availableRooms;
  }
}