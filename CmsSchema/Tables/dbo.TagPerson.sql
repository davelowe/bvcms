CREATE TABLE [dbo].[TagPerson]
(
[Id] [int] NOT NULL,
[PeopleId] [int] NOT NULL
)
ALTER TABLE [dbo].[TagPerson] WITH NOCHECK ADD
CONSTRAINT [Tags__Person] FOREIGN KEY ([PeopleId]) REFERENCES [dbo].[People] ([PeopleId])

CREATE NONCLUSTERED INDEX [IX_TagPerson] ON [dbo].[TagPerson] ([PeopleId])


GO
ALTER TABLE [dbo].[TagPerson] ADD CONSTRAINT [PK_TagPeople] PRIMARY KEY CLUSTERED  ([Id], [PeopleId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TagPerson] WITH NOCHECK ADD CONSTRAINT [PersonTags__Tag] FOREIGN KEY ([Id]) REFERENCES [dbo].[Tag] ([Id])
GO