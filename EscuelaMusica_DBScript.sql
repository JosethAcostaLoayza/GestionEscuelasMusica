-- ===========================================
-- Proyecto: Gestión Escuelas de Música - Prueba Técnica Italika
-- Autor: Joseth David Acosta Loayza
-- Fecha: 29 Junio 2025
-- Descripción: Script completo para creación de BD, tablas y stored procedures
-- ===========================================

-- ===========================================
-- 1. Crear Base de Datos y Seleccionar Contexto
-- ===========================================
CREATE DATABASE Italika_GestionEscuelasMusica;
GO

USE Italika_GestionEscuelasMusica;
GO

-- ===========================================
-- 2. Crear Tablas
-- ===========================================

-- Tabla: Escuelas
CREATE TABLE Escuelas (
    EscuelaId INT IDENTITY PRIMARY KEY,
    Codigo NVARCHAR(20) NOT NULL UNIQUE,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255)
);
GO

-- Tabla: Profesores
CREATE TABLE Profesores (
    ProfesorId INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Identificacion NVARCHAR(20) NOT NULL UNIQUE,
    EscuelaId INT NOT NULL,
    CONSTRAINT FK_Profesores_Escuelas FOREIGN KEY (EscuelaId) REFERENCES Escuelas(EscuelaId)
);
GO

-- Tabla: Alumnos
CREATE TABLE Alumnos (
    AlumnoId INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Identificacion NVARCHAR(20) NOT NULL UNIQUE
);
GO

-- Tabla intermedia: AlumnoProfesor (relación N:N)
CREATE TABLE AlumnoProfesor (
    AlumnoId INT NOT NULL,
    ProfesorId INT NOT NULL,
    PRIMARY KEY (AlumnoId, ProfesorId),
    CONSTRAINT FK_AlumnoProfesor_Alumno FOREIGN KEY (AlumnoId) REFERENCES Alumnos(AlumnoId),
    CONSTRAINT FK_AlumnoProfesor_Profesor FOREIGN KEY (ProfesorId) REFERENCES Profesores(ProfesorId)
);
GO

-- Tabla intermedia: AlumnoEscuela (relación N:N)
CREATE TABLE AlumnoEscuela (
    AlumnoId INT NOT NULL,
    EscuelaId INT NOT NULL,
    FechaInscripcion DATE NOT NULL DEFAULT GETDATE(),
    PRIMARY KEY (AlumnoId, EscuelaId),
    CONSTRAINT FK_AlumnoEscuela_Alumno FOREIGN KEY (AlumnoId) REFERENCES Alumnos(AlumnoId),
    CONSTRAINT FK_AlumnoEscuela_Escuela FOREIGN KEY (EscuelaId) REFERENCES Escuelas(EscuelaId)
);
GO

-- ===========================================
-- 3. Stored Procedures CRUD Alumnos
-- ===========================================

-- Crear Alumno
CREATE PROCEDURE sp_CrearAlumno
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @FechaNacimiento DATE,
    @Identificacion NVARCHAR(20)
AS
BEGIN
    INSERT INTO Alumnos (Nombre, Apellido, FechaNacimiento, Identificacion)
    VALUES (@Nombre, @Apellido, @FechaNacimiento, @Identificacion);
END;
GO

-- Actualizar Alumno
CREATE PROCEDURE sp_ActualizarAlumno
    @AlumnoId INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @FechaNacimiento DATE,
    @Identificacion NVARCHAR(20)
AS
BEGIN
    UPDATE Alumnos
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        FechaNacimiento = @FechaNacimiento,
        Identificacion = @Identificacion
    WHERE AlumnoId = @AlumnoId;
END;
GO

-- Eliminar Alumno
CREATE PROCEDURE sp_EliminarAlumno
    @AlumnoId INT
AS
BEGIN
    DELETE FROM Alumnos
    WHERE AlumnoId = @AlumnoId;
END;
GO

-- Obtener todos los Alumnos
CREATE PROCEDURE sp_ObtenerAlumnos
AS
BEGIN
    SELECT * FROM Alumnos;
END;
GO

-- Obtener Alumno por ID
CREATE PROCEDURE sp_ObtenerAlumnoPorId
    @AlumnoId INT
AS
BEGIN
    SELECT * FROM Alumnos
    WHERE AlumnoId = @AlumnoId;
END;
GO

-- ===========================================
-- 4. Stored Procedures CRUD Escuelas
-- ===========================================

-- Crear Escuela
CREATE PROCEDURE sp_CrearEscuela
    @Codigo NVARCHAR(20),
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(255)
AS
BEGIN
    INSERT INTO Escuelas (Codigo, Nombre, Descripcion)
    VALUES (@Codigo, @Nombre, @Descripcion);
END;
GO

-- Actualizar Escuela
CREATE PROCEDURE sp_ActualizarEscuela
    @EscuelaId INT,
    @Codigo NVARCHAR(20),
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(255)
AS
BEGIN
    UPDATE Escuelas
    SET Codigo = @Codigo,
        Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE EscuelaId = @EscuelaId;
END;
GO

-- Eliminar Escuela
CREATE PROCEDURE sp_EliminarEscuela
    @EscuelaId INT
AS
BEGIN
    DELETE FROM Escuelas
    WHERE EscuelaId = @EscuelaId;
END;
GO

-- Obtener todas las Escuelas
CREATE PROCEDURE sp_ObtenerEscuelas
AS
BEGIN
    SELECT * FROM Escuelas;
END;
GO

-- Obtener Escuela por ID
CREATE PROCEDURE sp_ObtenerEscuelaPorId
    @EscuelaId INT
AS
BEGIN
    SELECT * FROM Escuelas
    WHERE EscuelaId = @EscuelaId;
END;
GO

-- ===========================================
-- 5. Stored Procedures CRUD Profesores
-- ===========================================

-- Crear Profesor
CREATE PROCEDURE sp_CrearProfesor
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Identificacion NVARCHAR(20),
    @EscuelaId INT
AS
BEGIN
    INSERT INTO Profesores (Nombre, Apellido, Identificacion, EscuelaId)
    VALUES (@Nombre, @Apellido, @Identificacion, @EscuelaId);
END;
GO

-- Actualizar Profesor
CREATE PROCEDURE sp_ActualizarProfesor
    @ProfesorId INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Identificacion NVARCHAR(20),
    @EscuelaId INT
AS
BEGIN
    UPDATE Profesores
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        Identificacion = @Identificacion,
        EscuelaId = @EscuelaId
    WHERE ProfesorId = @ProfesorId;
END;
GO

-- Eliminar Profesor
CREATE PROCEDURE sp_EliminarProfesor
    @ProfesorId INT
AS
BEGIN
    DELETE FROM Profesores
    WHERE ProfesorId = @ProfesorId;
END;
GO

-- Obtener todos los Profesores
CREATE PROCEDURE sp_ObtenerProfesores
AS
BEGIN
    SELECT * FROM Profesores;
END;
GO

-- Obtener Profesor por ID
CREATE PROCEDURE sp_ObtenerProfesorPorId
    @ProfesorId INT
AS
BEGIN
    SELECT * FROM Profesores
    WHERE ProfesorId = @ProfesorId;
END;
GO

-- ===========================================
-- 6. Stored Procedures para Inscripciones y Asignaciones
-- ===========================================

-- Inscribir alumno en escuela
CREATE PROCEDURE sp_InscribirAlumnoEscuela
    @AlumnoId INT,
    @EscuelaId INT
AS
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM AlumnoEscuela 
        WHERE AlumnoId = @AlumnoId AND EscuelaId = @EscuelaId
    )
    BEGIN
        INSERT INTO AlumnoEscuela (AlumnoId, EscuelaId, FechaInscripcion)
        VALUES (@AlumnoId, @EscuelaId, GETDATE());
    END
    ELSE
    BEGIN
        RAISERROR('El alumno ya está inscrito en esta escuela.', 16, 1);
    END
END;
GO

-- Asignar alumno a profesor
CREATE PROCEDURE sp_AsignarAlumnoProfesor
    @AlumnoId INT,
    @ProfesorId INT
AS
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM AlumnoProfesor
        WHERE AlumnoId = @AlumnoId AND ProfesorId = @ProfesorId
    )
    BEGIN
        INSERT INTO AlumnoProfesor (AlumnoId, ProfesorId)
        VALUES (@AlumnoId, @ProfesorId);
    END
    ELSE
    BEGIN
        RAISERROR('El alumno ya está asignado a este profesor.', 16, 1);
    END
END;
GO

-- ===========================================
-- 7. Stored Procedures para Consultas
-- ===========================================

-- Consultar alumnos inscritos por profesor y mostrar escuela del profesor
CREATE PROCEDURE sp_AlumnosPorProfesorConEscuela
    @ProfesorId INT
AS
BEGIN
    SELECT
        a.AlumnoId,
        a.Nombre,
        a.Apellido,
        e.EscuelaId,
        e.Nombre AS NombreEscuela,
        e.Codigo,
        e.Descripcion
    FROM AlumnoProfesor ap
    INNER JOIN Alumnos a ON ap.AlumnoId = a.AlumnoId
    INNER JOIN Profesores p ON ap.ProfesorId = p.ProfesorId
    INNER JOIN Escuelas e ON p.EscuelaId = e.EscuelaId
    WHERE ap.ProfesorId = @ProfesorId;
END;
GO

-- Consultar todas las escuelas que imparte un profesor y los alumnos inscritos
CREATE PROCEDURE sp_EscuelasYAlumnosPorProfesor
    @ProfesorId INT
AS
BEGIN
    SELECT
        e.EscuelaId,
        e.Nombre AS NombreEscuela,
        e.Codigo,
        e.Descripcion,
        a.AlumnoId,
        a.Nombre AS NombreAlumno,
        a.Apellido AS ApellidoAlumno
    FROM Profesores p
    INNER JOIN Escuelas e ON p.EscuelaId = e.EscuelaId
    LEFT JOIN AlumnoProfesor ap ON ap.ProfesorId = p.ProfesorId
    LEFT JOIN Alumnos a ON a.AlumnoId = ap.AlumnoId
    WHERE p.ProfesorId = @ProfesorId;
END;
GO

-- ===========================================
-- Datos de Ejemplo (Inserts)
-- ===========================================

-- Insertar escuelas
INSERT INTO Escuelas (Codigo, Nombre, Descripcion) VALUES
('ESC001', 'Escuela Clásica', 'Escuela de música clásica'),
('ESC002', 'Escuela Moderna', 'Escuela de música moderna');
GO

-- Insertar profesores
INSERT INTO Profesores (Nombre, Apellido, Identificacion, EscuelaId) VALUES
('Julio', 'Quispe', 'ID123456', 1),
('Karina', 'Gonzales', 'ID654321', 2);
GO

-- Insertar alumnos
INSERT INTO Alumnos (Nombre, Apellido, FechaNacimiento, Identificacion) VALUES
('Kaleb', 'Acosta', '1992-04-22', 'ALU001'),
('Santiago', 'Carpio', '1995-10-15', 'ALU002');
GO

-- Asignar alumnos a profesores
INSERT INTO AlumnoProfesor (AlumnoId, ProfesorId) VALUES
(1, 1),
(2, 2);
GO

-- Inscribir alumnos en escuelas
INSERT INTO AlumnoEscuela (AlumnoId, EscuelaId) VALUES
(1, 1),
(2, 2);
GO
