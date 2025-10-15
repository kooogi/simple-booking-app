using Microsoft.Data.SqlClient;
public interface IReservationRepository
{
  bool CreateReservation(int roomNumber, int guestsNumber, DateTime startDate, DateTime endDate);
  List<(Reservation, Room)> reservationsHistory();
  void ReservationRemoval(int reservationId);
  void ReservationUpdate(int reservationId, DateTime startDate, DateTime endDate);
}

public class ReservationRepository : IReservationRepository
{
  private readonly string _connectionString;
  public ReservationRepository(string connectionString)
  {
    _connectionString = connectionString;
  }
  public bool CreateReservation(int roomNumber, int guestsNumber, DateTime startDate, DateTime endDate)
  {
    using (var connection = new SqlConnection(_connectionString))
    {
      connection.Open();
      const string query = @"INSERT INTO Reservations (RoomId, GuestsNumber, StartDate, EndDate) 
        VALUES (
            (SELECT RoomId FROM Rooms WHERE RoomNumber = @roomNumber),  
            @guestsNumber, 
            @startDate, 
            @endDate
        );";

      using var command = new SqlCommand(query, connection);
      command.Parameters.AddWithValue("@roomNumber", roomNumber);
      command.Parameters.AddWithValue("@guestsNumber", guestsNumber);
      command.Parameters.AddWithValue("@startDate", startDate);
      command.Parameters.AddWithValue("@endDate", endDate);

      int rowsAffected = command.ExecuteNonQuery();
      if (rowsAffected > 0)
      {
        Console.WriteLine("Reservation created successfully");
        return true;
      }
      else
      {
        Console.WriteLine("Failed to create reservation");
        return false;
      }
    }
  }
  public List<(Reservation, Room)> reservationsHistory()
  {
    var allReservation = new List<(Reservation, Room)>();
    using (var connection = new SqlConnection(_connectionString))
    {
      connection.Open();
      const string query = @"SELECT Reservations.StartDate, Reservations.EndDate, Rooms.RoomNumber, Reservations.GuestsNumber, Rooms.Capacity, Rooms.PricePerNight FROM Reservations JOIN Rooms ON Reservations.RoomId = Rooms.RoomId";

      using var command = new SqlCommand(query, connection);

      using var reader = command.ExecuteReader();
      while (reader.Read())
      {
        var reservation = new Reservation
        {
          StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
          EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
          GuestsNumber = reader.GetInt32(reader.GetOrdinal("GuestsNumber"))
        };

        var room = new Room
        {
          RoomNumber = reader.GetInt32(reader.GetOrdinal("RoomNumber")).ToString(),
          Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
          PricePerNight = reader.GetDecimal(reader.GetOrdinal("PricePerNight"))
        };

        allReservation.Add((reservation, room));
      }
    }
    return allReservation;
  }
  public void ReservationRemoval(int reservationId)
  {
    using (var connection = new SqlConnection(_connectionString))
    {
      connection.Open();
      const string query = "DELETE FROM Reservations WHERE ReservationId=@reservationId";

      using var command = new SqlCommand(query, connection);
      command.Parameters.AddWithValue("@reservationId", reservationId);


      int rowsAffected = command.ExecuteNonQuery();
      if (rowsAffected > 0)
      {
        Console.WriteLine("Reservation deleted successfully");
      }
      else
      {
        Console.WriteLine("Failed to delete reservation");
      }
    }
  }
  public void ReservationUpdate(int reservationId, DateTime startDate, DateTime endDate)
  {
    using (var connection = new SqlConnection(_connectionString))
    {
      connection.Open();
      const string query = "UPDATE Reservations SET StartDate = @startDate, EndDate = @endDate WHERE ReservationId = @reservationId;";

      using var command = new SqlCommand(query, connection);
      command.Parameters.AddWithValue("@reservationId", reservationId);
      command.Parameters.AddWithValue("@startDate", startDate);
      command.Parameters.AddWithValue("@endDate", endDate);

      int rowsAffected = command.ExecuteNonQuery();
      if (rowsAffected > 0)
      {
        Console.WriteLine("Reservation updated successfully");
      }
      else
      {
        Console.WriteLine("Failed to update reservation");
      }
    }
  }
}