CREATE PROCEDURE dbo.GetPicturesRooms @RoomId int
AS
SELECT Pictures.Path 
FROM Pictures inner join PictureRooms ON Pictures.Id = PictureRooms.Picture_Id
WHERE Room_Id = @RoomId
GO