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
    Console.WriteLine("Wybrano moduł sparawdzenia dostępności");
  }
}