CREATE DATABASE BudgetPlanner
GO

USE BudgetPlanner;

CREATE TABLE [dbo].[Budget](
	[Id] INT NOT NULL
		CONSTRAINT PK_Budget PRIMARY KEY
	,[Reference] VARCHAR(200) NOT NULL
		CONSTRAINT IQ_Budget_Reference UNIQUE
	,[Active] BIT NOT NULL
	,[Created] DATETIMEOFFSET NOT NULL
	,[LastUpdated] DATETIMEOFFSET NULL
	,INDEX Idx_Budget_Reference 
		NONCLUSTERED ([Reference])
)

CREATE TABLE [dbo].[TransactionType]
(
	[Id] INT NOT NULL
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
	[Id] INT NOT NULL
		CONSTRAINT PK_Transaction PRIMARY KEY
	,[BudgetId] INT NOT NULL
		CONSTRAINT FK_Transaction_Budget
		REFERENCES [dbo].[Budget]
	,[TransactionTypeId] INT NOT NULL
	,[Active] BIT NOT NULL
	,[Amount] DECIMAL(18,4) NOT NULL
	,[Created] DATETIMEOFFSET NOT NULL
)