CREATE PROCEDURE [dbo].[AddEditUser] 
	@Id int, 
	@FullName nvarchar(100),
	@UserName nvarchar(100),
	@Email nvarchar(200),
	@Password nvarchar(100),
	@Phone nvarchar(20),
	@CreatedBy int
AS
if(@Id=0)
begin
	declare @count int,@Roles nvarchar(50) 
	set @count= (select count(Id) from Users)
	if(@count=0)
		begin
			set @roles='Admin'
		end
	else 
		begin
			set @Roles='User'
		end
	INSERT INTO Users( FullName,UserName,Email,Password,Phone,Roles, createdOn,CreatedBy, IsVerified)
	VALUES ( @FullName,@UserName,@Email,@Password,@Phone,@Roles,getdate(),@CreatedBy, 1);
end
else
begin
	UPDATE Users
	SET FullName=@FullName,
	UserName=@UserName,
	Email=@Email,
	Phone=@Phone
	WHERE Id=@Id;
end
select 0 As Id
