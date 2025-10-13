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
services.AddTransient<IRoomAvailabilityService, RoomAvailabilityService>();
services.AddTransient<IRoomCreationService, RoomCreationService>();

if (connectionString == null)
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}
services.AddSingleton<IRoomRepository>(provider => new RoomRepository(connectionString));


var serviceProvider = services.BuildServiceProvider();

// Retrieve the reservation service instance
var reservationService = serviceProvider.GetService<IReservationService>();
var roomAvailabilityService = serviceProvider.GetService<IRoomAvailabilityService>();
var roomCreationService = serviceProvider.GetService<IRoomCreationService>();


if (connectionString == null)
{
  Console.WriteLine("Connection string 'DefaultConnection' not found.");
}
else
{
  // Console.WriteLine($"Połączenie: {connectionString}");
  // var db = new Database(connectionString);
  // db.TestConnection();

  // var rooms = db.GetAllRooms();

  // foreach(var r in rooms)
  // {
  //     Console.WriteLine($"{r.Id} - {r.Number} - {r.Capacity} - {r.PricePerNight}");
  // }
  bool isRunning = true;
  Console.WriteLine("Witamy  w systemie rezerwacji pokoi wybierz jedną z dostępnych opcji");
  while (isRunning)
  {
    
    Console.WriteLine("1. Stwórz rezerwację | 2. Sprawdź dostępne pokoje | 3. Stwórz pokój | 4. Exit");
    string? userInput = Console.ReadLine();
    int value;
    if (int.TryParse(userInput, out value))
    {
      switch (value)
      {
        case 1:
          if (reservationService != null)
          {
            reservationService.CreateReservation();
          }
          else
          {
            Console.WriteLine("Reservation service is not available.");
          }
          break;
        case 2:
          if (roomAvailabilityService != null)
          {
            roomAvailabilityService.CheckAvailability();
          }
          else
          {
            Console.WriteLine("Availability service is not available.");
          }
          break;
        case 3:
          if (roomCreationService != null)
          {
            roomCreationService.CreateRoom();
          }
          else
          {
            Console.WriteLine("Creation service is not available.");
          }
          break;
        case 4:
          Console.WriteLine("Goodbye!");
          isRunning = false;
          break;
      }
    }
    else
    {
      Console.WriteLine("Wprowadzona wartość nie jest cyfrą! Proszę ponownie dokonać wyboru");
    }
    
  }

}
