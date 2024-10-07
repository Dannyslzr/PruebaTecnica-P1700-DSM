USE Master
GO
USE P1700_2

----Tiendas
INSERT INTO Tiendas (IdTienda, Nombre) VALUES ('68061DA2-DE7D-446A-9732-E897340A0672', 'Zapote');
INSERT INTO Tiendas (IdTienda, Nombre) VALUES ('6A6BD902-EA3E-40A4-A49B-5AFAC681A349', 'San Pedro');
INSERT INTO Tiendas (IdTienda, Nombre) VALUES ('B2D9C151-3977-436A-8B1B-1A6987D58BAB', 'Heredia');
INSERT INTO Tiendas (IdTienda, Nombre) VALUES ('BD3AD5B1-76AF-41AD-A39B-CC2E58B487B9', 'San José');

----Permisos
INSERT INTO Permisos (IdPermiso, Descripcion, Clave) VALUES ('87B38624-08BF-4476-8C28-8C41A1F97633', 'Registro de Empleados', 'RegEmp');
INSERT INTO Permisos (IdPermiso, Descripcion, Clave) VALUES ('4DD9184B-B23E-446E-9193-5E6B424301C1', 'Consulta de Empleados', 'ConEmp');
INSERT INTO Permisos (IdPermiso, Descripcion, Clave) VALUES ('DE9A80B4-9E02-4311-A389-A864A33E9392', 'Consulta de Perfiles', 'ConPer');
	  
----Perfiles
INSERT INTO Perfil (IdPerfil, Descripcion) VALUES ('29C2C836-B383-4EAE-933D-022AC4EF4715', 'Administrador');
INSERT INTO Perfil (IdPerfil, Descripcion) VALUES ('0C1BC772-4686-4995-BF48-681363C44BEC', 'Encargado');
INSERT INTO Perfil (IdPerfil, Descripcion) VALUES ('5C9D072B-BF74-4E1C-B978-B8261D613649', 'Auditoria');

----Permisos por perfil
INSERT INTO PerfilPermisos (Id,IdPerfil, IdPermiso) VALUES ('C731E2EC-8DB9-403C-9887-E84A2C5A3427','29C2C836-B383-4EAE-933D-022AC4EF4715','87B38624-08BF-4476-8C28-8C41A1F97633');
INSERT INTO PerfilPermisos (Id,IdPerfil, IdPermiso) VALUES ('EC9BE004-8C85-4BAE-8FB1-DB264A8F580D','29C2C836-B383-4EAE-933D-022AC4EF4715','4DD9184B-B23E-446E-9193-5E6B424301C1');
INSERT INTO PerfilPermisos (Id,IdPerfil, IdPermiso) VALUES ('B6AB6F41-F385-437E-9378-6AF2F205C51C','29C2C836-B383-4EAE-933D-022AC4EF4715','DE9A80B4-9E02-4311-A389-A864A33E9392');
INSERT INTO PerfilPermisos (Id,IdPerfil, IdPermiso) VALUES ('A0536C46-E10E-4981-88EC-99E4135E6469','0C1BC772-4686-4995-BF48-681363C44BEC','4DD9184B-B23E-446E-9193-5E6B424301C1');
INSERT INTO PerfilPermisos (Id,IdPerfil, IdPermiso) VALUES ('C6AA9E5A-E3DF-4494-84F3-88D7C83C2AF4','5C9D072B-BF74-4E1C-B978-B8261D613649','DE9A80B4-9E02-4311-A389-A864A33E9392');

----Usuario
INSERT INTO Usuarios (IdUsuario, IdTienda, IdPerfil, Identificacion, Nombre, Apellido1, Apellido2, Correo, Contrasenna, FechaCreacion, UActualiza, FechaActualiza) 
VALUES ('3AA74754-C859-422D-BB96-D6A4BF91EDA0', 'BD3AD5B1-76AF-41AD-A39B-CC2E58B487B9', '29C2C836-B383-4EAE-933D-022AC4EF4715', '123456789', 'Danny', 'Salazar', 'Mendez', 'danny_slzr@gmail.com', 'BOIOErxLj3o=', '2024-10-06', NULL, NULL);

----Empleado
INSERT INTO Empleados (IdEmpleado, IdTienda, Identificacion, Nombre, Apellido1, Apellido2, Telefono, TipoEmpleado, IdSupervisor, Salario, UCreador, FechaCreacion, UActualiza, FechaActualiza, IndEliminado) 
VALUES ('6C8967F0-04BF-455C-A3CF-1B7417643845', 'BD3AD5B1-76AF-41AD-A39B-CC2E58B487B9', '12345678', 'Juan', 'Pérez', 'García', '88888888', 'Gerente', '6C8967F0-04BF-455C-A3CF-1B7417643845', 3500.00, '3AA74754-C859-422D-BB96-D6A4BF91EDA0', '2024-10-06', NULL, NULL, 0);

INSERT INTO Empleados (IdEmpleado, IdTienda, Identificacion, Nombre, Apellido1, Apellido2, Telefono, TipoEmpleado, IdSupervisor, Salario, UCreador, FechaCreacion, UActualiza, FechaActualiza, IndEliminado) 
VALUES ('304302BF-E9DE-471E-B17F-8C7EA324FF68', 'BD3AD5B1-76AF-41AD-A39B-CC2E58B487B9', '87654321', 'María', 'López', 'Rodríguez', '88888888', 'Gerente', '6C8967F0-04BF-455C-A3CF-1B7417643845', 2800.00, '3AA74754-C859-422D-BB96-D6A4BF91EDA0', '2024-10-06', NULL, NULL, 0);

INSERT INTO Empleados (IdEmpleado, IdTienda, Identificacion, Nombre, Apellido1, Apellido2, Telefono, TipoEmpleado, IdSupervisor, Salario, UCreador, FechaCreacion, UActualiza, FechaActualiza, IndEliminado) 
VALUES ('BBF681D3-1BBD-4D97-9EFE-ED93E7F08224', 'BD3AD5B1-76AF-41AD-A39B-CC2E58B487B9', '98765432', 'Carlos', 'Martínez', 'Hernández', '88888888', 'Cajero', '304302BF-E9DE-471E-B17F-8C7EA324FF68', 2800.00, '3AA74754-C859-422D-BB96-D6A4BF91EDA0', '2024-10-06', NULL, NULL, 0);

INSERT INTO Empleados (IdEmpleado, IdTienda, Identificacion, Nombre, Apellido1, Apellido2, Telefono, TipoEmpleado, IdSupervisor, Salario, UCreador, FechaCreacion, UActualiza, FechaActualiza, IndEliminado) 
VALUES ('579D8066-FE89-4F11-9C89-3463AA676053', 'BD3AD5B1-76AF-41AD-A39B-CC2E58B487B9', '54321678', 'Luis', 'Sánchez', 'Vega', '88888888', 'Cajero', '6C8967F0-04BF-455C-A3CF-1B7417643845', 2800.00, '3AA74754-C859-422D-BB96-D6A4BF91EDA0', '2024-10-06', NULL, NULL, 0);

INSERT INTO Empleados (IdEmpleado, IdTienda, Identificacion, Nombre, Apellido1, Apellido2, Telefono, TipoEmpleado, IdSupervisor, Salario, UCreador, FechaCreacion, UActualiza, FechaActualiza, IndEliminado) 
VALUES ('66FE2697-1A75-4ADF-B977-C8A8EE9CCFCA', 'BD3AD5B1-76AF-41AD-A39B-CC2E58B487B9', '54321678', 'Ana', 'González', 'Ramírez', '88888888', 'Cajero', '6C8967F0-04BF-455C-A3CF-1B7417643845', 2800.00, '3AA74754-C859-422D-BB96-D6A4BF91EDA0', '2024-10-06', NULL, NULL, 0);

INSERT INTO Empleados (IdEmpleado, IdTienda, Identificacion, Nombre, Apellido1, Apellido2, Telefono, TipoEmpleado, IdSupervisor, Salario, UCreador, FechaCreacion, UActualiza, FechaActualiza, IndEliminado) 
VALUES ('85A5A8D7-5077-44A8-93EC-8A52F5DBD392', 'B2D9C151-3977-436A-8B1B-1A6987D58BAB', '98765432', 'Carlos', 'Pérez', 'López', '77777777', 'Cajero', '6C8967F0-04BF-455C-A3CF-1B7417643845', 2500.00, '3AA74754-C859-422D-BB96-D6A4BF91EDA0', '2024-10-06', NULL, NULL, 0);

INSERT INTO Empleados (IdEmpleado, IdTienda, Identificacion, Nombre, Apellido1, Apellido2, Telefono, TipoEmpleado, IdSupervisor, Salario, UCreador, FechaCreacion, UActualiza, FechaActualiza, IndEliminado) 
VALUES ('FAF03C72-62B8-414E-A556-39DE78DFF9BD', '6A6BD902-EA3E-40A4-A49B-5AFAC681A349', '87654321', 'Lucía', 'Rodríguez', 'Sánchez', '66666666', 'Cajero', '6C8967F0-04BF-455C-A3CF-1B7417643845', 4000.00, '3AA74754-C859-422D-BB96-D6A4BF91EDA0', '2024-10-06', NULL, NULL, 0);

INSERT INTO Empleados (IdEmpleado, IdTienda, Identificacion, Nombre, Apellido1, Apellido2, Telefono, TipoEmpleado, IdSupervisor, Salario, UCreador, FechaCreacion, UActualiza, FechaActualiza, IndEliminado) 
VALUES ('49D5A68D-9204-4BA5-B620-A9FE41A1A7C6', 'BD3AD5B1-76AF-41AD-A39B-CC2E58B487B9', '65432198', 'Miguel', 'Fernández', 'Castillo', '55555555', 'Cajero', '6C8967F0-04BF-455C-A3CF-1B7417643845', 2800.00, '3AA74754-C859-422D-BB96-D6A4BF91EDA0', '2024-10-06', NULL, NULL, 0);

INSERT INTO Empleados (IdEmpleado, IdTienda, Identificacion, Nombre, Apellido1, Apellido2, Telefono, TipoEmpleado, IdSupervisor, Salario, UCreador, FechaCreacion, UActualiza, FechaActualiza, IndEliminado) 
VALUES ('0BFD165F-8174-4D89-B58C-964AB3079944', 'BD3AD5B1-76AF-41AD-A39B-CC2E58B487B9', '43219876', 'Valeria', 'García', 'Ortiz', '44444444', 'Cajero', '6C8967F0-04BF-455C-A3CF-1B7417643845', 3500.00, '3AA74754-C859-422D-BB96-D6A4BF91EDA0', '2024-10-06', NULL, NULL, 0);

INSERT INTO Empleados (IdEmpleado, IdTienda, Identificacion, Nombre, Apellido1, Apellido2, Telefono, TipoEmpleado, IdSupervisor, Salario, UCreador, FechaCreacion, UActualiza, FechaActualiza, IndEliminado) 
VALUES ('24FA2C91-8F86-40CC-8B2E-1CC5C2908B3A', '6A6BD902-EA3E-40A4-A49B-5AFAC681A349', '65498732', 'Jorge', 'Méndez', 'Rivas', '33333333', 'Cajero', '6C8967F0-04BF-455C-A3CF-1B7417643845', 2600.00, '3AA74754-C859-422D-BB96-D6A4BF91EDA0', '2024-10-06', NULL, NULL, 0);

INSERT INTO Empleados (IdEmpleado, IdTienda, Identificacion, Nombre, Apellido1, Apellido2, Telefono, TipoEmpleado, IdSupervisor, Salario, UCreador, FechaCreacion, UActualiza, FechaActualiza, IndEliminado) 
VALUES ('947FF4A2-7FB7-439D-817C-0D9AF8E1C4A4', 'B2D9C151-3977-436A-8B1B-1A6987D58BAB', '78965432', 'Sofía', 'Ramírez', 'Gómez', '22222222', 'Cajero', '6C8967F0-04BF-455C-A3CF-1B7417643845', 2900.00, '3AA74754-C859-422D-BB96-D6A4BF91EDA0', '2024-10-06', NULL, NULL, 0);

INSERT INTO Empleados (IdEmpleado, IdTienda, Identificacion, Nombre, Apellido1, Apellido2, Telefono, TipoEmpleado, IdSupervisor, Salario, UCreador, FechaCreacion, UActualiza, FechaActualiza, IndEliminado) 
VALUES ('B3B946F2-9D9D-4701-888C-814624A5315D', 'BD3AD5B1-76AF-41AD-A39B-CC2E58B487B9', '32198745', 'Raúl', 'Santos', 'Torres', '11111111', 'Cajero', '49D5A68D-9204-4BA5-B620-A9FE41A1A7C6', 2100.00, '3AA74754-C859-422D-BB96-D6A4BF91EDA0', '2024-10-06', NULL, NULL, 0);

INSERT INTO Empleados (IdEmpleado, IdTienda, Identificacion, Nombre, Apellido1, Apellido2, Telefono, TipoEmpleado, IdSupervisor, Salario, UCreador, FechaCreacion, UActualiza, FechaActualiza, IndEliminado) 
VALUES ('14D0379F-2743-48DB-BB8B-3C3B5D8839F6', 'B2D9C151-3977-436A-8B1B-1A6987D58BAB', '87651234', 'Gabriela', 'Martínez', 'Morales', '99999999', 'Cajero', '6C8967F0-04BF-455C-A3CF-1B7417643845', 3700.00, '3AA74754-C859-422D-BB96-D6A4BF91EDA0', '2024-10-06', NULL, NULL, 0);

INSERT INTO Empleados (IdEmpleado, IdTienda, Identificacion, Nombre, Apellido1, Apellido2, Telefono, TipoEmpleado, IdSupervisor, Salario, UCreador, FechaCreacion, UActualiza, FechaActualiza, IndEliminado) 
VALUES ('C1685B92-8492-4A2A-87E9-78C7C1A2C3FB', '68061DA2-DE7D-446A-9732-E897340A0672', '98732145', 'Manuel', 'Gómez', 'Salazar', '12345678', 'Cajero', '6C8967F0-04BF-455C-A3CF-1B7417643845', 2700.00, '3AA74754-C859-422D-BB96-D6A4BF91EDA0', '2024-10-06', NULL, NULL, 0);