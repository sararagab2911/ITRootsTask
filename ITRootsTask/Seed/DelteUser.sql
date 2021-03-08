CREATE PROCEDURE [dbo].[DelteUser] @Id int
AS
delete  FROM Users WHERE Id = @Id
select 0 as Id
