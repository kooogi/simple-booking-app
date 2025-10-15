DELETE FROM Reservations;
DELETE FROM Rooms;
GO

-- Wstawianie przykładowych danych do tabeli Rooms
-- Wstawianie przykładowych danych do tabeli Rooms
INSERT INTO Rooms (RoomNumber, Capacity, PricePerNight)
VALUES 
(101, 2, 250.00),
(102, 3, 300.00),
(103, 4, 450.00),
(201, 2, 280.00),
(202, 1, 180.00),
(203, 2, 220.00),
(301, 5, 600.00),
(302, 3, 350.00),
(303, 4, 400.00),
(401, 6, 1200.00);
GO
-- Wstawianie przykładowych danych do tabeli Reservations
INSERT INTO Reservations (RoomId, GuestsNumber, StartDate, EndDate)
VALUES
(1, 2, '2025-10-01', '2025-10-05'),
(2, 3, '2025-10-03', '2025-10-06'),
(3, 4, '2025-10-10', '2025-10-15'),
(1, 2, '2025-10-20', '2025-10-22'),
(4, 2, '2025-10-05', '2025-10-09'),
(5, 1, '2025-10-07', '2025-10-09'),
(6, 2, '2025-10-11', '2025-10-13'),
(7, 5, '2025-10-15', '2025-10-20'),
(8, 3, '2025-10-18', '2025-10-22'),
(10, 4, '2025-10-25', '2025-10-30');
GO


-- Sprawdzenie, czy dane zostały wstawione poprawnie
SELECT * FROM Rooms;
SELECT * FROM Reservations;