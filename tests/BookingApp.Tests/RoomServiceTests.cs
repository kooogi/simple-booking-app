using Xunit;
using Moq;

namespace BookingApp.Tests;

public class RoomServiceTests
{
    [Fact]
    public void ShowAllRooms_ShouldDisplayAllRooms_WhenRoomsExist()
    {
        var mockRoomRepository = new Mock<IRoomRepository>();
        var testRooms = new List<Room>
        {
            new Room { RoomId = 1, RoomNumber = "101", Capacity = 2, PricePerNight = 100m },
            new Room { RoomId = 2, RoomNumber = "102", Capacity = 4, PricePerNight = 150m }
        };
        
        mockRoomRepository.Setup(repo => repo.RoomList())
            .Returns(testRooms);
        
        var roomService = new RoomService(mockRoomRepository.Object);
        
        roomService.ShowAllRooms();
        
        mockRoomRepository.Verify(repo => repo.RoomList(), Times.Once);
    }

    [Fact]
    public void ShowAllRooms_ShouldHandleEmptyRoomList()
    {
        var mockRoomRepository = new Mock<IRoomRepository>();
        mockRoomRepository.Setup(repo => repo.RoomList())
            .Returns(new List<Room>());
        
        var roomService = new RoomService(mockRoomRepository.Object);
        
        var exception = Record.Exception(() => roomService.ShowAllRooms());
        Assert.Null(exception);
    }
}
