CREATE TABLE [dbo].[PodCast]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[S3Name] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Title] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Description] [varchar] (3000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[pubDate] [datetime] NULL,
[length] [int] NULL,
[postId] [int] NULL,
[UserId] [int] NULL
)

GO
ALTER TABLE [dbo].[PodCast] ADD CONSTRAINT [PK_PodCast] PRIMARY KEY CLUSTERED ([id])
GO
ALTER TABLE [dbo].[PodCast] ADD CONSTRAINT [FK_PodCast_BlogPost] FOREIGN KEY ([postId]) REFERENCES [dbo].[BlogPost] ([Id])
GO
ALTER TABLE [dbo].[PodCast] ADD CONSTRAINT [FK_PodCast_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
GO