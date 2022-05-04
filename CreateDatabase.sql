Use danil
GO
--fesaka1670@bunlets.com
CREATE TABLE [Client] (
	Phone nvarchar(20) PRIMARY KEY NOT NULL,
	FullName nvarchar(200) NOT NULL,
	[Address] nvarchar(200) NOT NULL,
	[Status] nvarchar(20) NOT NULL
)
GO

CREATE TABLE [Role] (
IDRole int primary key identity(1,1) NOT NULL,
[Role] nvarchar(50) NOT NULL
)
GO

CREATE TABLE [Employee] (
	Phone nvarchar(20) PRIMARY KEY NOT NULL,
	[Role] int NOT NULL,
	[Password] nvarchar(18) NOT NULL,
	[Name] nvarchar(32) NOT NULL,
	Surname nvarchar(32) NOT NULL,
	Patronymic nvarchar(32) NOT NULL,
	[Status] nvarchar(15) NOT NULL,
	Foreign key ([Role]) references [Role](IDRole)
)
GO
CREATE TABLE [Order] (
	ID bigint primary key NOT NULL,
	Client nvarchar(20) NOT NULL,
	Employee nvarchar(20) NOT NULL,
	[Date] datetime NOT NULL,
	[Status] nvarchar(20) NOT NULL,
	FOREIGN KEY (Client) REFERENCES Client(Phone),
	FOREIGN KEY (Employee) REFERENCES Employee(Phone)
)
GO

CREATE TABLE [Ingredient] (
	Articule bigint PRIMARY KEY NOT NULL,
	[Name] nvarchar(200) NOT NULL,
	Unit nvarchar(20) NOT NULL,
  
)
GO

CREATE TABLE [Dish] (
	ID bigint PRIMARY KEY NOT NULL,
	[Name] nvarchar(200) NOT NULL,
	Price money NOT NULL,
	[Count] integer NOT NULL,

)
GO
CREATE TABLE [DishCompound] (
	DishCompoundID INT PRIMARY KEY IDENTITY(1,1),
	Dish bigint NOT NULL,
	Ingredient bigint NOT NULL,
	Volume decimal NOT NULL,
	FOREIGN KEY (Dish) REFERENCES Dish(ID),
	FOREIGN KEY (Ingredient) REFERENCES Ingredient(Articule)

)
GO


CREATE TABLE [OrderCompound] (
	OrderCompoundID INT PRIMARY KEY IDENTITY(1,1),
	[Order] bigint NOT NULL,
	Dish bigint NOT NULL,
	Price money NOT NULL,
	[Count] int NOT NULL,
	[Status] nvarchar(20) NOT NULL,
	FOREIGN KEY ([Order]) REFERENCES [Order](ID),
	FOREIGN KEY (Dish) REFERENCES Dish(ID)

)
GO
