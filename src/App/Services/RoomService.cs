public interface IRoomService { (int, DateTime, DateTime) CheckAvailability(); void CreateRoom(); void EditRoom(); void DeleteRoom(); }

public class RoomService : IRoomService
{
  private readonly IRoomRepository _roomRepository;

  public RoomService(IRoomRepository roomRepository)
  {
    _roomRepository = roomRepository;
  }
  public (int, DateTime, DateTime) CheckAvailability()
  {
    Console.WriteLine("Enter the number of guests");
    string? questsNumberInput = Console.ReadLine();
    int questsNumber;
    if (int.TryParse(questsNumberInput, out questsNumber))
    {

    }
    else
    {
      Console.WriteLine("Invalid format, please enter a number");
      return (default, default, default);
    }
    //Entering dates
    Console.WriteLine("Enter check-in date (dd.MM.yyyy)");
    string? startDateInput = Console.ReadLine();
    DateTime startDate;
    if (startDateInput != null && DateTime.TryParse(startDateInput, out startDate))
    {

    }
    else
    {
      Console.WriteLine("Invalid date format (dd.MM.yyyy)");
      return (default, default, default);
    }

    Console.WriteLine("Enter check-out date (dd.MM.yyyy)");
    string? endDateInput = Console.ReadLine();
    DateTime endDate;
    if (endDateInput != null && DateTime.TryParse(endDateInput, out endDate))
    {

    }
    else
    {
      Console.WriteLine("Invalid date format (dd.MM.yyyy)");
      return (default, default, default);
    }

    //Check room availability based on provided dates
    var availableRooms = _roomRepository.GetAvailableRooms(questsNumber, startDate, endDate);
    Console.WriteLine("Available rooms");
    foreach (var room in availableRooms)
    {
      Console.WriteLine($"Room: {room.RoomNumber}, Capacity: {room.Capacity}, Price per night: {room.PricePerNight} PLN");
    }
    return (questsNumber, startDate, endDate);
  }

  public void CreateRoom()
  {
    var (roomNumber, roomCapacity, roomPrice) = RoomCreateEditUI();
    _roomRepository.RoomRegistration(roomNumber, roomCapacity, roomPrice);
  }
  public void EditRoom()
  {
    Console.WriteLine("Enter the current room number");
    string? roomNumberInput = Console.ReadLine();
    int oldRoomNumber;
    if (int.TryParse(roomNumberInput, out oldRoomNumber))
    {

    }
    else
    {
      Console.WriteLine("Invalid format, please enter a number");
    }
    var (newRoomNumber, roomCapacity, roomPrice) = RoomCreateEditUI();
    _roomRepository.RoomUpdate(oldRoomNumber, newRoomNumber, roomCapacity, roomPrice);
  }
  public void DeleteRoom()
  {
    Console.WriteLine("Enter the room number to delete");
    string? roomNumberInput = Console.ReadLine();
    int roomNumber;
    if (int.TryParse(roomNumberInput, out roomNumber))
    {

    }
    else
    {
      Console.WriteLine("Invalid format, please enter a number");
    }
    _roomRepository.RoomRemoval(roomNumber);
  }

  private (int, int, int) RoomCreateEditUI()
  {
    Console.WriteLine("Enter the room number");
    string? roomNumberInput = Console.ReadLine();
    int roomNumber;
    if (int.TryParse(roomNumberInput, out roomNumber))
    {

    }
    else
    {
      Console.WriteLine("Invalid format, please enter a number");
    }

    Console.WriteLine("Room capacity");
    string? roomCapacityInput = Console.ReadLine();
    int roomCapacity;
    if (int.TryParse(roomCapacityInput, out roomCapacity))
    {

    }
    else
    {
      Console.WriteLine("Invalid format, please enter a number");
    }
    Console.WriteLine("Price per night:");
    string? roomPriceInput = Console.ReadLine();
    int roomPrice;
    if (int.TryParse(roomPriceInput, out roomPrice))
    {

    }
    else
    {
      Console.WriteLine("Invalid format, please enter a number");
    }

    return (roomNumber, roomCapacity, roomPrice);
  }
}