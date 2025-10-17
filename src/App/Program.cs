// Import required namespaces
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Build configuration from appsettings.json
var builder = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();

// Get the connection string from configuration
string? connectionString = config.GetConnectionString("DefaultConnection");


// Set up dependency injection container
var services = new ServiceCollection();
services.AddTransient<IReservationService, ReservationService>();
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


if (connectionString == null)
{
  Console.WriteLine("Connection string 'DefaultConnection' not found.");
}
else
{
  MainModule.DisplayMenu();
}