 
GO

DELETE FROM [dbo].[AdminUser]
DELETE FROM [dbo].[ProductQuantity] 
GO
 
SET IDENTITY_INSERT [dbo].[AdminUser] ON
GO
INSERT INTO [dbo].[AdminUser] ([Id],[UserName],[Password],[IsActive]) VALUES (1,'Admin','SriSugi',1);
INSERT INTO [dbo].[AdminUser] ([Id],[UserName],[Password],[IsActive]) VALUES (2,'admin','srisugi',1);
INSERT INTO [dbo].[AdminUser] ([Id],[UserName],[Password],[IsActive]) VALUES (3,'Natarajan','SriSugi',1);
INSERT INTO [dbo].[AdminUser] ([Id],[UserName],[Password],[IsActive]) VALUES (4,'natarajan','srisugi',1);
INSERT INTO [dbo].[AdminUser] ([Id],[UserName],[Password],[IsActive]) VALUES (5,'Savitha','SriSugi',1); 
INSERT INTO [dbo].[AdminUser] ([Id],[UserName],[Password],[IsActive]) VALUES (6,'savitha','srisugi',1); 
GO

SET IDENTITY_INSERT [dbo].[AdminUser] OFF

GO
SET IDENTITY_INSERT [dbo].[ProductQuantity] ON
INSERT INTO [dbo].[ProductQuantity] ([Id],[Type],[Name],[Description],[IsActive],[CreatedDate],[UpdatedDate]) VALUES (1,50, 'gm','Grams',1,GETDATE(),GETDATE())
INSERT INTO [dbo].[ProductQuantity] ([Id],[Type],[Name],[Description],[IsActive],[CreatedDate],[UpdatedDate]) VALUES (2,100, 'gm','Grams',1,GETDATE(),GETDATE())
INSERT INTO [dbo].[ProductQuantity] ([Id],[Type],[Name],[Description],[IsActive],[CreatedDate],[UpdatedDate]) VALUES (3,250, 'gm','Grams',1,GETDATE(),GETDATE())
INSERT INTO [dbo].[ProductQuantity] ([Id],[Type],[Name],[Description],[IsActive],[CreatedDate],[UpdatedDate]) VALUES (4,500, 'gm','Grams',1,GETDATE(),GETDATE())
INSERT INTO [dbo].[ProductQuantity] ([Id],[Type],[Name],[Description],[IsActive],[CreatedDate],[UpdatedDate]) VALUES (5,1, 'KG','',1,GETDATE(),GETDATE())
GO
SET IDENTITY_INSERT [dbo].[ProductQuantity] OFF