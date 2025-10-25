public class Reservation
{
    public int ReservationId { get; set; }
    public int RoomId { get; set; }
    public int GuestsNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
