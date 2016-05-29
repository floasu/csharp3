CREATE PROCEDURE dbo.GetFacilities @RoomId int
AS
SELECT Facilities.Id, Facilities.Name, Facilities.Active 
FROM Facilities inner join RoomFacilities ON Facilities.Id = RoomFacilities.Facility_Id
WHERE Room_Id = @RoomId
GO