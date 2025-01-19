CREATE PROCEDURE spGetReviewByMovieId
    @MovieId BIGINT
AS
BEGIN
        SELECT 
            [Id], 
            [Active], 
            [CreatedOn], 
            [UserIdentifier], 
            [MovieId], 
            [Comments], 
            [Rating]
        FROM 
            [Reviews]
        WHERE 
            [MovieId] = @MovieId 
        END