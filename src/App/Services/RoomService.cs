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
    Console.WriteLine("Wprowadź liczbę gości");
    string? questsNumberInput = Console.ReadLine();
    int questsNumber;
    if (int.TryParse(questsNumberInput, out questsNumber))
    {

    }
    else
    {
      Console.WriteLine("Niepoprawny format proszę wprowadzić liczbę");
      return (default, default, default);
    }
    //Wprowadzanie dat
    Console.WriteLine("Wprowadź daty przyjazdu (dd.MM.yyyy)");
    string? startDateInput = Console.ReadLine();
    DateTime startDate;
    if (startDateInput != null && DateTime.TryParse(startDateInput, out startDate))
    {

    }
    else
    {
      Console.WriteLine("Nie wprowadzono poprawnego formatu daty (dd.MM.yyyy)");
      return (default, default, default);
    }

    Console.WriteLine("Wprowadź datę wyjazdu (dd.MM.yyyy)");
    string? endDateInput = Console.ReadLine();
    DateTime endDate;
    if (endDateInput != null && DateTime.TryParse(endDateInput, out endDate))
    {

    }
    else
    {
      Console.WriteLine("Nie wprowadzono poprawnego formatu daty (dd.MM.yyyy)");
      return (default, default, default);
    }

    //sprawdzenie dostępności pokoi na podstawie podanych dat
    var availableRooms = _roomRepository.GetAvailableRooms(questsNumber, startDate, endDate);
    Console.WriteLine("Dostępne pokoje");
    foreach (var room in availableRooms)
    {
      Console.WriteLine($"Pokój: {room.RoomNumber}, Pojemność: {room.Capacity}, Cena za noc: {room.PricePerNight} PLN");
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
    Console.WriteLine("Wprowadź aktualny numer pokoju");
    string? roomNumberInput = Console.ReadLine();
    int oldRoomNumber;
    if (int.TryParse(roomNumberInput, out oldRoomNumber))
    {

    }
    else
    {
      Console.WriteLine("Niepoprawny format proszę wprowadzić liczbę");
    }
    var (newRoomNumber, roomCapacity, roomPrice) = RoomCreateEditUI();
    _roomRepository.RoomUpdate(oldRoomNumber, newRoomNumber, roomCapacity, roomPrice);
  }
  public void DeleteRoom()
  {
    Console.WriteLine("Wprowadź numer pokoju do usunięcia");
    string? roomNumberInput = Console.ReadLine();
    int roomNumber;
    if (int.TryParse(roomNumberInput, out roomNumber))
    {

    }
    else
    {
      Console.WriteLine("Niepoprawny format proszę wprowadzić liczbę");
    }
    _roomRepository.RoomRemoval(roomNumber);
  }

  private (int, int, int) RoomCreateEditUI()
  {
    Console.WriteLine("Wprowadź numer pokoju");
    string? roomNumberInput = Console.ReadLine();
    int roomNumber;
    if (int.TryParse(roomNumberInput, out roomNumber))
    {

    }
    else
    {
      Console.WriteLine("Niepoprawny format proszę wprowadzić liczbę");
    }

    Console.WriteLine("Wielkość pokoju");
    string? roomCapacityInput = Console.ReadLine();
    int roomCapacity;
    if (int.TryParse(roomCapacityInput, out roomCapacity))
    {

    }
    else
    {
      Console.WriteLine("Niepoprawny format proszę wprowadzić liczbę");
    }
    Console.WriteLine("Cena za noc:");
    string? roomPriceInput = Console.ReadLine();
    int roomPrice;
    if (int.TryParse(roomPriceInput, out roomPrice))
    {

    }
    else
    {
      Console.WriteLine("Niepoprawny format proszę wprowadzić liczbę");
    }

    return (roomNumber, roomCapacity, roomPrice);
  }
}