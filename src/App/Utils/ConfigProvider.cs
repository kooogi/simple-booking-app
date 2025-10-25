using Azure.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigProvider
{
  private static IServiceProvider? _serviceProvider;

  static ConfigProvider()
  {
    var services = new ServiceCollection();
    string? connectionString = GetConnectionString();

    if (connectionString == null)
    {
      throw new InvalidOperationException("Connection string not found.");
    }

    services.AddTransient<IRoomService, RoomService>();
    services.AddTransient<IReservationService, ReservationService>();
    services.AddSingleton<IRoomRepository>(p => new RoomRepository(connectionString));
    services.AddSingleton<IReservationRepository>(p => new ReservationRepository(connectionString));

    _serviceProvider = services.BuildServiceProvider();
  }

  public static IRoomService? RoomServiceProvider() => _serviceProvider?.GetService<IRoomService>();

  public static IReservationService? ReservationServiceProvider() => _serviceProvider?.GetService<IReservationService>();

  public static string? GetConnectionString()
  {
    var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    IConfiguration config = builder.Build();

    return config.GetConnectionString("DefaultConnection");
  }
}