DELETE FROM Reservations;
DELETE FROM Rooms;
GO

-- =================================================================
-- Wstawianie danych do tabeli Rooms
-- =================================================================
-- Używamy IDENTITY_INSERT, aby móc ręcznie wstawić wartości klucza głównego (Id)
SET IDENTITY_INSERT Rooms ON;
GO

INSERT INTO Rooms (RoomId, RoomNumber, Capacity) VALUES
(1, '101', 2),  -- Pokój dwuosobowy standard
(2, '102', 2),  -- Pokój dwuosobowy standard
(3, '103', 1),  -- Pokój jednoosobowy
(4, '201', 2),  -- Pokój dwuosobowy premium
(5, '202', 3),  -- Pokój trzyosobowy z dostawką
(6, '203', 1),  -- Pokój jednoosobowy
(7, '301', 4),  -- Apartament rodzinny
(8, '302', 2),  -- Pokój dwuosobowy z widokiem na miasto
(9, '401', 2),  -- Apartament typu Deluxe
(10, '402', 1); -- Pokój jednoosobowy premium
GO

SET IDENTITY_INSERT Rooms OFF;
GO

-- =================================================================
-- Wstawianie danych do tabeli Reservations
-- =================================================================
SET IDENTITY_INSERT Reservations ON;
GO

INSERT INTO Reservations (ReservationId, RoomId, PricePerNight, IsAvailable, StartDate, EndDate) VALUES
-- Rezerwacje dla pokoju 1 (RoomId = 1)
(1, 1, 150.00, 0, '2025-10-10', '2025-10-15'),
(2, 1, 165.50, 0, '2025-12-20', '2025-12-26'),

-- Rezerwacje dla pokoju 2 (RoomId = 2)
(3, 2, 145.00, 0, '2025-11-01', '2025-11-03'),

-- Rezerwacje dla pokoju 3 (RoomId = 3)
(4, 3, 99.99, 0, '2025-11-12', '2025-11-14'),

-- Rezerwacje dla pokoju 4 (RoomId = 4)
(5, 4, 250.00, 0, '2026-01-05', '2026-01-10'),

-- Rezerwacje dla pokoju 5 (RoomId = 5)
(6, 5, 210.75, 0, '2025-11-28', '2025-12-02'),
(7, 5, 220.00, 0, '2025-12-29', '2026-01-02'),

-- Rezerwacje dla pokoju 7 (RoomId = 7)
(8, 7, 350.00, 0, '2025-11-08', '2025-11-18'),

-- Rezerwacje dla pokoju 9 (RoomId = 9)
(9, 9, 450.50, 0, '2026-02-10', '2026-02-14'),

-- Rezerwacje dla pokoju 10 (RoomId = 10)
(10, 10, 180.00, 0, '2025-11-05', '2025-11-06'),
-- Możesz dodać więcej rezerwacji, jeśli potrzebujesz
(11, 2, 155.00, 0, '2025-11-11', '2025-11-13');
GO

SET IDENTITY_INSERT Reservations OFF;
GO

-- Sprawdzenie, czy dane zostały wstawione poprawnie
SELECT * FROM Rooms;
SELECT * FROM Reservations;