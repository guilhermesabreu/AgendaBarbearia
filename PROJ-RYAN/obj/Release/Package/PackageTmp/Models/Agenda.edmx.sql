
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/27/2019 13:16:37
-- Generated from EDMX file: C:\Users\userd\OneDrive\Área de Trabalho\SolutionsWebC#\BarberShop-Tournment-master\BarberShop-Tournment-master\PROJ-RYAN\Models\Agenda.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [barbearia];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Agenda]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Agenda];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Agenda'
CREATE TABLE [dbo].[Agenda] (
    [ClienteId] int IDENTITY(1,1) NOT NULL,
    [Nome] varchar(200)  NOT NULL,
    [Email] varchar(100)  NULL,
    [DataHora] datetime  NOT NULL,
    [Hora] time  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ClienteId] in table 'Agenda'
ALTER TABLE [dbo].[Agenda]
ADD CONSTRAINT [PK_Agenda]
    PRIMARY KEY CLUSTERED ([ClienteId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------