

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'dbo.sp_DeleteToken','P') IS NOT NULL
DROP PROC [dbo].[sp_DeleteToken]
GO

EXEC (N'
CREATE PROCEDURE [dbo].[sp_DeleteToken]
	@UserId INT
AS
BEGIN
	
	DELETE [dbo].[UserRefreshToken] WHERE UserId = @UserId AND ExpirationDate > DATEADD(day, -1, GETDATE())
END')

GO