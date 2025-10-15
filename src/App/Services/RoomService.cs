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

  public void CreateRoom() { }
  public void EditRoom() { }
  public void DeleteRoom() { }
}