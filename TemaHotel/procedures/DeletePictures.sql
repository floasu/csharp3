CREATE PROCEDURE dbo.DeletePictures @RoomId int
AS
Delete
FROM PictureRooms
WHERE Room_Id = @RoomId
GO