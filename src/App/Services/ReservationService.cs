using System.Text.RegularExpressions;
public interface IReservationService { void ProcessReservation(); void ShowReservations(); void EditReservation(); void DeleteReservation(); }

public class ReservationService : IReservationService
{
  private readonly IReservationRepository _reservationRepository;
  private readonly IRoomService  _roomService;
  public ReservationService(IReservationRepository reservationRepository, IRoomService  roomService)
  {
    _reservationRepository = reservationRepository;
    _roomService = roomService;
  }
  public void ProcessReservation()
  {
    var (guestsNumber, startDate, endDate) = _roomService.CheckAvailability();

    Console.WriteLine("Choose from the available options by entering the room number");
    string? availableOptionsInput = Console.ReadLine();
    int availableRoomOptions;
    if (int.TryParse(availableOptionsInput, out availableRoomOptions))
    {
      _reservationRepository.CreateReservation(availableRoomOptions, guestsNumber, startDate, endDate);
    }
    else
    {
      Console.WriteLine("Invalid format, please choose from the list by entering the room number");
      return;
    }
  }
  public void ShowReservations()
  {
    var reservationList = _reservationRepository.reservationsHistory();
    Console.WriteLine("All reservations:");
    foreach (var reservation in reservationList)
    {
      Console.WriteLine($"ID: {reservation.Item1.ReservationId}| From: {reservation.Item1.StartDate:d}, To: {reservation.Item1.EndDate:d}, Room: {reservation.Item2.RoomNumber}, Number of guests: {reservation.Item1.GuestsNumber}, Room capacity: {reservation.Item2.Capacity}, Price per night: {reservation.Item2.PricePerNight}");
    }
  }

  public void EditReservation(){
    var reservationList = _reservationRepository.reservationsHistory();
    // Console.WriteLine("All reservations:");
    List<string> list = new List<string>();
    foreach (var reservation in reservationList)
    {
      list.Add($"ID: {reservation.Item1.ReservationId} | From: {reservation.Item1.StartDate:d}, To: {reservation.Item1.EndDate:d}, Room: {reservation.Item2.RoomNumber}, Number of guests: {reservation.Item1.GuestsNumber}, Room capacity: {reservation.Item2.Capacity}, Price per night: {reservation.Item2.PricePerNight}");
    }
    string[] menuReservation = list.ToArray();
    int choice = ConsoleMenu.ShowMenu("All reservations:", menuReservation);

    Console.WriteLine($"Selected reservation: {menuReservation[choice]}");

    Match matchResId = Regex.Match(menuReservation[choice], @"ID:\s*(\d+)");
    int reservationId = int.Parse(matchResId.Groups[1].Value);
    Match matchRoomId = Regex.Match(menuReservation[choice], @"Room:\s*(\d+)");
    int roomId = int.Parse(matchRoomId.Groups[1].Value);

    Console.WriteLine("Enter check-in date (dd.MM.yyyy)");
    string? startDateInput = Console.ReadLine();
    DateTime startDate;
    while(startDateInput == null || !DateTime.TryParse(startDateInput, out startDate) || startDate < DateTime.Today){
      Console.WriteLine("Invalid date format (dd.MM.yyyy) | Provide check-in date once again");
      startDateInput = Console.ReadLine();
    }

    Console.WriteLine("Enter check-out date (dd.MM.yyyy)");
    string? endDateInput = Console.ReadLine();
    DateTime endDate;
    while (endDateInput == null || !DateTime.TryParse(endDateInput, out endDate) || endDate <= startDate)
    {
      Console.WriteLine("Invalid date format (dd.MM.yyyy) | Provide check-out date once again");
      endDateInput = Console.ReadLine();
    }
    _reservationRepository.ReservationUpdate(reservationId, roomId,startDate, endDate);
  }

  public void DeleteReservation()
  {
    Console.WriteLine("Enter the reservation number to delete");
    string? roomNumberInput = Console.ReadLine();
    int reservationId;
    if (int.TryParse(roomNumberInput, out reservationId))
    {

    }
    else
    {
      Console.WriteLine("Invalid format, please enter a number");
    }
    _reservationRepository.ReservationRemoval(reservationId);
  }
}