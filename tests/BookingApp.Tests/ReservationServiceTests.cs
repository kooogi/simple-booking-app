using Xunit;
using Moq;

namespace BookingApp.Tests;

public class ReservationServiceTests
{
    [Fact]
    public void ShowReservations_ShouldCallRepository()
    {
        var mockReservationRepo = new Mock<IReservationRepository>();
        var mockRoomService = new Mock<IRoomService>();
        
        var testData = new List<(Reservation, Room)>
        {
            (
                new Reservation { ReservationId = 1, RoomId = 1, GuestsNumber = 2, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2) },
                new Room { RoomId = 1, RoomNumber = "101", Capacity = 2, PricePerNight = 100m }
            )
        };
        
        mockReservationRepo.Setup(repo => repo.reservationsHistory())
            .Returns(testData);
        
        var reservationService = new ReservationService(mockReservationRepo.Object, mockRoomService.Object);
        
        reservationService.ShowReservations();
        
        mockReservationRepo.Verify(repo => repo.reservationsHistory(), Times.Once);
    }
}
