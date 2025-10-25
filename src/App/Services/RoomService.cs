using System.Text.RegularExpressions;
public interface IRoomService { string[] CheckAvailability(); void CreateRoom(); void EditRoom(); void DeleteRoom(); void ShowAllRooms(); void DisplayAvailability(); }

public class RoomService : IRoomService
{
  private readonly IRoomRepository _roomRepository;

  public RoomService(IRoomRepository roomRepository)
  {
    _roomRepository = roomRepository;
  }
  public string[] CheckAvailability()
  {
    Console.WriteLine("Enter the number of guests");
    string? guestsNumberInput = Console.ReadLine();
    int guestsNumber;

    while (!int.TryParse(guestsNumberInput, out guestsNumber) || guestsNumber < 0)
    {
      Console.WriteLine("Invalid value | Provide number of guests once again");
      guestsNumberInput = Console.ReadLine();
    }

    Console.WriteLine("Enter check-in date (dd.MM.yyyy)");
    string? startDateInput = Console.ReadLine();
    DateTime startDate;
    while (startDateInput == null || !DateTime.TryParse(startDateInput, out startDate) || startDate < DateTime.Today)
    {
      Console.WriteLine("Invalid date format (dd.MM.yyyy) | Provide check-in date once again");
      startDateInput = Console.ReadLine();
    }

    Console.WriteLine("Enter check-out date (dd.MM.yyyy)");
    string? endDateInput = Console.ReadLine();
    DateTime endDate;
    while (endDateInput == null || !DateTime.TryParse(endDateInput, out endDate) || endDate <= startDate)
    {
      Console.WriteLine("Invalid date format (dd.MM.yyyy) | Provide check-out date once again");
      endDateInput = Console.ReadLine();
    }

    var availableRooms = _roomRepository.GetAvailableRooms(guestsNumber, startDate, endDate);
    List<string> list = new List<string>();
    foreach (var room in availableRooms)
    {
      list.Add($"Room: {room.RoomNumber}, Capacity: {room.Capacity}, Price per night: {room.PricePerNight} PLN, Check-In: {startDate:d}, Check-Out: {endDate:d}");
    }
    string[] menuRoomCreation = list.ToArray();
    if (menuRoomCreation.Length == 0)
    {
      Console.WriteLine($"No available rooms between {startDate} and {endDate} for {guestsNumber} guests");
    }
    return menuRoomCreation;
  }
  
  public void DisplayAvailability()
  {
    string[] availableRooms = CheckAvailability();
    if(availableRooms.Length != 0)
    {
      Console.WriteLine("Available rooms: ");
      foreach(var room in availableRooms)
      {
        Console.WriteLine(room);
      }
    }
  }

  public void CreateRoom()
  {
    var (roomNumber, roomCapacity, roomPrice) = RoomCreateEditUI();
    _roomRepository.RoomRegistration(roomNumber, roomCapacity, roomPrice);
  }
  public void EditRoom()
  {
    var roomList = _roomRepository.RoomList();
    List<string> list = new List<string>();

    foreach (var room in roomList)
    {
      list.Add($"RoomID: {room.RoomId}, Room Number: {room.RoomNumber}, Capacity: {room.Capacity}, Price Per Night: {room.PricePerNight}");
    }
    
    string[] menuRoom = list.ToArray();
    int choice = ConsoleMenu.ShowMenu("All reservations:", menuRoom);

    Console.WriteLine($"Selected Room: {menuRoom[choice]}");

    Match matchRoomNumber = Regex.Match(menuRoom[choice], @"Room Number:\s*(\d+)");
    int roomNumber = int.Parse(matchRoomNumber.Groups[1].Value);

    Console.WriteLine("Provide new Room data:");

    var (newRoomNumber, roomCapacity, roomPrice) = RoomCreateEditUI();
    _roomRepository.RoomUpdate(roomNumber, newRoomNumber, roomCapacity, roomPrice);
  }
  public void DeleteRoom()
  {
    var roomList = _roomRepository.RoomList();
    List<string> list = new List<string>();

    foreach (var room in roomList)
    {
      list.Add($"RoomID: {room.RoomId}, Room Number: {room.RoomNumber}, Capacity: {room.Capacity}, Price Per Night: {room.PricePerNight}");
    }
    
    string[] menuRoom = list.ToArray();
    int choice = ConsoleMenu.ShowMenu("All reservations:", menuRoom);

    Console.WriteLine($"Selected Room: {menuRoom[choice]}");

    Match matchRoomId = Regex.Match(menuRoom[choice], @"RoomID:\s*(\d+)");
    int roomId = int.Parse(matchRoomId.Groups[1].Value);

    _roomRepository.RoomRemoval(roomId);
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

  public void ShowAllRooms() {
    var roomList = _roomRepository.RoomList();
    Console.WriteLine("List of all Rooms");
    foreach(var room in roomList){
      Console.WriteLine($"RoomID: {room.RoomId}, Room Number: {room.RoomNumber}, Capacity: {room.Capacity}, Price Per Night: {room.PricePerNight}");
    }
  }
}