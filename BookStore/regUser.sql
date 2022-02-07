 
--creating database 
Create database BookStoreDB;

-- creating table
Create table RegUser
(
UserId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
UserName varchar(50) NOT NULL,
Email varchar(50) NOT NULL,
PhoneNo varchar(50) NOT NULL,
Password varchar(50) NOT NULL
)

 

-- creating store procedure for user
Create procedure usp_AddUsers
(   
    @UserName VARCHAR(50),
    @Email VARCHAR(50),   
    @PhoneNo VARCHAR(50),    
	@Password VARCHAR(50) 
)   
as  
Begin    
    Insert into RegUser   
	Values (@UserName,@Email,@PhoneNo, @Password)    
End

exec usp_AddUsers 'samarth','sethi@gmail.com','7771966794','sam123456789';

CREATE PROCEDURE SP_Login
(
	@Email varchar(100),
	@Password varchar(400)
)
AS
BEGIN
	SELECT Email, Password FROM RegUser 
	WHERE @Email=Email AND @Password=Password
END;

exec SP_Login 'sethi@gmail.com','sam123456789';

CREATE PROCEDURE SP_Forget
(
	@Email varchar(100)
)
AS
BEGIN
	SELECT UserId,Email FROM RegUser 
	WHERE @Email=Email 
END;

exec SP_Forget 'sethi@gmail.com';

drop PROCEDURE SP_Forget

delete from RegUser where UserId=3

select * from RegUser


create procedure SP_ResetPassword
 (
    @Email varchar(30),
	@Password varchar(40)
)
 as
 begin
	 Update RegUser 
	 SET Password=@Password
	 where Email=@Email
	 Select * from RegUser where Email=@Email; 
 End;