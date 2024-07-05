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

CREATE TABLE [Stores] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Stores] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [VAT] decimal(18,2) NOT NULL,
    [storeId] int NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Stores_storeId] FOREIGN KEY ([storeId]) REFERENCES [Stores] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Products_storeId] ON [Products] ([storeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240703134342_InitialDatabase', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240703143644_fixinPrice_VAT', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Name') AND [object_id] = OBJECT_ID(N'[Stores]'))
    SET IDENTITY_INSERT [Stores] ON;
INSERT INTO [Stores] ([Id], [Address], [Name])
VALUES (1, N'12 Park Street', N'Store 1'),
(2, N'21 Park Street', N'Store 12');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Name') AND [object_id] = OBJECT_ID(N'[Stores]'))
    SET IDENTITY_INSERT [Stores] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240703210457_putSeedDataForStore', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240704152726_foreignKeybetweenProduct_StoreFix', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240704155949_foreignKeybetweenProduct_StoreFix2', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240704160026_foreignKeybetweenProduct_StoreFix3', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240704195331_remove_identation_from_productstore', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Stores] ADD [ShippingCost] int NOT NULL DEFAULT 0;
GO

UPDATE [Stores] SET [ShippingCost] = 0
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Stores] SET [ShippingCost] = 0
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240705023142_settingCartCartItemShippingCost', N'8.0.6');
GO

COMMIT;
GO

