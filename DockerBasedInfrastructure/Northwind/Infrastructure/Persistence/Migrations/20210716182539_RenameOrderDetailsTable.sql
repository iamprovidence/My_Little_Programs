EXEC sp_rename N'[dbo].[Order Details]', N'OrderDetails';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210716182539_RenameOrderDetailsTable', N'3.1.5');

GO

