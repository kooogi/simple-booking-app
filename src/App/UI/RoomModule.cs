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

    int choice = ConsoleMenu.ShowMenu("Reservation Module", menuOptions);
    
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
        Console.WriteLine(" ");
        break;
      case 2:
        Console.WriteLine("T");
        break;
      case 3:
        Console.WriteLine(" ");
        break;
      case 4:
        MainMenu.DisplayMenu();
        break;
    }
    Console.ReadKey();
  }
}