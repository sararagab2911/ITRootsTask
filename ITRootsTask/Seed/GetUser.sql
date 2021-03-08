CREATE PROCEDURE [dbo].[GetUser] @Id int
AS
select * FROM Users WHERE Id = @Id
