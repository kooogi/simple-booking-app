public static class ReservationModule
{
  public static void DisplayMenu()
  {
    while (true)
    {
      string[] menuOptions =
      {
          "Create Reservation",
          "Edit Reservation",
          "Delete Reservation",
          "List Reservations",
          "Back"
        };

      Console.Clear();
      Console.WriteLine("Select from available modules");
      Console.WriteLine(" ");

      int choice = ConsoleMenu.ShowMenu("Reservation Module", menuOptions);
      var reservationService = ConfigProvider.ReservationServiceProvider();

      switch (choice)
      {
        case 0:
          reservationService?.ProcessReservation();
          Console.WriteLine("Press any key to continue...");
          Console.ReadKey();
          break;
        case 1:
          if (reservationService != null)
          {
            reservationService.EditReservation();
          }
          else
          {
            Console.WriteLine("Reservation service is not available.");
          }
          Console.WriteLine("Press any key to go back...");
          Console.ReadKey();
          break;
        case 2:
          if (reservationService != null)
          {
            reservationService.DeleteReservation();
          }
          else
          {
            Console.WriteLine("Reservation service is not available.");
          }
          Console.WriteLine("Press any key to go back...");
          Console.ReadKey();
          break;
        case 3:
          if (reservationService != null)
          {
            reservationService.ShowReservations();
          }
          else
          {
            Console.WriteLine("Reservation service is not available.");
          }
          Console.WriteLine("Press any key to go back...");
          Console.ReadKey();
          break;
        case 4:
          return;
      }
    }
  }
}