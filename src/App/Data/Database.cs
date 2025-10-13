using System.Data;
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;

public class Database
{
  private readonly string _connectionString;

  public Database(string connectionString)
  {
    _connectionString = connectionString;
  }
  public void TestConnection()
  {
    using var connection = new SqlConnection(_connectionString);
    connection.Open();
    Console.WriteLine("Połączenie z bazą działa!");
  }

  public string getSqlConnection()
  {
    return _connectionString;
  }
}