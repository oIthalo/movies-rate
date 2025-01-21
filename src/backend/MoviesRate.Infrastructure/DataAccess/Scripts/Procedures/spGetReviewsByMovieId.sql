CREATE PROCEDURE spGetReviewsByMovieId
    @MovieId BIGINT
AS
BEGIN
    SELECT * 
    FROM Reviews
    WHERE MovieId = @MovieId;
END;
