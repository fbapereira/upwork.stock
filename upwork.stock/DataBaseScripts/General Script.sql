USE demo;
GO
/* Create a table with the views name */
create table dbo.tbl_reports (
	id int IDENTITY(1,1) PRIMARY KEY,
    name varchar(255) NOT NULL,
	text varchar(255) NOT NULL,
	description varchar(255) NOT NULL,
    isActive bit DEFAULT 1
);
GO

CREATE TABLE dbo.tbl_TransactionsStatement (
	id int IDENTITY(1,1) PRIMARY KEY,
    scheme varchar(255) NOT NULL,
	scrip varchar(255) NOT NULL,
    [date] date,
	isBuy bit,
	isAvailable bit,
	netRate numeric,
	quantity numeric,
	amount numeric,
	realisedGain numeric
);
GO 

/* Create the first view */
CREATE VIEW dbo.vw_stocks 
	AS SELECT top 10 FnOStocks.[SYMBOL]
		,[OPEN]
		,[HIGH]
		,[LOW]
		,[CLOSE]
		,[TIMESTAMP]
		,[SERIES]
		,[LAST]
		,[PREVCLOSE]
		,[TOTTRDQTY]
		,[TOTTRDVAL]
		,[TOTALTRADES]
		,[ISIN]
	FROM [demo].[dbo].[EQUITY] INNER JOIN  FnOStocks on [EQUITY].SYMBOL = FnOStocks.[SYMBOL];

GO

/* Insert the first view on the view table */
INSERT dbo.tbl_reports (name, text, description) VALUES ('vw_stocks', 'Stocks', 'test description');

GO

/* Create procedure to select the views */ 
CREATE PROCEDURE dbo.sp_getReports 
	AS SELECT * FROM dbo.tbl_reports WHERE isActive = 1

GO

CREATE PROCEDURE sp_addTransactionsStatement
    @scheme varchar(255),
	@scrip varchar(255),
    @date date,
	@isBuy bit,
	@isAvailable bit,
	@netRate numeric,
	@quantity numeric,
	@amount numeric,
	@realisedGain numeric
AS
	Insert Into dbo.tbl_TransactionsStatement(
		scheme
		,scrip
		,date
		,isBuy
		,isAvailable
		,quantity
		,amount
		,realisedGain
		,netRate
	) VALUES (
		@scheme,
		@scrip,
		@date,
		@isBuy,
		@isAvailable,
		@quantity,
		@amount,
		@realisedGain,
		@netRate
	)
