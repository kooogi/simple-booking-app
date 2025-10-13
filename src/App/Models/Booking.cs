public class Booking
{
  public int Id { get; set; }
  public int RoomId { get; set; }
  public decimal PricePerNight { get; set; }
  public bool IsAvailable { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
}
