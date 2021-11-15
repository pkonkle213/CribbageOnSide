-- Switch to the system (aka master) database
USE master;
GO

-- Delete the Hands Database (IF EXISTS)
IF EXISTS(select * from sys.databases where name='Hands')
DROP DATABASE Hands;
GO

-- Create a new Hands Database
CREATE DATABASE Hands;
GO

-- Switch to the Hands Database
USE Hands
GO

-- Begin a TRANSACTION that must complete with no errors
BEGIN TRANSACTION;

CREATE TABLE hands (
	handId integer NOT NULL,
	cardOneName varchar(10) NOT NULL,
	cardOneSuit varchar(10) NOT NULL,
	cardTwoName varchar(10) NOT NULL,
	cardTwoSuit varchar(10) NOT NULL,
	cardThreeName varchar(10) NOT NULL,
	cardThreeSuit varchar(10) NOT NULL,
	cardFourName varchar(10) NOT NULL,
	cardFourSuit varchar(10) NOT NULL,
	cardStarterName varchar(10) NOT NULL,
	cardStarterSuit varchar(10) NOT NULL,
	handPointValue integer NOT NULL
    CONSTRAINT pk_handId PRIMARY KEY (handId)
);

COMMIT TRANSACTION