public interface IRoomAvailabilityService { void CheckAvailability(); }

public class RoomAvailabilityService : IRoomAvailabilityService
{
  private readonly IRoomRepository _roomRepository;

  public RoomAvailabilityService(IRoomRepository roomRepository)
  {
    _roomRepository = roomRepository;
  }
  public void CheckAvailability()
  {
    var availableRooms = _roomRepository.GetAvailableRooms(DateTime.Now, DateTime.Now.AddDays(7));
    Console.WriteLine("Wybrano moduł sprawdzenia dostępności");
    foreach (var room in availableRooms)
    {
      Console.WriteLine($"Pokój: {room.RoomNumber}, Pojemność: {room.Capacity}");
    }
  }
}