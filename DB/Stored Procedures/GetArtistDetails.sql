CREATE PROCEDURE dbo.GetArtistDetails
	@artistID INT 
AS
BEGIN

	SELECT [title], [biography], [imageURL], [heroURL]
	FROM Artist 
	WHERE
		Artist.artistID = @artistID

	SELECT Song.[title] as 'songTitle',  Album.[title] as 'albumTitleSong', Album.[imageURL] as 'albumImageSong', [bpm], [timeSignature]
	FROM Song INNER JOIN Album ON Album.artistID = Song.artistID
	WHERE 
		Album.artistID = @artistID AND Song.artistID = @artistID AND Song.albumID = Album.albumID

	SELECT Album.[imageURL] as 'albumImage', Album.[title] as 'albumTitle'
	FROM Album
	WHERE
		Album.artistID = @artistID
END