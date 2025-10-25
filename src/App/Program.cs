using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();

string? connectionString = config.GetConnectionString("DefaultConnection");

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