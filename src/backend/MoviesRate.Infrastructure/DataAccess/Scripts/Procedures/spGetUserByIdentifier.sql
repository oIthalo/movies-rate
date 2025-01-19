CREATE PROCEDURE spGetUserByIdentifier
	@Identifier UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

		SELECT [Id], [Active], [CreatedOn], [Name], [Password], [Identifier]
		FROM [Users]
		WHERE [Identifier] = @Identifier;
	END