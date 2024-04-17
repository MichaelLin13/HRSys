
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/17/2024 10:22:33
-- Generated from EDMX file: D:\Microsoft\MVC\HRSys\HRSys\HRSys\Models\HRSysModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HRSys];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'USRFORMS'
CREATE TABLE [dbo].[USRFORMS] (
    [UsrId] nvarchar(10)  NOT NULL,
    [UsrPSW] nvarchar(10)  NOT NULL,
    [Employed] bit  NOT NULL
);
GO

-- Creating table 'HolidayPublics'
CREATE TABLE [dbo].[HolidayPublics] (
    [HolidayPublicId] int IDENTITY(1,1) NOT NULL,
    [PDate] datetime  NOT NULL
);
GO

-- Creating table 'HolidaySelfs'
CREATE TABLE [dbo].[HolidaySelfs] (
    [HolidaySelfId] int IDENTITY(1,1) NOT NULL,
    [SDate] datetime  NOT NULL,
    [UsrId] nvarchar(10)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UsrId] in table 'USRFORMS'
ALTER TABLE [dbo].[USRFORMS]
ADD CONSTRAINT [PK_USRFORMS]
    PRIMARY KEY CLUSTERED ([UsrId] ASC);
GO

-- Creating primary key on [HolidayPublicId] in table 'HolidayPublics'
ALTER TABLE [dbo].[HolidayPublics]
ADD CONSTRAINT [PK_HolidayPublics]
    PRIMARY KEY CLUSTERED ([HolidayPublicId] ASC);
GO

-- Creating primary key on [HolidaySelfId] in table 'HolidaySelfs'
ALTER TABLE [dbo].[HolidaySelfs]
ADD CONSTRAINT [PK_HolidaySelfs]
    PRIMARY KEY CLUSTERED ([HolidaySelfId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------