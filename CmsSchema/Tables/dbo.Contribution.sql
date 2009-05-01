CREATE TABLE [dbo].[Contribution]
(
[ContributionId] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [int] NOT NULL,
[CreatedDate] [datetime] NOT NULL,
[FundId] [int] NOT NULL,
[ContributionTypeId] [int] NOT NULL,
[PeopleId] [int] NULL,
[ContributionDate] [datetime] NULL,
[ContributionAmount] [numeric] (11, 2) NULL,
[ContributionDesc] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ContributionStatusId] [int] NULL,
[PledgeFlag] [bit] NOT NULL,
[ModifiedBy] [int] NULL,
[ModifiedDate] [datetime] NULL,
[BrokerContactDate] [datetime] NULL,
[BrokerName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CheckReceivedDate] [datetime] NULL,
[DateSold] [datetime] NULL,
[Amount] [varchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FundDistributionInfo] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ReturnedCheckNum] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ReturnedCheckBank] [varchar] (40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ReturnedCheckDate] [datetime] NULL,
[PostingDate] [datetime] NULL
)
CREATE NONCLUSTERED INDEX [IX_ContributionTypeId] ON [dbo].[Contribution] ([ContributionTypeId])

ALTER TABLE [dbo].[Contribution] ADD
CONSTRAINT [FK_Contribution_ContributionFund] FOREIGN KEY ([FundId]) REFERENCES [dbo].[ContributionFund] ([FundId])
ALTER TABLE [dbo].[Contribution] ADD
CONSTRAINT [FK_Contribution_ContributionStatus] FOREIGN KEY ([ContributionStatusId]) REFERENCES [lookup].[ContributionStatus] ([Id])
ALTER TABLE [dbo].[Contribution] ADD
CONSTRAINT [FK_Contribution_ContributionType] FOREIGN KEY ([ContributionTypeId]) REFERENCES [lookup].[ContributionType] ([Id])
ALTER TABLE [dbo].[Contribution] ADD
CONSTRAINT [FK_Contribution_People] FOREIGN KEY ([PeopleId]) REFERENCES [dbo].[People] ([PeopleId])


GO
ALTER TABLE [dbo].[Contribution] ADD CONSTRAINT [CONTRIBUTION_PK] PRIMARY KEY NONCLUSTERED ([ContributionId])
GO
CREATE NONCLUSTERED INDEX [CONTRIBUTION_DATE_IX] ON [dbo].[Contribution] ([ContributionDate])
GO

CREATE NONCLUSTERED INDEX [IX_INDIVIDUAL_CONTRIBUTION_TBL_1] ON [dbo].[Contribution] ([ContributionStatusId])
GO
CREATE NONCLUSTERED INDEX [CONTRIBUTION_FUND_FK_IX] ON [dbo].[Contribution] ([FundId])
GO
CREATE NONCLUSTERED INDEX [CONTRIBUTION_PEOPLE_FK_IX] ON [dbo].[Contribution] ([PeopleId])
GO
CREATE NONCLUSTERED INDEX [IX_INDIVIDUAL_CONTRIBUTION_TBL_2] ON [dbo].[Contribution] ([PledgeFlag])
GO