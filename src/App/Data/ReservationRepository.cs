using System.Data;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;
public interface IReservationRepository
{
    void CreateReservation(int roomNumber, int guestsNumber, DateTime startDate, DateTime endDate);
    List<(Reservation, Room)> reservationsHistory();
    void ReservationRemoval(int reservationId);
    void ReservationUpdate(int reservationId, int roomId, DateTime startDate, DateTime endDate);
}

public class ReservationRepository : IReservationRepository
{
    private const string accept = "Y";
    private readonly string _connectionString;
    public ReservationRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public void CreateReservation(int roomNumber, int guestsNumber, DateTime startDate, DateTime endDate)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            const string query = @"INSERT INTO Reservations (RoomId, GuestsNumber, StartDate, EndDate) 
        VALUES (
            (SELECT RoomId FROM Rooms WHERE RoomNumber = @roomNumber),  
            @guestsNumber, 
            @startDate, 
            @endDate
        );";

            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                using var command = new SqlCommand(query, connection, transaction);
                command.Parameters.AddWithValue("@roomNumber", roomNumber);
                command.Parameters.AddWithValue("@guestsNumber", guestsNumber);
                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);
                command.ExecuteNonQuery();
                Console.WriteLine("Do You want to create reservation on room: " + roomNumber + " (Y/N)");
                string? confirm = Console.ReadLine();

                if (confirm?.ToUpper() == accept)
                {
                    transaction.Commit();
                    Console.WriteLine("Transaction confirmed - room " + roomNumber + " reservation created");
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
    public List<(Reservation, Room)> reservationsHistory()
    {
        var allReservation = new List<(Reservation, Room)>();
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            const string query = @"SELECT Reservations.ReservationId, Reservations.StartDate, Reservations.EndDate, Rooms.RoomNumber, Reservations.GuestsNumber, Rooms.Capacity, Rooms.PricePerNight FROM Reservations JOIN Rooms ON Reservations.RoomId = Rooms.RoomId";

            using var command = new SqlCommand(query, connection);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var reservation = new Reservation
                {
                    ReservationId = reader.GetInt32(reader.GetOrdinal("ReservationId")),
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
            const string query = "DELETE FROM Reservations WHERE ReservationId=@reservationId";

            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                using var command = new SqlCommand(query, connection, transaction);
                command.Parameters.AddWithValue("@reservationId", reservationId);
                command.ExecuteNonQuery();
                Console.WriteLine("Do You want to delete reservation: " + reservationId + " (Y/N)");
                string? confirm = Console.ReadLine();

                if (confirm?.ToUpper() == accept)
                {
                    transaction.Commit();
                    Console.WriteLine("Transaction confirmed - reservation: " + reservationId + " deleted");
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
    public void ReservationUpdate(int reservationId, int roomId, DateTime startDate, DateTime endDate)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            const string query = "UPDATE Reservations SET StartDate = @startDate, EndDate = @endDate, RoomId = @roomId WHERE ReservationId = @reservationId;";

            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                using var command = new SqlCommand(query, connection, transaction);
                command.Parameters.AddWithValue("@reservationId", reservationId);
                command.Parameters.AddWithValue("@roomId", roomId);
                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);
                command.ExecuteNonQuery();

                Console.WriteLine("Do You want to update reservation: " + reservationId + " (Y/N)");
                string? confirm = Console.ReadLine();

                if (confirm?.ToUpper() == accept)
                {
                    transaction.Commit();
                    Console.WriteLine("Transaction confirmed - reservation: " + reservationId + " updated");
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
}