public static class RoomModule
{
  public static void DisplayMenu()
  {
    string[] menuOptions =
    {
      "Create Room",
      "Edit Room",
      "Delete Room",
      "Check Available Rooms",
      "Back"
    };

    Console.Clear();
    Console.WriteLine("Select from available modules");
    Console.WriteLine(" ");

    int choice = ConsoleMenu.ShowMenu("Room Module", menuOptions);
    var roomService = ConfigProvider.RoomServiceProvider();
    
    switch (choice)
    {
      case 0:
        if (roomService != null)
        {
          roomService.CreateRoom();
        }
        else
        {
          Console.WriteLine("Creation service is not available.");
        }
        break;
      case 1:
        if (roomService != null)
        {
          roomService.EditRoom();
        }
        else
        {
          Console.WriteLine("Reservation service is not available.");
        }
        break;
      case 2:
        if (roomService != null)
        {
          roomService.DeleteRoom();
        }
        else
        {
          Console.WriteLine("Reservation service is not available.");
        }
        break;
      case 3:
        if (roomService != null)
        {
          roomService.CheckAvailability();
        }
        else
        {
          Console.WriteLine("Availability service is not available.");
        }
        break;
      case 4:
        MainModule.DisplayMenu();
        break;
    }
    Console.ReadKey();
  }
}