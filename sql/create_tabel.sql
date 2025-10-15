USE SimpleBookingAppDB;
GO

IF OBJECT_ID('Reservations', 'U') IS NOT NULL DROP TABLE Reservations;
IF OBJECT_ID('Rooms', 'U') IS NOT NULL DROP TABLE Rooms;


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Rooms') 
CREATE TABLE Rooms (
    RoomId INT PRIMARY KEY IDENTITY(1,1),
    RoomNumber NVARCHAR(50) NOT NULL UNIQUE,
    Capacity INT NOT NULL,
);
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Reservations') 
CREATE TABLE Reservations (
    ReservationId INT PRIMARY KEY IDENTITY(1,1),
    RoomId INT FOREIGN KEY REFERENCES Rooms(RoomId),
    PricePerNight DECIMAL(18, 2) NOT NULL,
    IsAvailable BIT DEFAULT 1,
    StartDate DATE,
    EndDate DATE
);
GO

-- TODO zmienić pricePerNight do room a nie do rezerwacji
