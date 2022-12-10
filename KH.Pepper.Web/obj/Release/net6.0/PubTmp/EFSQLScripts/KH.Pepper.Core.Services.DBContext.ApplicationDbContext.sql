IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE TABLE [dbo].[AdminUser] (
        [Id] int NOT NULL IDENTITY,
        [UserName] nvarchar(50) NOT NULL,
        [Password] nvarchar(50) NOT NULL,
        [IsActive] bit NULL,
        CONSTRAINT [PK_AdminUser] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE TABLE [dbo].[ContactUs] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(50) NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [EmailAddress] nvarchar(max) NOT NULL,
        [Descriptions] nvarchar(max) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_ContactUs] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE TABLE [dbo].[Product] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [ImageName] nvarchar(max) NULL,
        [ImagePath] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Benefits] nvarchar(max) NULL,
        [Tips] nvarchar(max) NULL,
        [StackAvailable] bit NULL,
        [IsActive] bit NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Product] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE TABLE [dbo].[ProductQuantity] (
        [Id] int NOT NULL IDENTITY,
        [Type] nvarchar(50) NOT NULL,
        [Name] nvarchar(100) NULL,
        [Description] nvarchar(max) NULL,
        [IsActive] bit NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_ProductQuantity] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE TABLE [dbo].[ProductReview] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [ProductId] int NOT NULL,
        [Comments] nvarchar(max) NULL,
        [Rating] nvarchar(50) NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_ProductReview] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE TABLE [dbo].[User] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(50) NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [EmailAddress] nvarchar(max) NULL,
        [Password] nvarchar(max) NOT NULL,
        [OneTimePassword] nvarchar(max) NULL,
        [IsActive] bit NULL,
        [Address] nvarchar(max) NULL,
        [Location] nvarchar(max) NULL,
        [PinCode] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE TABLE [dbo].[ProductPrice] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] int NOT NULL,
        [ProductQtyId] int NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [IsOfferAvailable] bit NULL,
        [OfferAmount] decimal(18,2) NULL,
        [OfferDetails] nvarchar(max) NULL,
        [IsDisplayOnProduct] bit NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_ProductPrice] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductPrice_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductPrice_ProductQuantity_ProductQtyId] FOREIGN KEY ([ProductQtyId]) REFERENCES [dbo].[ProductQuantity] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE TABLE [dbo].[AddToCart] (
        [Id] int NOT NULL IDENTITY,
        [IpAddress] nvarchar(max) NOT NULL,
        [UserId] int NOT NULL,
        [ProductId] int NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [Quantity] int NOT NULL,
        [Total] decimal(18,2) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_AddToCart] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AddToCart_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AddToCart_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE TABLE [dbo].[ProductOrderDetails] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [ProductId] int NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [Quantity] int NOT NULL,
        [Total] decimal(18,2) NOT NULL,
        [OrderStatus] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_ProductOrderDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductOrderDetails_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductOrderDetails_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE TABLE [dbo].[UserRefreshToken] (
        [Id] int NOT NULL IDENTITY,
        [Token] nvarchar(max) NULL,
        [RefreshToken] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        [ExpirationDate] datetime2 NOT NULL,
        [IpAddress] nvarchar(max) NULL,
        [IsInvalidated] bit NOT NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_UserRefreshToken] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserRefreshToken_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE INDEX [IX_AddToCart_ProductId] ON [dbo].[AddToCart] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE INDEX [IX_AddToCart_UserId] ON [dbo].[AddToCart] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE INDEX [IX_ProductOrderDetails_ProductId] ON [dbo].[ProductOrderDetails] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE INDEX [IX_ProductOrderDetails_UserId] ON [dbo].[ProductOrderDetails] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE INDEX [IX_ProductPrice_ProductId] ON [dbo].[ProductPrice] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE INDEX [IX_ProductPrice_ProductQtyId] ON [dbo].[ProductPrice] ([ProductQtyId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    CREATE INDEX [IX_UserRefreshToken_UserId] ON [dbo].[UserRefreshToken] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916060727_Setup_initial_Tables_Scripts')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220916060727_Setup_initial_Tables_Scripts', N'6.0.9');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916061421_Setup_Initial_Insert_Scripts')
BEGIN
    PRINT 'Executing statements in : ... DDLScripts/Procs/dbo.sp_DeleteTokens.sql'
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916061421_Setup_Initial_Insert_Scripts')
BEGIN
    SET ANSI_NULLS ON
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916061421_Setup_Initial_Insert_Scripts')
BEGIN
    SET QUOTED_IDENTIFIER ON
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916061421_Setup_Initial_Insert_Scripts')
BEGIN
    IF OBJECT_ID(N'dbo.sp_DeleteToken','P') IS NOT NULL
    DROP PROC [dbo].[sp_DeleteToken]
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916061421_Setup_Initial_Insert_Scripts')
BEGIN
    EXEC (N'
    CREATE PROCEDURE [dbo].[sp_DeleteToken]
    	@UserId INT
    AS
    BEGIN
    	
    	DELETE [dbo].[UserRefreshToken] WHERE UserId = @UserId AND ExpirationDate > DATEADD(day, -1, GETDATE())
    END')
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916061421_Setup_Initial_Insert_Scripts')
BEGIN
    PRINT 'Executing statements in : ... BladeLogic/DataRefresh/Initial_Insert_Scripts.sql'
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916061421_Setup_Initial_Insert_Scripts')
BEGIN
    DELETE FROM [dbo].[AdminUser]
    DELETE FROM [dbo].[ProductQuantity] 
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916061421_Setup_Initial_Insert_Scripts')
BEGIN
    SET IDENTITY_INSERT [dbo].[AdminUser] ON
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916061421_Setup_Initial_Insert_Scripts')
BEGIN
    INSERT INTO [dbo].[AdminUser] ([Id],[UserName],[Password],[IsActive]) VALUES (1,'Admin','SriSugi',1);
    INSERT INTO [dbo].[AdminUser] ([Id],[UserName],[Password],[IsActive]) VALUES (2,'admin','srisugi',1);
    INSERT INTO [dbo].[AdminUser] ([Id],[UserName],[Password],[IsActive]) VALUES (3,'Natarajan','SriSugi',1);
    INSERT INTO [dbo].[AdminUser] ([Id],[UserName],[Password],[IsActive]) VALUES (4,'natarajan','srisugi',1);
    INSERT INTO [dbo].[AdminUser] ([Id],[UserName],[Password],[IsActive]) VALUES (5,'Savitha','SriSugi',1); 
    INSERT INTO [dbo].[AdminUser] ([Id],[UserName],[Password],[IsActive]) VALUES (6,'savitha','srisugi',1); 
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916061421_Setup_Initial_Insert_Scripts')
BEGIN
    SET IDENTITY_INSERT [dbo].[AdminUser] OFF
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916061421_Setup_Initial_Insert_Scripts')
BEGIN
    SET IDENTITY_INSERT [dbo].[ProductQuantity] ON
    INSERT INTO [dbo].[ProductQuantity] ([Id],[Type],[Name],[Description],[IsActive],[CreatedDate],[UpdatedDate]) VALUES (1,50, 'gm','Grams',1,GETDATE(),GETDATE())
    INSERT INTO [dbo].[ProductQuantity] ([Id],[Type],[Name],[Description],[IsActive],[CreatedDate],[UpdatedDate]) VALUES (2,100, 'gm','Grams',1,GETDATE(),GETDATE())
    INSERT INTO [dbo].[ProductQuantity] ([Id],[Type],[Name],[Description],[IsActive],[CreatedDate],[UpdatedDate]) VALUES (3,250, 'gm','Grams',1,GETDATE(),GETDATE())
    INSERT INTO [dbo].[ProductQuantity] ([Id],[Type],[Name],[Description],[IsActive],[CreatedDate],[UpdatedDate]) VALUES (4,500, 'gm','Grams',1,GETDATE(),GETDATE())
    INSERT INTO [dbo].[ProductQuantity] ([Id],[Type],[Name],[Description],[IsActive],[CreatedDate],[UpdatedDate]) VALUES (5,1, 'KG','',1,GETDATE(),GETDATE())
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916061421_Setup_Initial_Insert_Scripts')
BEGIN
    SET IDENTITY_INSERT [dbo].[ProductQuantity] OFF
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916061421_Setup_Initial_Insert_Scripts')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220916061421_Setup_Initial_Insert_Scripts', N'6.0.9');
END;
GO

COMMIT;
GO

