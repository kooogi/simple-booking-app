using System.Runtime.CompilerServices;

public interface IReservationService { void ProcessReservation(); void showReservations(); }

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
  public void showReservations()
  {
    var reservationList = _reservationRepository.reservationsHistory();
    Console.WriteLine("Wszystkie rezerwacje:");
    foreach (var reservation in reservationList)
    {
      Console.WriteLine($"Od: {reservation.Item1.StartDate:d}, Do: {reservation.Item1.EndDate:d}, Pokój: {reservation.Item2.RoomNumber}, Ilość gości: {reservation.Item1.GuestsNumber}, Wielkość Pokoju {reservation.Item2.Capacity}, Cena za noc: {reservation.Item2.PricePerNight}");
    }
  }
}