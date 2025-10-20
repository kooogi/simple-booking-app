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
    string[] menuRoomCreation = _roomService.CheckAvailability();
    int choice = ConsoleMenu.ShowMenu("Available rooms: ", menuRoomCreation);

    Console.WriteLine($"Selected room: {menuRoomCreation[choice]}");

    Match matchRoomNumber = Regex.Match(menuRoomCreation[choice], @"Room:\s*(\d+)");
    int roomNumber = int.Parse(matchRoomNumber.Groups[1].Value);
    Match matchGuestsNumber = Regex.Match(menuRoomCreation[choice], @"Capacity:\s*(\d+)");
    int guestsNumber = int.Parse(matchGuestsNumber.Groups[1].Value);
    Match matchStartDate = Regex.Match(menuRoomCreation[choice], @"Check-In:\s*(\d{1,2}\.\d{1,2}\.\d{4})");
    DateTime startDate = DateTime.Parse(matchStartDate.Groups[1].Value);
    Match matchEndDate = Regex.Match(menuRoomCreation[choice], @"Check-Out:\s*(\d{1,2}\.\d{1,2}\.\d{4})");
    DateTime endDate = DateTime.Parse(matchEndDate.Groups[1].Value);
  
    _reservationRepository.CreateReservation(roomNumber, guestsNumber, startDate, endDate);
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