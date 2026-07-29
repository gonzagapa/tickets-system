-- Procedimientos almacenados



--Obtener Ticket 
Create proc p_ObtenerTicket(
	@IdTicket int
) 
as 
begin
	select titulo, descripccion, estatus, Ti.fechaCreacion
	from Tickets Ti
	inner join DocumentosAdjuntos DA on DA.ticketId = Ti.idTicket
	where estaActivo = 1 AND Ti.idTicket = @IdTicket
end

-- Obtener lista de Tickets
Create proc p_ObtenerListaTickets
as 
begin
	select titulo, descripccion, estatus, Ti.fechaCreacion
	from Tickets Ti
	where estaActivo = 1;
end

-- Actualiza estatus del ticket = 'Abierto', 'Cerrado' o 'Trabajando'
Create proc p_ActualizarTicket(
	@idTicket int,
	@estatus varchar(30)
)
as 
begin
	update Tickets set estatus = @estatus  where idTicket = @idTicket
end

-- Aplicar un soft delete a los tickets 
Create proc p_EliminarTicket(
	@idTicket int
)
as 
begin 
	update Tickets set estaActivo = 0 where idTicket = @idTicket;
end

-- Aplicar un hard delete a los tickets
Create proc p_EliminarTicketDuro(
	@idTicket int
)
as 
begin 
	delete from Tickets where idTicket = @idTicket;
end 

-- Crear Ticket
Create proc p_CrearTicket(
	@titulo nvarchar(300),
	@descripcion nvarchar(max),
	@estatus nvarchar(30)
)
as 
begin 
	insert into Tickets(titulo, descripccion, estatus) values(@titulo, @descripcion, @estatus)
end

-- Guardar documento
Create proc p_GuardarDocumento(
	@ruta varchar(300),
	@nombreOriginal varchar(300),
	@ticketId int
)
as 
begin
	insert into DocumentosAdjuntos(ruta,nombreOriginal,ticketId) values (@ruta, @nombreOriginal, @ticketId);
end
