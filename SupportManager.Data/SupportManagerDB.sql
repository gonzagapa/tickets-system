use SupportManagerDb;

create table Tickets  (
	idTicket int identity primary key, 
	titulo nvarchar(300) not null,
	descripccion nvarchar(max) not null constraint check_descripccion_vacio check (len(descripccion) > 3), 
	estatus nvarchar(30) default 'Abierto' constraint check_estatus_ticket check(estatus = 'Abierto' OR estatus = 'Cerrado' OR estatus = 'Trabajando' ),
	fechaCreacion datetime not null 
); 

create table DocumentosAdjuntos(
	id uniqueidentifier DEFAULT NEWID() primary key,
	ruta varchar(300) not null, 
	nombreOriginal varchar(300) not null,
	fechaCreacion datetime not null, 
	ticketId int not null, 
	constraint fk_ticketId_DocumentosAdjuntos foreign key (ticketId) references Tickets(idTicket) on delete cascade
);

-- Modificaciones 
alter table Tickets add estaActivo bit not null default 1; 
alter table Tickets add constraint default_datetime default Getdate() for fechaCreacion;

alter table DocumentosAdjuntos add constraint datetime_default default GetDate() for fechaCreacion;

-- Agregar on delete cascade en DocumentosAdjuntos
alter table DocumentosAdjuntos drop constraint fk_ticketId_DocumentosAdjuntos;
alter table DocumentosAdjuntos add constraint fk_ticketId_DocumentosAdjuntos foreign key (ticketId) references Tickets(idTicket)
	ON DELETE CASCADE;

-- Datos de prueba para Tickets
INSERT INTO Tickets (titulo, descripccion, estatus, fechaCreacion, estaActivo) 
VALUES 
('Error al iniciar sesión', 'El usuario reporta que no puede acceder al sistema con sus credenciales.', 'Abierto', GETDATE(), 1),
('Impresora sin conexión', 'La impresora del departamento de contabilidad no responde.', 'Trabajando', GETDATE(), 1),
('Pantalla azul en equipo', 'El equipo del director general muestra una pantalla azul al encender.', 'Abierto', GETDATE(), 1),
('Actualización de software', 'Se requiere instalar la última versión de Office.', 'Cerrado', GETDATE(), 1),
('Falla en la red WiFi', 'Problemas de intermitencia en la red inalámbrica del segundo piso.', 'Trabajando', GETDATE(), 1);

-- Datos de prueba para DocumentosAdjuntos asociados a los tickets anteriores
INSERT INTO DocumentosAdjuntos (ruta, nombreOriginal, fechaCreacion, ticketId)
VALUES
('/uploads/evidencia1.png', 'captura_error.png', GETDATE(), 1),
('/uploads/evidencia2.jpg', 'foto_impresora.jpg', GETDATE(), 2);
