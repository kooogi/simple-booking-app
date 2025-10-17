using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigProvider
{
  private static readonly ServiceCollection services = new();
  public static string? GetConnectionString()
  {
    var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    IConfiguration config = builder.Build();

    return config.GetConnectionString("DefaultConnection");
  }

  public static IRoomService? RoomServiceProvider()
  {
    string? connectionString = GetConnectionString();
    if (connectionString == null)
    {
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    services.AddTransient<IRoomService, RoomService>();
    services.AddSingleton<IRoomRepository>(provider => new RoomRepository(connectionString));
    var serviceProvider = services.BuildServiceProvider();
    var roomService = serviceProvider.GetService<IRoomService>();
    return roomService;
  }
  public static IReservationService? ReservationServiceProvider()
  {
    string? connectionString = GetConnectionString();
    if (connectionString == null)
    {
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    services.AddTransient<IReservationService, ReservationService>();
    services.AddTransient<IRoomService, RoomService>();
    services.AddSingleton<IRoomRepository>(provider => new RoomRepository(connectionString));
    services.AddSingleton<IReservationRepository>(provider => new ReservationRepository(connectionString));
    var serviceProvider = services.BuildServiceProvider();
    var reservationService = serviceProvider.GetService<IReservationService>();
    return reservationService;
  }
}