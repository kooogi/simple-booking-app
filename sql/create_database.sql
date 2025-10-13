IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'SimpleBookingAppDB')
BEGIN
	CREATE DATABASE SimpleBookingAppDB;
END
GO