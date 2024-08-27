CREATE DATABASE RegistroClientes;
GO

USE RegistroClientes;

CREATE TABLE Clientes(
	Identificacion INT PRIMARY KEY IDENTITY,
	Nombres VARCHAR(50) NOT NULL,
	Apellidos VARCHAR(50) NOT NULL,
	Telefono INT,
	Direccion VARCHAR(100) NOT NULL,
	Correo VARCHAR(75),
	EstadoCivil VARCHAR(50),
	Activo BIT DEFAULT 1,
);

INSERT INTO Clientes (Nombres, Apellidos, Telefono, Direccion, Correo, EstadoCivil, Activo)
VALUES ('Juan Francisco', 'Pérez Galdamez', 5551234, 'Calle Falsa 123', 'juan.perez@email.com', 'Soltero', 1);

