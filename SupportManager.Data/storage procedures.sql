-- Procedimientos almacenados
drop proc p_ObtenerTicket;
drop proc p_ObtenerListaTickets;
drop proc p_ActualizarTicket;
drop proc p_EliminarTicket;
drop proc p_EliminarTicketDuro;
drop proc p_CrearTicket;
drop proc p_GuardarDocumento;

--Obtener Ticket 
Create proc p_ObtenerTicket(
	@IdTicket int
) 
as 
begin
	set nocount on;

	select titulo, descripccion, estatus, Ti.fechaCreacion, latitud, longitud, Ti.idTicket
	from Tickets Ti
	where estaActivo = 1 AND Ti.idTicket = @IdTicket

	select id, ruta, nombreOriginal, fechaCreacion
	from DocumentosAdjuntos
	where ticketId = @IdTicket;
end

-- Obtener lista de Tickets
Create proc p_ObtenerListaTickets
as 
begin
	set nocount on;
	select titulo, descripccion, estatus, Ti.fechaCreacion, latitud, longitud, Ti.idTicket 
	from Tickets Ti
	where estaActivo = 1;
end

-- Actualiza estatus del ticket = 'Abierto', 'Cerrado' o 'Trabajando'
Create proc p_ActualizarEstatusTicket(
	@idTicket int,
	@estatus varchar(30)
)
as 
begin
	set nocount on;
	update Tickets set estatus = @estatus  where idTicket = @idTicket
end

-- Aplicar un soft delete a los tickets 
Create proc p_EliminarTicket(
	@idTicket int
)
as 
begin
	set nocount on;
	update Tickets set estaActivo = 0 where idTicket = @idTicket;
end

-- Aplicar un hard delete a los tickets
Create proc p_EliminarTicketDuro(
	@idTicket int
)
as 
begin 
	set nocount on;
	delete from Tickets where idTicket = @idTicket;
end 

-- Crear Ticket
Create proc p_CrearTicket(
	@titulo nvarchar(300),
	@descripcion nvarchar(max),
	@estatus nvarchar(30),
	@latitud decimal(10,8),
	@longitud decimal(11,8)
)
as 
begin 
	set nocount on;
	insert into Tickets(titulo, descripccion, estatus,latitud, longitud) values(@titulo, @descripcion, @estatus,@latitud, @longitud);
	select SCOPE_IDENTITY();
end

-- Guardar documento
Create proc p_GuardarDocumento(
	@ruta varchar(300),
	@nombreOriginal varchar(300),
	@ticketId int
)
as 
begin
	set nocount on;
	insert into DocumentosAdjuntos(ruta,nombreOriginal,ticketId) values (@ruta, @nombreOriginal, @ticketId);
end 

exec sp_rename 'p_ActualizarTicket', 'p_ActualizarEstatusTicket';
