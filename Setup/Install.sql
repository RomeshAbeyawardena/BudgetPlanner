USE master;
--CREATE DATABASE BudgetPlanner_CMS
ALTER DATABASE BudgetPlanner SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

DROP DATABASE BudgetPlanner;
GO

CREATE DATABASE BudgetPlanner;
GO

USE BudgetPlanner;

CREATE USER [WebUser] FROM LOGIN [WebUser]
WITH DEFAULT_SCHEMA = dbo

ALTER ROLE db_datareader
ADD MEMBER [WebUser]

ALTER ROLE db_datawriter
ADD MEMBER [WebUser]

CREATE TABLE [dbo].[Account]
(
    [Id] INT NOT NULL IDENTITY(1, 1)
        CONSTRAINT PK_Account PRIMARY KEY,
    [EmailAddress] VARBINARY(MAX) NOT NULL,
    [Password] VARBINARY(MAX) NOT NULL,
    [FirstName] VARBINARY(MAX) NOT NULL,
    [Lastname] VARBINARY(MAX) NOT NULL,
    [Active] BIT NOT NULL,
    [Created] DATETIMEOFFSET NOT NULL,
    [Modified] DATETIMEOFFSET NULL
);

CREATE TABLE [dbo].[Budget]
(
    [Id] INT NOT NULL IDENTITY(1, 1)
        CONSTRAINT PK_Budget PRIMARY KEY,
    [AccountId] INT NOT NULL
        CONSTRAINT FK_Budget_Account
        REFERENCES [dbo].[Account],
    [Reference] VARCHAR(200) NOT NULL
        CONSTRAINT IQ_Budget_Reference
        UNIQUE,
    [Name] VARCHAR(200) NOT NULL,
    [Active] BIT NOT NULL,
    [LastTransactionId] INT NULL,
    [Created] DATETIMEOFFSET NOT NULL,
    [LastUpdated] DATETIMEOFFSET NULL,
    INDEX Idx_Budget_Reference NONCLUSTERED ([Reference])
);

CREATE TABLE [dbo].[BudgetLimit]
(
    [Id] INT NOT NULL IDENTITY(1,1)
        CONSTRAINT PK_BudgetLimit PRIMARY KEY
    ,[Description] VARCHAR(200) NOT NULL
    ,[Amount] DECIMAL(18, 4) NOT NULL
    ,[FromDate] DATETIME NOT NULL
    ,[ToDate] DATETIME NULL
    ,[Created] DATETIMEOFFSET NOT NULL
    ,[Modified] DATETIMEOFFSET NOT NULL
)

CREATE TABLE [dbo].[TransactionType]
(
    [Id] INT NOT NULL IDENTITY(1, 1)
        CONSTRAINT PK_TransactionType PRIMARY KEY,
    [Name] VARCHAR(200) NOT NULL
        CONSTRAINT IQ_TransactionType
        UNIQUE,
    [Created] DATETIMEOFFSET NOT NULL,
    [Modified] DATETIMEOFFSET NOT NULL,
    INDEX Idx_TransactionType NONCLUSTERED ([Name])
);

CREATE TABLE [dbo].[Transaction]
(
    [Id] INT NOT NULL IDENTITY(1, 1)
        CONSTRAINT PK_Transaction PRIMARY KEY,
    [BudgetId] INT NOT NULL
        CONSTRAINT FK_Transaction_Budget
        REFERENCES [dbo].[Budget],
    [TransactionTypeId] INT NOT NULL,
    [Description] VARCHAR(320) NULL,
    [Active] BIT NOT NULL,
    [Amount] DECIMAL(18, 4) NOT NULL,
    [Created] DATETIMEOFFSET NOT NULL
);

CREATE TABLE [dbo].[TransactionLedger]
(
    [Id] INT NOT NULL IDENTITY(1, 1)
        CONSTRAINT PK_TransactionLedger PRIMARY KEY,
    [TransactionId] INT NOT NULL
        CONSTRAINT FK_TransactionLedger_Transaction
        REFERENCES [dbo].[Transaction],
    [Amount] DECIMAL(18, 4) NOT NULL,
    [PreviousBalance] DECIMAL(18, 4) NOT NULL,
    [NewBalance] DECIMAL(18, 4) NOT NULL,
    [Created] DATETIMEOFFSET NOT NULL
);

INSERT INTO [dbo].[TransactionType]
(
    [Name],
    [Created],
    [Modified]
)
VALUES
(   'Income',            -- Name - varchar(200)
    SYSDATETIMEOFFSET(), -- Created - datetimeoffset
    SYSDATETIMEOFFSET()  -- Modified - datetimeoffset
    ),
(   'Expense',           -- Name - varchar(200)
    SYSDATETIMEOFFSET(), -- Created - datetimeoffset
    SYSDATETIMEOFFSET()  -- Modified - datetimeoffset
);


CREATE TABLE [dbo].[RequestToken]
(
    [Id] INT NOT NULL IDENTITY(1, 1)
        CONSTRAINT PK_RequestToken PRIMARY KEY,
    [Key] VARBINARY(MAX),
    [Created] DATETIMEOFFSET NOT NULL,
    [Expires] DATETIMEOFFSET NOT NULL
);

CREATE TABLE [dbo].[Role]
(
    [Id] INT NOT NULL IDENTITY(1, 1)
        CONSTRAINT PK_Role PRIMARY KEY
    ,[Name] VARCHAR(200) NOT NULL
        CONSTRAINT IQ_Role
        UNIQUE
    ,[Active] BIT NOT NULL
    ,[Created] DATETIMEOFFSET NOT NULL
    ,[Modified] DATETIMEOFFSET NULL
);

INSERT INTO [dbo].[Role]
(
    [Name],
    [Active],
    [Created],
    [Modified]
)
VALUES
(   'Admin',    -- Name - varchar(200)
    1,
    SYSDATETIMEOFFSET(), -- Created - datetimeoffset
    NULL                 -- Modified - datetimeoffset
    ),
(   'Standard User',     -- Name - varchar(200)
    1,
    SYSDATETIMEOFFSET(), -- Created - datetimeoffset
    NULL                 -- Modified - datetimeoffset
);

CREATE TABLE [dbo].[AccountRole]
(
    [Id] INT NOT NULL IDENTITY(1, 1)
        CONSTRAINT PK_AccountRole PRIMARY KEY,
    [AccountId] INT NOT NULL
        CONSTRAINT FK_AccountRole_Account
        REFERENCES [dbo].[Account],
    [RoleId] INT NOT NULL
        CONSTRAINT FK_AccountRole_Role
        REFERENCES [dbo].[Role]
    ,[Active] BIT NOT NULL
    ,[Created] DATETIMEOFFSET NOT NULL
)

CREATE TABLE [dbo].[Claim]
(
	[Id] INT NOT NULL IDENTITY(1, 1)
        CONSTRAINT PK_Claim PRIMARY KEY
	,[Name] VARCHAR(200) NOT NULL
    ,[Active] BIT NOT NULL
	,[Created] DATETIMEOFFSET NOT NULL
	,[Modified] DATETIMEOFFSET NOT NULL
)

CREATE TABLE [dbo].[AccountClaim]
(
    [Id] INT NOT NULL IDENTITY(1, 1)
        CONSTRAINT PK_AccountClaim PRIMARY KEY
    ,[AccountId] INT NOT NULL
        CONSTRAINT FK_AccountClaim_Account
        REFERENCES [dbo].[Account]
    ,[ClaimId] INT NOT NULL
        CONSTRAINT FK_AccountClaim_Claim
        REFERENCES [dbo].[Claim]
	,[Value] VARCHAR(2000) NOT NULL
    ,[Active] BIT NOT NULL
    ,[Created] DATETIMEOFFSET NOT NULL
)

CREATE TABLE [dbo].[AccessType]
(
    [Id] INT NOT NULL IDENTITY(1,1)
        CONSTRAINT PK_AccessType PRIMARY KEY
    ,[Name] VARCHAR(200) NOT NULL
	,[Active] BIT NOT NULL
    ,[Created] DATETIMEOFFSET NOT NULL
    ,[Modified] DATETIMEOFFSET NOT NULL
)

INSERT INTO dbo.AccessType
    (
        [Name],
		[Active],
        Created,
        Modified
    )
VALUES
    (
        'Login',                  -- Name - varchar(200)
		1,
        SYSDATETIMEOFFSET(), -- Created - datetimeoffset
        SYSDATETIMEOFFSET()  -- Modified - datetimeoffset
    )

CREATE TABLE [dbo].[AccountAccess]
(
    [Id] INT NOT NULL IDENTITY(1,1)
        CONSTRAINT PK_AccountAccess PRIMARY KEY
    ,[AccountId] INT NOT NULL
        CONSTRAINT FK_AccountAccess_Account
        REFERENCES [dbo].[Account]
    ,[AccessTypeId] INT NOT NULL
        CONSTRAINT FK_AccountAccess_AccessType
        REFERENCES [dbo].[AccessType]
    ,[Succeeded] BIT NOT NULL
    ,[Active] BIT NOT NULL
    ,[Created] DATETIMEOFFSET NOT NULL
 )
 GO

 CREATE FUNCTION [dbo].[fn_GetBudgetStats](
@budgetId INT,
@fromDate DATETIME,
@toDate DATETIME)
	RETURNS @statistics TABLE ([Date] DATETIME, 
		[ClosingBalance] DECIMAL(18, 4), 
		[TotalExpenses] DECIMAL(18, 4),
		[TotalIncome] DECIMAL(18,4))
AS BEGIN
	DECLARE @currentDate DATETIME = @fromDate

	WHILE @currentDate <= @toDate
	BEGIN
		DECLARE @totalExpenses DECIMAL(18, 4)
		DECLARE @totalIncome DECIMAL(18, 4)
		
		SELECT @totalExpenses = ISNULL(SUM([Amount]), 0)
		FROM dbo.[Transaction] 
		WHERE BudgetId = @budgetId 
				AND TransactionTypeId = 2 
				AND CONVERT(DATE,Created) = @currentDate

		SELECT @totalIncome = ISNULL(SUM([Amount]),0)
		FROM dbo.[Transaction] 
		WHERE BudgetId = @budgetId 
				AND TransactionTypeId = 1
				AND CONVERT(DATE,Created) = @currentDate


		INSERT INTO @statistics
		    (
		        [Date],
		        ClosingBalance,
		        TotalExpenses,
				TotalIncome
		    )
		SELECT TOP(1) @currentDate [Date], NewBalance [Balance], 
			@totalExpenses, @totalIncome
		FROM dbo.TransactionLedger
		INNER JOIN dbo.[Transaction]
		ON [Transaction].Id = TransactionLedger.TransactionId
		WHERE BudgetId = @budgetId
		AND CONVERT(DATE, [TransactionLedger].[Created]) =  @currentDate
		ORDER BY TransactionLedger.Created DESC

		SET @currentDate = DATEADD(DAY, 1, @currentDate)
	END

	RETURN
END
GO