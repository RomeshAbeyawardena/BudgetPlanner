USE master;

ALTER DATABASE BudgetPlanner SET SINGLE_USER WITH ROLLBACK IMMEDIATE 

DROP DATABASE BudgetPlanner
GO

CREATE DATABASE BudgetPlanner
GO

USE BudgetPlanner;

CREATE TABLE [dbo].[Budget](
	[Id] INT NOT NULL IDENTITY(1,1)
		CONSTRAINT PK_Budget PRIMARY KEY
	,[Reference] VARCHAR(200) NOT NULL
		CONSTRAINT IQ_Budget_Reference UNIQUE
	,[Name] VARCHAR(200) NOT NULL
	,[Active] BIT NOT NULL
	,[LastTransactionId] INT NULL
	,[Created] DATETIMEOFFSET NOT NULL
	,[LastUpdated] DATETIMEOFFSET NULL
	,INDEX Idx_Budget_Reference 
		NONCLUSTERED ([Reference])
)

CREATE TABLE [dbo].[TransactionType]
(
	[Id] INT NOT NULL IDENTITY(1,1)
		CONSTRAINT PK_TransactionType PRIMARY KEY
	,[Name] VARCHAR(200) NOT NULL
		CONSTRAINT IQ_TransactionType UNIQUE
	,[Created] DATETIMEOFFSET NOT NULL
	,[Modified] DATETIMEOFFSET NOT NULL
	,INDEX Idx_TransactionType
		NONCLUSTERED ([Name])
)

CREATE TABLE [dbo].[Transaction]
(
	[Id] INT NOT NULL IDENTITY(1,1)
		CONSTRAINT PK_Transaction PRIMARY KEY
	,[BudgetId] INT NOT NULL
		CONSTRAINT FK_Transaction_Budget
		REFERENCES [dbo].[Budget]
	,[TransactionTypeId] INT NOT NULL
	,[Description] VARCHAR(320) NULL
	,[Active] BIT NOT NULL
	,[Amount] DECIMAL(18,4) NOT NULL
	,[Created] DATETIMEOFFSET NOT NULL
)

CREATE TABLE [dbo].[TransactionLedger]
(
	 [Id] INT NOT NULL IDENTITY(1,1)
		CONSTRAINT PK_TransactionLedger 
			PRIMARY KEY
	,[TransactionId] INT NULL
		CONSTRAINT FK_TransactionLedger_Transaction
		REFERENCES [dbo].[Transaction]
	,[Amount] DECIMAL(18, 4) NOT NULL
	,[PreviousBalance] DECIMAL(18, 4) NOT NULL
	,[NewBalance] DECIMAL(18, 4)
	,[Created] DATETIMEOFFSET NOT NULL
)

INSERT INTO [dbo].[TransactionType]
(
    [Name],
    [Created],
    [Modified]
)
VALUES
(   'Income',                  -- Name - varchar(200)
    SYSDATETIMEOFFSET(), -- Created - datetimeoffset
    SYSDATETIMEOFFSET()  -- Modified - datetimeoffset
    ),
	(   'Expense',                  -- Name - varchar(200)
    SYSDATETIMEOFFSET(), -- Created - datetimeoffset
    SYSDATETIMEOFFSET()  -- Modified - datetimeoffset
    )
	SELECT * FROM [Budget]

	SELECT * FROM dbo.TransactionLedger