using Xunit;

namespace BookingApp.Tests;

public class ModelTests
{
    [Fact]
    public void Room_ShouldSetPropertiesCorrectly()
    {
        var room = new Room
        {
            RoomId = 1,
            RoomNumber = "101",
            Capacity = 2,
            PricePerNight = 100.50m
        };
        
        Assert.Equal(1, room.RoomId);
        Assert.Equal("101", room.RoomNumber);
        Assert.Equal(2, room.Capacity);
        Assert.Equal(100.50m, room.PricePerNight);
    }

    [Fact]
    public void Reservation_ShouldSetPropertiesCorrectly()
    {
        var startDate = DateTime.Today;
        var endDate = DateTime.Today.AddDays(3);
        
        var reservation = new Reservation
        {
            ReservationId = 1,
            RoomId = 101,
            GuestsNumber = 2,
            StartDate = startDate,
            EndDate = endDate
        };
        
        Assert.Equal(1, reservation.ReservationId);
        Assert.Equal(101, reservation.RoomId);
        Assert.Equal(2, reservation.GuestsNumber);
        Assert.Equal(startDate, reservation.StartDate);
        Assert.Equal(endDate, reservation.EndDate);
    }

    [Theory]
    [InlineData(1, "Single occupancy")]
    [InlineData(2, "Double occupancy")]
    [InlineData(4, "Family room")]
    public void Room_CapacityValues_ShouldBeValid(int capacity, string description)
    {
        var room = new Room { Capacity = capacity };
        
        Assert.True(room.Capacity > 0, $"{description} should have positive capacity");
    }

    [Fact]
    public void Reservation_DateRange_ShouldBeValid()
    {
        var reservation = new Reservation
        {
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(3)
        };
        
        Assert.True(reservation.EndDate > reservation.StartDate, "End date should be after start date");
        var duration = (reservation.EndDate - reservation.StartDate).Days;
        Assert.True(duration > 0, "Reservation should have positive duration");
    }
}
