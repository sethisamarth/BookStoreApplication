 
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
Create procedure usp_AddUser
(   
    @UserName VARCHAR(50),
    @Email VARCHAR(50),   
    @PhoneNo VARCHAR(50),   
	@Password VARCHAR(50) 
)   
as  
Begin    
    Insert into RegUser (UserName,Email,PhoneNo,Password)    
	Values (@UserName,@Email,@PhoneNo, @Password)    
End