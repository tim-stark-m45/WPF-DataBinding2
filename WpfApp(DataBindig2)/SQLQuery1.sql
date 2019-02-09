create table Contacts(
	Id int primary key identity,
	Name nvarchar(100)not null,
	Surname nvarchar(100) not null,
	Phone nvarchar(100) not null unique
);

select * from Contacts

insert into Contacts values('Tim','Lenck','123123')