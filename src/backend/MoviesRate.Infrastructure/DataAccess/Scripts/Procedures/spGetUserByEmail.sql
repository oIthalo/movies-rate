CREATE PROCEDURE spGetUserByEmail
	@Email NVARCHAR(160)
AS
BEGIN
		(
		SELECT [Id], [Active], [CreatedOn], [Name], [Password], [Identifier]
		FROM [Users]
		WHERE [Email] = @Email
		) END