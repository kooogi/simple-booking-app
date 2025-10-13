public interface IReservationService { void CreateReservation(); }

public class ReservationService : IReservationService
{
  public void CreateReservation()
  {
    Console.WriteLine("Wybrano moduł tworzenia rezerwacji");
  }
}