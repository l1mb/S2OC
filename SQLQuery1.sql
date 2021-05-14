----create database OOP_COURSEWORK
use OOP_COURSEWORK
	drop table AUTH
	drop table PERSON
	drop table HABBIT
	drop table CHALLENGE
	drop table EVENT
	DROP TABLE WALLET
select * from Auth
select * from person
select * from Habbit

CREATE TABLE AUTH
(
ID INT PRIMARY KEY ,
LOGIN VARCHAR(255) unique,
PASSWORD VARCHAR(255),
SALT VARCHAR(255)
)
--USE OOP_COURSEWORK







--drop table WALLET

----GO
----ТУТ ПОМЕНЯЛ ФАМИЛИЮ 
CREATE TABLE WALLET
(
IDWALLET INT PRIMARY KEY,
BALANCE DECIMAL(17,3),
)

CREATE TABLE PERSON
(
ID INT FOREIGN KEY REFERENCES AUTH(ID),
NAME VARCHAR(15),
LASTNAME VARCHAR(20),
ROLE VARCHAR(20) CHECK (ROLE = 'Администратор' or ROLE = 'Модератор' or ROLE = 'Пользователь' ) DEFAULT ('Пользователь'),
PICTURE VARCHAR(40),
IDWALLET INT foreign key references Wallet(idwallet)
Primary key (id)
)
use OOP_COURSEWORK

select * from AUTH
select * from PERSON
select *from HABIT
select * from WEEKPROGRESS
select * from WALLET

	select * from Challenge

--drop table PERSON
use OOP_COURSEWORK

select * from AUTH
select * from PERSON
select * from HABBIT
--USE OOP_COURSEWORK;

create table HABBIT
(
	ID INT PRIMARY KEY,
	USERID INT FOREIGN KEY REFERENCES PERSON(ID) ,
	Title varchar(40),
	Description varchar(100),
	DaysCount int,
	CreateDate date,
	CurrentStreak int
)



--SELECT * FROM PERSON

create table CHALLENGE
(
	CHALLENGEID INT PRIMARY KEY,
	CREATORID INT FOREIGN KEY REFERENCES PERSON(ID),
	TITLE VARCHAR(40),
	TIP VARCHAR(100),
	DAYSCOUNT INT,
	CHALLENGERID INT,
	EVENTID int
)





drop table CHALLENGE
use OOP_COURSEWORK
CREATE TABLE EVENT
(
	CHALLENGEID INT FOREIGN KEY REFERENCES CHALLENGE(CHALLENGEID),
	EVENTID INT PRIMARY KEY ,
	DAY datetime,
	EVENT VARCHAR(50),
	ISDONE bit
)



drop table event
--go