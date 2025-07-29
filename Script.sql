-- Verificar y crear la base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'DBPrueba')
BEGIN
    CREATE DATABASE DBPrueba
END
GO

-- Usar la base de datos
USE [MiBaseDeDatos]
GO

-- Verificar y crear la tabla Usuario si no existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
   CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Correo] [varchar](70) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[EsBloqueado] [bit] NOT NULL
) ON [PRIMARY]
END

-- Verificar y crear la tabla UsuarioIntento si no existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'UsuarioIntento' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
  CREATE TABLE [dbo].[UsuarioIntento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[Intentos] [int] NULL,
	[Bloqueado] [bit] NULL,
	[FechaBloqueo] [datetime] NULL
) ON [PRIMARY]
END

-- Verificar y crear la tabla Producto si no existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Producto' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
 CREATE TABLE [dbo].[Producto](
	[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Marcas] [varchar](50) NULL,
	[Precio] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END