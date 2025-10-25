using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace BookingApp.Tests;

public class DatabaseTestHelper
{
    private readonly string _connectionString;

    public DatabaseTestHelper()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        _connectionString = config.GetConnectionString("DefaultConnection") 
            ?? throw new InvalidOperationException("Connection string not found");
    }

    public string ConnectionString => _connectionString;

    public void ExecuteNonQuery(string sql, Action<SqlCommand>? configureCommand = null)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        using var command = new SqlCommand(sql, connection);
        configureCommand?.Invoke(command);
        command.ExecuteNonQuery();
    }

    public List<T> ExecuteQuery<T>(string sql, Func<SqlDataReader, T> mapFunction, Action<SqlCommand>? configureCommand = null)
    {
        var results = new List<T>();
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        using var command = new SqlCommand(sql, connection);
        configureCommand?.Invoke(command);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            results.Add(mapFunction(reader));
        }
        return results;
    }

    public void CleanupTestRoom(int roomNumber)
    {
        ExecuteNonQuery(
            "DELETE FROM Reservations WHERE RoomId IN (SELECT RoomId FROM Rooms WHERE RoomNumber = @roomNumber); DELETE FROM Rooms WHERE RoomNumber = @roomNumber",
            cmd => cmd.Parameters.AddWithValue("@roomNumber", roomNumber)
        );
    }

    public void CleanupTestReservation(int reservationId)
    {
        ExecuteNonQuery(
            "DELETE FROM Reservations WHERE ReservationId = @reservationId",
            cmd => cmd.Parameters.AddWithValue("@reservationId", reservationId)
        );
    }
}
