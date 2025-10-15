// tutaj kod do zarezerwowania pokoju czyli uslata dany w jakich pokoj jest zajety oraz zmienia stan pokoju isAvaiable na 0 (zajęty)
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Data.SqlClient;
public interface IReservationRepository
{
  bool CreateReservation(int roomNumber, DateTime startDate, DateTime endDate);
}

public class ReservationRepository : IReservationRepository
{
  private readonly string _connectionString;
  public ReservationRepository(string connectionString)
  {
    _connectionString = connectionString;
  }
  public bool CreateReservation(int roomNumber, DateTime startDate, DateTime endDate)
  {
    using (var connection = new SqlConnection(_connectionString))
    {
      connection.Open();
      const string query = @"INSERT INTO Reservations (RoomId, PricePerNight, StartDate, EndDate) 
        VALUES (
            (SELECT RoomId FROM Rooms WHERE RoomNumber = @roomNumber),  
            200, 
            @startDate, 
            @endDate
        );";

      using var command = new SqlCommand(query, connection);
      command.Parameters.AddWithValue("@roomNumber", roomNumber);
      command.Parameters.AddWithValue("@startDate", startDate);
      command.Parameters.AddWithValue("@endDate", endDate);

      int rowsAffected = command.ExecuteNonQuery();
      if (rowsAffected > 0)
      {
        Console.WriteLine("Pomyślnie dokonano rezerwacji");
        return true;
      }
      else
      {
        Console.WriteLine("Nie udało się dokonać rezerwacji");
        return false;
      }
    }
  }

}