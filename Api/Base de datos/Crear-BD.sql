USE Master

CREATE DATABASE P1700_2

GO
USE P1700_2

GO
CREATE TABLE Tiendas (
	IdTienda VARCHAR(50) NOT NULL PRIMARY KEY,
	Nombre VARCHAR(30) NOT NULL,
)


CREATE TABLE Perfil (
	IdPerfil VARCHAR(50) NOT NULL PRIMARY KEY DEFAULT(NEWID()),
	Descripcion VARCHAR(50) NOT NULL,
)

CREATE TABLE Permisos (
	IdPermiso VARCHAR(50) NOT NULL PRIMARY KEY DEFAULT(NEWID()),
	Descripcion VARCHAR(50) NOT NULL,
	Clave VARCHAR(50) NOT NULL
)

CREATE TABLE Usuarios (
	IdUsuario VARCHAR(50) NOT NULL PRIMARY KEY DEFAULT(NEWID()),
	IdTienda VARCHAR(50) NOT NULL,
	IdPerfil VARCHAR(50) NOT NULL,
	Identificacion VARCHAR(20) NOT NULL,
	Nombre VARCHAR(30) NOT NULL,
	Apellido1 VARCHAR(30) NOT NULL,
	Apellido2 VARCHAR(30) NOT NULL,
	Correo VARCHAR(100) NOT NULL,
	Contrasenna VARCHAR(100) NOT NULL,
	FechaCreacion DATETIME NOT NULL,
	UActualiza VARCHAR(50) NULL,
	FechaActualiza DATETIME NULL,

	FOREIGN KEY (IdTienda) REFERENCES Tiendas(IdTienda),
	FOREIGN KEY (IdPerfil) REFERENCES Perfil(IdPerfil),
)

CREATE TABLE PerfilPermisos (
	Id VARCHAR(50) NOT NULL PRIMARY KEY DEFAULT(NEWID()),
	IdPerfil VARCHAR(50) NOT NULL,
	IdPermiso VARCHAR(50) NOT NULL,

	FOREIGN KEY (IdPerfil) REFERENCES Perfil(IdPerfil),
	FOREIGN KEY (IdPermiso) REFERENCES Permisos(IdPermiso),
)

CREATE TABLE Empleados (
	IdEmpleado VARCHAR(50) NOT NULL PRIMARY KEY DEFAULT(NEWID()),
	IdTienda VARCHAR(50) NOT NULL,
	Identificacion VARCHAR(15) NOT NULL,
	Nombre VARCHAR(30) NOT NULL,
	Apellido1 VARCHAR(30) NOT NULL,
	Apellido2 VARCHAR(30) NOT NULL,
	Telefono VARCHAR(20) NOT NULL,
	TipoEmpleado VARCHAR(50) NOT NULL,
	IdSupervisor VARCHAR(50) NOT NULL,
	Salario DECIMAL(10,2) NOT NULL,
	UCreador VARCHAR(50) NULL,
	FechaCreacion DATETIME NOT NULL,
	UActualiza VARCHAR(50) NULL,
	FechaActualiza DATETIME NULL,
	IndEliminado BIT NOT NULL,

	FOREIGN KEY (IdTienda) REFERENCES Tiendas(IdTienda),
	FOREIGN KEY (UCreador) REFERENCES Usuarios(IdUsuario),
	FOREIGN KEY (UActualiza) REFERENCES Usuarios(IdUsuario),
	FOREIGN KEY (IdSupervisor) REFERENCES Empleados(IdEmpleado),
)

CREATE TABLE TiendasEmpleados (
	Id VARCHAR(50) NOT NULL PRIMARY KEY,
	IdTienda VARCHAR(50) NOT NULL,
	IdEmpleado VARCHAR(50) NOT NULL,
	Fecha DATE NOT NULL,

	FOREIGN KEY (IdTienda) REFERENCES Tiendas(IdTienda),
	FOREIGN KEY (IdEmpleado) REFERENCES Empleados(IdEmpleado),
	CONSTRAINT EmpleadoXFecha UNIQUE (IdEmpleado, Fecha)
)


GO
CREATE PROCEDURE ObtenerEmpleados
	@IdEmpleado VARCHAR(50)
AS
BEGIN
	
	IF (@IdEmpleado IS NOT NULL AND @IdEmpleado != '')
	BEGIN
		SELECT 
		CONCAT_WS(' ', emp.Nombre, emp.Apellido1, emp.Apellido1) AS 'Nombre del Empleado',
		emp.Telefono AS 'Teléfono',
		emp.FechaCreacion AS 'Fecha Creación',
		emp.TipoEmpleado AS 'Tipo de empleado',
		emp.Salario AS 'Salario',
		(SELECT CONCAT_WS(' ', empS.Nombre, empS.Apellido1, empS.Apellido1) 
			FROM Empleados empS 
			WHERE  empS.IdEmpleado = emp.IdSupervisor) AS 'Nombre del Supervidor'
		FROM Empleados emp
		WHERE emp.IdEmpleado = @IdEmpleado
	END
	ELSE
	BEGIN
		SELECT 
		CONCAT_WS(' ', emp.Nombre, emp.Apellido1, emp.Apellido1) AS 'Nombre del Empleado',
		emp.Telefono AS 'Teléfono',
		emp.FechaCreacion AS 'Fecha Creación',
		emp.TipoEmpleado AS 'Tipo de empleado',
		emp.Salario AS 'Salario',
		(SELECT CONCAT_WS(' ', empS.Nombre, empS.Apellido1, empS.Apellido1) 
			FROM Empleados empS 
			WHERE  empS.IdEmpleado = emp.IdSupervisor) AS 'Nombre del Supervidor'
		FROM Empleados emp
	END
END;

GO
CREATE PROCEDURE ObtenerCantidadEmpleadosSupervisado
AS
BEGIN
	SELECT CONCAT_WS(' ', emp.Nombre, emp.Apellido1, emp.Apellido1) AS 'Nombre del Empleado',
	(SELECT COUNT(empS.IdEmpleado)
			FROM Empleados empS 
			WHERE  empS.IdEmpleado = emp.IdEmpleado) AS 'Cantidad de Empleados Supervisados'
	FROM Empleados emp
END;
