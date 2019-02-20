USE [FriendlyFisherman]
GO

ALTER TABLE [dbo].[AspNetUsers]
ADD FirstName nvarchar(256) NULL;
GO

ALTER TABLE [dbo].[AspNetUsers]
ADD LastName nvarchar(256) NULL;
GO