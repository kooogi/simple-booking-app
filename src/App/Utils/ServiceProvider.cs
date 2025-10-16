using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
public static class ServiceProvider
{
  private var services = new ServiceCollection();
  public static void RoomService()
  {
    string? connectionString = ProvideConnectionString();
    // Set up dependency injection container
    
    services.AddTransient<IRoomService, RoomService>();

    if (connectionString == null)
    {
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }
    services.AddSingleton<IRoomRepository>(provider => new RoomRepository(connectionString));
    services.AddSingleton<IReservationRepository>(provider => new ReservationRepository(connectionString));


    var serviceProvider = services.BuildServiceProvider();

    // Retrieve the reservation service instance
    var reservationService = serviceProvider.GetService<IReservationService>();
    var roomService = serviceProvider.GetService<IRoomService>();
  }
  public static void ReservationService()
  {
    string? connectionString = ProvideConnectionString();
  }
  public static string? ProvideConnectionString()
  {
    var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

    IConfiguration config = builder.Build();

    // Get the connection string from configuration
    string? connectionString = config.GetConnectionString("DefaultConnection");

    return connectionString;
  }
}