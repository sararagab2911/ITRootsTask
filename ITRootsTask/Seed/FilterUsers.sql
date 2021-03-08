
create PROCEDURE FilterUsers @Serach nvarchar(100)AS
select * from Users where @Serach is null or Username like ('%'+@Serach+'%') or Email like ('%'+@Serach+'%') or FullName like ('%'+@Serach+'%')
