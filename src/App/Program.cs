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
  bool isRunning = true;
  Console.WriteLine("Witamy  w systemie rezerwacji pokoi wybierz jedną z dostępnych opcji");
  while (isRunning)
  {
    
    Console.WriteLine("1. Stwórz rezerwację | 2. Sprawdź dostępne pokoje | 3. Stwórz pokój | 4. Lista Rezerwacji | 5. Edycja Pokoju | 6. Edycja Rezerwacji | 7. Usuwanie Pokoju | 8. Usuwanie Rezerwacji | 9. Exit");
    string? userInput = Console.ReadLine();
    int value;
    if (int.TryParse(userInput, out value))
    {
      switch (value)
      {
        case 1:
          if (reservationService != null)
          {
            reservationService.ProcessReservation();
          }
          else
          {
            Console.WriteLine("Reservation service is not available.");
          }
          break;
        case 2:
          if (roomService != null)
          {
            roomService.CheckAvailability();
          }
          else
          {
            Console.WriteLine("Availability service is not available.");
          }
          break;
        case 3:
          if (roomService != null)
          {
            roomService.CreateRoom();
          }
          else
          {
            Console.WriteLine("Creation service is not available.");
          }
          break;
        case 4:
          if (reservationService != null)
          {
            reservationService.showReservations();
          }
          else
          {
            Console.WriteLine("Reservation service is not available.");
          }
          break;
        case 5:
          if (roomService != null)
          {
            roomService.EditRoom();
          }
          else
          {
            Console.WriteLine("Reservation service is not available.");
          }
          break;
        case 6:
          if (reservationService != null)
          {
            reservationService.EditReservation();
          }
          else
          {
            Console.WriteLine("Reservation service is not available.");
          }
          break;
        case 7:
          if (roomService != null)
          {
            roomService.DeleteRoom();
          }
          else
          {
            Console.WriteLine("Reservation service is not available.");
          }
          break;
        case 8:
          if (reservationService != null)
          {
            reservationService.DeleteReservation();
          }
          else
          {
            Console.WriteLine("Reservation service is not available.");
          }
          break;
        case 9:
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
