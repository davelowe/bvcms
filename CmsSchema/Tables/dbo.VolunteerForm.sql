CREATE TABLE [dbo].[VolunteerForm]
(
[PeopleId] [int] NOT NULL,
[AppDate] [datetime] NULL,
[LargeId] [int] NULL,
[MediumId] [int] NULL,
[SmallId] [int] NULL,
[Id] [int] NOT NULL IDENTITY(1, 1),
[UploaderId] [int] NULL,
[IsDocument] [bit] NULL
)
ALTER TABLE [dbo].[VolunteerForm] ADD
CONSTRAINT [FK_VolunteerForm_PEOPLE_TBL] FOREIGN KEY ([PeopleId]) REFERENCES [dbo].[People] ([PeopleId])

ALTER TABLE [dbo].[VolunteerForm] ADD
CONSTRAINT [VolunteerFormsUploaded__Uploader] FOREIGN KEY ([UploaderId]) REFERENCES [dbo].[Users] ([UserId])





ALTER TABLE [dbo].[VolunteerForm] ADD 
CONSTRAINT [PK_VolunteerForm] PRIMARY KEY CLUSTERED  ([Id])

GO
CREATE NONCLUSTERED INDEX [IX_VolunteerForm] ON [dbo].[VolunteerForm] ([PeopleId])
GO
ALTER TABLE [dbo].[VolunteerForm] ADD CONSTRAINT [FK_VolunteerForm_Volunteer1] FOREIGN KEY ([PeopleId]) REFERENCES [dbo].[Volunteer] ([PeopleId])
GO