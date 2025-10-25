public static class MainModule
{
  public static void DisplayMenu()
  {
    while (true)
    {
      string[] menuOptions =
      {
        "Reservation Management Module",
        "Room Management Module",
        "Exit"
      };

      Console.Clear();
      Console.WriteLine("Select from available modules");
      Console.WriteLine(" ");

      int choice = ConsoleMenu.ShowMenu("Main Menu", menuOptions);

      switch (choice)
      {
        case 0:
          ReservationModule.DisplayMenu();
          break;
        case 1:
          RoomModule.DisplayMenu();
          break;
        case 2:
          Console.WriteLine("Closing app ...");
          return;
      }
    }
  }
}