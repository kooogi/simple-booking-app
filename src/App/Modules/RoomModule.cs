public static class RoomModule
{
  public static void DisplayMenu()
  {
    while (true)
    {
      string[] menuOptions =
{
      "Create Room",
      "Edit Room",
      "Delete Room",
      "Check Available Rooms",
      "List All Rooms",
      "Back"
    };

      string[] subMenuOption = { "Back" };

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
          Console.WriteLine("Press any key to go back...");
          Console.ReadKey();
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
          Console.WriteLine("Press any key to go back...");
          Console.ReadKey();
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
          Console.WriteLine("Press any key to go back...");
          Console.ReadKey();
          break;
        case 3:
          if (roomService != null)
          {
            roomService.DisplayAvailability();
          }
          else
          {
            Console.WriteLine("Availability service is not available.");
          }
          Console.WriteLine("Press any key to go back...");
          Console.ReadKey();
          break;
        case 4:
          if (roomService != null)
          {
            roomService.ShowAllRooms();
          }
          else
          {
            Console.WriteLine("Availability service is not available.");
          }
          Console.WriteLine("Press any key to go back...");
          Console.ReadKey();
          break;
        case 5:
          return;
      }
    }
  }
}