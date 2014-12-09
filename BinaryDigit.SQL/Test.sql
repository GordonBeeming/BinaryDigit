

begin try

CREATE TABLE [dbo].[tbl_Test](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DateTimeStamp] [dbo].[BD_TimeStamp] NOT NULL,
	[SomeText] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tbl_Test] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

INSERT INTO [dbo].[tbl_Test]
           ([DateTimeStamp]
           ,[SomeText])
     VALUES
           (''
           ,'this is a test')

SELECT TOP 20 [ID]
      ,[DateTimeStamp]
      ,[DateTimeStamp].CreateDateTime
      ,[DateTimeStamp].IsDeleted
      ,[DateTimeStamp].ToString()
      ,[SomeText]
  FROM [dbo].[tbl_Test]

  declare @dt BD_TimeStamp = BD_TimeStamp::Parse('')


select @dt

set @dt = BD_TimeStamp::Parse('Created : 20130104081526319, Updated : 20130104081526335, Deleted : N/A')


select @dt

end try
begin catch
    
    print 'failed : ' + ERROR_MESSAGE()
end catch

DROP TABLE [dbo].[tbl_Test]
GO