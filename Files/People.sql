CREATE TABLE [dbo].[Cities](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[People](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[CityId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT [dbo].[Cities] ([Id], [Name]) VALUES (N'd3f447f6-bb45-4644-8b16-02d47874b476', N'Taggia')
GO
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (N'330b08ee-cb5c-4663-a4bb-13918cb2c858', N'Paperopoli')
GO
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (N'b105bd7a-9c28-4357-8487-6c14a8c732d7', N'Topolinia')
GO

INSERT [dbo].[People] ([Id], [FirstName], [LastName], [CityId]) VALUES (N'00337e50-0a73-44bd-a74d-36a95da66995', N'Donald', N'Duck', N'330b08ee-cb5c-4663-a4bb-13918cb2c858')
GO
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [CityId]) VALUES (N'ed9272a3-f55c-4fe2-8ef6-3e13718d3a7b', N'Scrooge', N'McDuck', N'330b08ee-cb5c-4663-a4bb-13918cb2c858')
GO
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [CityId]) VALUES (N'2694c630-458d-46a0-82b7-4f7786e88edd', N'Mickey', N'Mouse', N'b105bd7a-9c28-4357-8487-6c14a8c732d7')
GO
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [CityId]) VALUES (N'7dd7ca8e-4c10-41f0-85df-7735ca1eb375', N'Marco', N'Minerva', N'd3f447f6-bb45-4644-8b16-02d47874b476')
GO
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [CityId]) VALUES (N'67e3b557-7501-4611-9579-9aee96b70ffd', N'Fethry', N'Duck', N'330b08ee-cb5c-4663-a4bb-13918cb2c858')
GO

ALTER TABLE [dbo].[Cities] ADD  CONSTRAINT [DF_Cities_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[People] ADD  CONSTRAINT [DF_People_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_Cities] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
GO
ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_Cities]
GO
