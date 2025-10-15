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
  Console.WriteLine("Welcome to the room booking system, please choose one of the available options");
  while (isRunning)
  {
    
    Console.WriteLine("1. Create Reservation | 2. Check Available Rooms | 3. Create Room | 4. List Reservations | 5. Edit Room | 6. Edit Reservation | 7. Delete Room | 8. Delete Reservation | 9. Exit");
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
      Console.WriteLine("The entered value is not a number! Please make a choice again");
    }
    
  }

}
