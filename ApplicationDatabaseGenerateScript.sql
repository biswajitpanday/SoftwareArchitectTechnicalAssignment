USE [master]
GO
/****** Object:  Database [ApplicationDatabase]    Script Date: 11/21/2020 4:41:02 AM ******/
CREATE DATABASE [ApplicationDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ApplicationDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ApplicationDatabase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ApplicationDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ApplicationDatabase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ApplicationDatabase] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ApplicationDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ApplicationDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ApplicationDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ApplicationDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ApplicationDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ApplicationDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ApplicationDatabase] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ApplicationDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ApplicationDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [ApplicationDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ApplicationDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ApplicationDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ApplicationDatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ApplicationDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ApplicationDatabase] SET QUERY_STORE = OFF
GO
USE [ApplicationDatabase]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/21/2020 4:41:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 11/21/2020 4:41:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](max) NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[CurrencyCode] [nvarchar](max) NULL,
	[TransactionDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NULL,
	[OutputStatus] [nvarchar](max) NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [ApplicationDatabase] SET  READ_WRITE 
GO
