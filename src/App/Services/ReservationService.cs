public interface IReservationService { void ProcessReservation(); void showReservations(); void EditReservation(); void DeleteReservation(); }

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

    Console.WriteLine("Wybierz z listy dostępnych opcji poprzez wpisanie numeru pokoju");
    string? availableOptionsInput = Console.ReadLine();
    int availableRoomOptions;
    if (int.TryParse(availableOptionsInput, out availableRoomOptions))
    {
      _reservationRepository.CreateReservation(availableRoomOptions, guestsNumber, startDate, endDate);
    }
    else
    {
      Console.WriteLine("Niepoprawny format, proszę wybrać z listy poprzez wpisanie numeru pokoju");
      return;
    }
  }
  public void showReservations()
  {
    var reservationList = _reservationRepository.reservationsHistory();
    Console.WriteLine("Wszystkie rezerwacje:");
    foreach (var reservation in reservationList)
    {
      Console.WriteLine($"Od: {reservation.Item1.StartDate:d}, Do: {reservation.Item1.EndDate:d}, Pokój: {reservation.Item2.RoomNumber}, Ilość gości: {reservation.Item1.GuestsNumber}, Wielkość Pokoju {reservation.Item2.Capacity}, Cena za noc: {reservation.Item2.PricePerNight}");
    }
  }
  public void EditReservation()
  {
    Console.WriteLine("Wprowadź numer rezerwacji do edycji");
    string? roomNumberInput = Console.ReadLine();
    int reservationId;
    if (int.TryParse(roomNumberInput, out reservationId))
    {

    }
    else
    {
      Console.WriteLine("Niepoprawny format proszę wprowadzić liczbę");
    }
    Console.WriteLine("Wprowadź daty przyjazdu (dd.MM.yyyy)");
    string? startDateInput = Console.ReadLine();
    DateTime startDate;
    if (startDateInput == null || !DateTime.TryParse(startDateInput, out startDate))
    {
      Console.WriteLine("Nie wprowadzono poprawnego formatu daty (dd.MM.yyyy)");
      return;
    }

    Console.WriteLine("Wprowadź datę wyjazdu (dd.MM.yyyy)");
    string? endDateInput = Console.ReadLine();
    DateTime endDate;
    if (endDateInput == null || !DateTime.TryParse(endDateInput, out endDate))
    {
      Console.WriteLine("Nie wprowadzono poprawnego formatu daty (dd.MM.yyyy)");
      return;
    }
    _reservationRepository.ReservationUpdate(reservationId, startDate, endDate);
}

  public void DeleteReservation()
  {
    Console.WriteLine("Wprowadź numer rezerwacji do usunięcia");
    string? roomNumberInput = Console.ReadLine();
    int reservationId;
    if (int.TryParse(roomNumberInput, out reservationId))
    {

    }
    else
    {
      Console.WriteLine("Niepoprawny format proszę wprowadzić liczbę");
    }
    _reservationRepository.ReservationRemoval(reservationId);
  }
}