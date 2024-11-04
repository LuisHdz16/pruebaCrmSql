-- Crear base de datos
CREATE DATABASE CRM;
GO

USE CRM;
GO

-- Crear tabla Clientes
CREATE TABLE Clientes (
    ID_Cliente INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(50) NOT NULL,
    Apellidos NVARCHAR(50) NOT NULL,
    Telefono NVARCHAR(15) NULL,
    Correo NVARCHAR(50) NULL
);

-- Crear tabla Tratamientos
CREATE TABLE Tratamientos (
    ID_Tratamiento INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(255) NULL,
    DuracionEstimada INT NULL -- en minutos u horas, según prefieras
);

-- Crear tabla Promociones
CREATE TABLE Promociones (
    ID_Promocion INT PRIMARY KEY IDENTITY,
    ID_Tratamiento INT NOT NULL,
    Nombre NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(255) NULL,
    Estatus NVARCHAR(15) CHECK (Estatus IN ('Disponible', 'No disponible')),
    FOREIGN KEY (ID_Tratamiento) REFERENCES Tratamientos(ID_Tratamiento)
);

-- Crear tabla Cursos
CREATE TABLE Cursos (
    ID_Curso INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(255) NULL,
    Estatus NVARCHAR(15) CHECK (Estatus IN ('Disponible', 'No Disponible'))
);

-- Crear tabla Citas
CREATE TABLE Citas (
    ID_Cita INT PRIMARY KEY IDENTITY,
    ID_Cliente INT NOT NULL,
    ID_Tratamiento INT NOT NULL,
    ID_Promocion INT NULL,
    Fecha DATETIME NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    Estatus NVARCHAR(15) CHECK (Estatus IN ('Realizado', 'Próximamente', 'Cancelado')),
    FOREIGN KEY (ID_Cliente) REFERENCES Clientes(ID_Cliente),
    FOREIGN KEY (ID_Tratamiento) REFERENCES Tratamientos(ID_Tratamiento),
    FOREIGN KEY (ID_Promocion) REFERENCES Promociones(ID_Promocion)
);

-- Crear tabla Inscripcion de Curso
CREATE TABLE InscripcionCurso (
    ID_Inscripcion INT PRIMARY KEY IDENTITY,
    ID_Cliente INT NOT NULL,
    ID_Curso INT NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaFin DATE NULL,
    Duracion INT NULL, -- en días
    PrecioTotal DECIMAL(10, 2) NOT NULL,
    Estatus NVARCHAR(15) CHECK (Estatus IN ('En curso', 'Realizado', 'Por realizar')),
    FOREIGN KEY (ID_Cliente) REFERENCES Clientes(ID_Cliente),
    FOREIGN KEY (ID_Curso) REFERENCES Cursos(ID_Curso)
);