/*************************/
-- AUTOR: JUAN ALBERTO VILLEGAS NAVA
-- EJERCICIO 1
-- FECHA: 05/04/2017
-- DESCRIPCION: Crear un Sitio Web que será un paginador de registros.
-- Deberá crear un WCF el cual traerá los registros de la base de datos de 50 en 50 de la tabla Factura. 
-- La paginación deberá hacerse desde un Stored Procedure.
-- Email: alberto_ville@hotmail.com
/**************************/

-- drop db
use master;
GO
if db_id('Ejercicios') is not null
	DROP DATABASE [Ejercicios]

-- Se crea la base de datos
CREATE DATABASE [Ejercicios];
GO
USE Ejercicios;
Go

-- se crea el schema
CREATE SCHEMA [ej1]
GO

-- Se crea la tabla contribuyentes
CREATE TABLE [ej1].[Contribuyentes]
(
	Id int identity(1,1) primary key,
	Nombre nvarchar(100) not null,
	RFC nvarchar(13) not null unique,
	Domicilio nvarchar(100) not null
);

-- Se crea la tabla Facturas
CREATE TABLE [ej1].[Facturas](
	Id int identity(1,1) primary key,
	Folio varchar(10) unique not null,
	LugarExpedicion nvarchar(100) not null,
	FechaExpedicion datetime,
	RFC_Emisor nvarchar(13) not null,
	RFC_Receptor nvarchar(13) not null
	foreign key(RFC_Emisor) REFERENCES ej1.Contribuyentes(RFC),
	foreign key(RFC_Receptor) REFERENCES ej1.Contribuyentes(RFC)
	);
GO

-- Se crea la tabla Conceptos
CREATE TABLE [ej1].[Conceptos](
	Id int identity(1,1) primary key,
	Descripcion nvarchar(100) not null,
	Importe decimal not null default 0,
	Cantidad decimal not null default 0,
	IdFactura int,
	foreign key(IdFactura) references ej1.Facturas(Id)
);
GO

-- StoreProcedure que crea facturas a mostrar
CREATE PROCEDURE [ej1].[usp_insertarFacturasDePrueba]
	AS
	BEGIN TRAN
	BEGIN TRY

	-- se insertan los contribuyentes
	insert into ej1.Contribuyentes values('PROVEDOR 1 S.A. DE C.V.', 'PROV100110B12', 'CALLE BENITO JUAREZ #5 CIUDAD DE MEXICO');
	insert into ej1.Contribuyentes values('EMPRESA 2 S.A. DE S.R.L','EMPR090501V10', 'DOMICILIO CONOCIDO #5 CIUDAD DE MEXICO');
	
	DECLARE @i as int = 1;	

	-- se insertan 200 facturas
	WHILE @i <= 200
	BEGIN
		-- lugar de expedicion
		DECLARE @lugar as nvarchar(100);
		SET @lugar = CASE WHEN ((@i%3)= 0) THEN N'CIUDAD DE MEXICO' ELSE N'ACAPULCO GUERRERO' END;

		-- se almacena el id de la factura insertada
		DECLARE @factura as table(id int);
		insert into ej1.Facturas 
		output inserted.Id into @factura(id)
		values(
			 CONCAT('F', FORMAT(@i,'00000000#')),
			 @lugar, 
			 CURRENT_TIMESTAMP,
			'PROV100110B12', 
			'EMPR090501V10'); 
		
		-- se agregan 3 conceptos
		DECLARE @x as int = 1;
		WHILE @x <= 3
		BEGIN
		insert into ej1.Conceptos values(
			CONCAT('SERVICIO ', @x),
			(@i * 100),
			@x,
			(select top 1 id from @factura order by id desc));
			SET @x = @x + 1;
		END

	SET @i = @i + 1;
	END;

	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH
	
	IF @@TRANCOUNT > 0
		COMMIT
 GO

-- Se crea store para obtener la lista de facturas
CREATE PROCEDURE [ej1].[usp_GetFacturas](@pageSize int, @pageNumber int)
AS 
 select * from ej1.Facturas 
 ORDER BY Id 
 OFFSET (@pageSize * (@pageNumber - 1)) 
 ROWS FETCH NEXT @pageSize ROWS ONLY;
GO

-- Se crea TVF para obtener el detalle de una factura
CREATE FUNCTION [ej1].[tvf_GetFacturaDetalle](@folio varchar(10))
returns table
as return (
	select 
	f.Id  as Id,
	f.Folio as Folio,
	f.FechaExpedicion as FechaExpedicion,
	f.LugarExpedicion as LugarExpedicion,
	e.Id as Emisor_Id,
	f.RFC_Emisor as Emisor_RFC,
	e.Nombre as Emisor_Nombre,
	e.Domicilio as Emisor_Domicilio,
	r.Id as Receptor_Id,
	f.RFC_Receptor as Receptor_RFC,
	r.Nombre as Receptor_Nombre,
	r.Domicilio as Receptor_Domicilio
	from ej1.Facturas as f
	join ej1.Contribuyentes as e
	on f.RFC_Emisor = e.RFC
	join ej1.Contribuyentes as r
	on f.RFC_Receptor = r.RFC
	where f.Folio = @folio
	);
GO

-- Se crea tvf para obtener los conceptos de una factura
CREATE FUNCTION [ej1].[tvf_GetFacturaConceptos](@folio varchar(10))
returns table
as return(
	select 
		f.Id as Factura_Id,
		Folio as Factura_Folio,
		c.Id as Id,
		Descripcion,
		Importe,
		Cantidad
	from Facturas as f
	join Conceptos as c
	on f.Id = c.IdFactura
	where f.Folio = @folio);
GO


 -- Ejecuta storeProcedure que crea las facturas de prueba
EXEC ej1.usp_insertarFacturasDePrueba
GO

-- Ejecuta storeProcedure que obtiene las facturas 
EXEC [ej1].[usp_GetFacturas] 20, 1;
GO

-- Ejecuta tvf que obtiene el detalle de una factura
SELECT * FROM [ej1].[tvf_GetFacturaDetalle]('F000000001');
GO

-- Ejecuta tvf que obtiene los conceptos de una factura
SELECT * FROM [ej1].[tvf_GetFacturaConceptos]('F000000001');
GO