create table Units ( Id uniqueidentifier not null PRIMARY KEY default newid() ,
UnitName nvarchar(50) not null ,
)

create table StocksInfo ( Id uniqueidentifier not null PRIMARY KEY default newid() ,
StockName nvarchar(50) not null,
StockAdress nvarchar(50) not null)

create table Products ( Id uniqueidentifier not null PRIMARY KEY default newid() ,
ProductName nvarchar(50) not null,
Price int not null,
UnitId uniqueidentifier,
constraint FK_Products_Units foreign key (UnitId) references Units(Id) on delete set null) 

create table Stocks ( Id uniqueidentifier not null PRIMARY KEY default newid() ,
ProductId uniqueidentifier,
constraint FK_Stock_Products foreign key (ProductId) references Products(Id) on delete set null,
ProductQuantity int not null,
StockId uniqueidentifier,
constraint FK_Stocks_StocksInfo foreign key (StockId) references StocksInfo(Id) on delete set null,
CreateDate datetime default Sysdatetime()  ) 