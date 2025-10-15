public interface IReservationService { void ProcessReservation(); }

public class ReservationService : IReservationService
{
  private readonly IReservationRepository _reservationRepository;
  private readonly IRoomAvailabilityService  _availabilityService;
  public ReservationService(IReservationRepository reservationRepository, IRoomAvailabilityService  availabilityService)
  {
    _reservationRepository = reservationRepository;
    _availabilityService = availabilityService;
  }
  public void ProcessReservation()
  {
    var (guestsNumber, startDate, endDate) = _availabilityService.CheckAvailability();

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
}