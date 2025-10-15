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

  }
  public void EditReservation() { }
  public void DeleteReservation(){}
}