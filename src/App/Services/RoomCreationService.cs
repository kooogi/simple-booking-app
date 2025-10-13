public interface IRoomCreationService { void CreateRoom(); }

public class RoomCreationService : IRoomCreationService
{
  public void CreateRoom()
  {
    Console.WriteLine("Wybrano moduł tworzenia pokoju");
  }
}