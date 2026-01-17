IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [StudentId] nvarchar(20) NOT NULL,
    [Role] int NOT NULL,
    [Department] nvarchar(100) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Projects] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [TechStack] nvarchar(max) NOT NULL,
    [CategoryId] int NOT NULL,
    [SupervisorId] int NOT NULL,
    [Status] int NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Projects_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Projects_Users_SupervisorId] FOREIGN KEY ([SupervisorId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ProjectMembers] (
    [Id] int NOT NULL IDENTITY,
    [ProjectId] int NOT NULL,
    [UserId] int NOT NULL,
    [Role] nvarchar(max) NOT NULL,
    [JoinedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_ProjectMembers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectMembers_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectMembers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([Id], [CreatedAt], [Description], [Name])
VALUES (1, '2025-07-31T23:13:15.8324110Z', N'Web-based applications and websites', N'Web Development'),
(2, '2025-07-31T23:13:15.8324113Z', N'Mobile applications for iOS and Android', N'Mobile Development'),
(3, '2025-07-31T23:13:15.8324114Z', N'AI and ML projects', N'Machine Learning'),
(4, '2025-07-31T23:13:15.8324115Z', N'Data analysis and visualization projects', N'Data Science'),
(5, '2025-07-31T23:13:15.8324116Z', N'Desktop software applications', N'Desktop Applications');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Department', N'Email', N'FirstName', N'IsActive', N'LastName', N'PasswordHash', N'Role', N'StudentId', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [CreatedAt], [Department], [Email], [FirstName], [IsActive], [LastName], [PasswordHash], [Role], [StudentId], [UpdatedAt])
VALUES (1, '2025-07-31T23:13:15.9924266Z', N'Computer Science', N'admin@smartfyp.com', N'System', CAST(1 AS bit), N'Admin', N'$2a$11$J7u4nsGfofNLEZmUSE.NFuDSufpa8KArN5cC4wif4wGN7lCESH61u', 3, N'', '2025-07-31T23:13:15.9924272Z');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Department', N'Email', N'FirstName', N'IsActive', N'LastName', N'PasswordHash', N'Role', N'StudentId', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO

CREATE INDEX [IX_ProjectMembers_ProjectId] ON [ProjectMembers] ([ProjectId]);
GO

CREATE INDEX [IX_ProjectMembers_UserId] ON [ProjectMembers] ([UserId]);
GO

CREATE INDEX [IX_Projects_CategoryId] ON [Projects] ([CategoryId]);
GO

CREATE INDEX [IX_Projects_SupervisorId] ON [Projects] ([SupervisorId]);
GO

CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250731231316_Initial-Create', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [DepartmentId] int NULL;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProjectMembers]') AND [c].[name] = N'Role');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ProjectMembers] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ProjectMembers] ALTER COLUMN [Role] int NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProjectMembers]') AND [c].[name] = N'ProjectId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ProjectMembers] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ProjectMembers] ALTER COLUMN [ProjectId] int NULL;
GO

ALTER TABLE [ProjectMembers] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [ProjectMembers] ADD [FYPProjectId] int NULL;
GO

ALTER TABLE [ProjectMembers] ADD [UpdatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

CREATE TABLE [Departments] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Code] nvarchar(10) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [IdeaAnalyses] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] int NOT NULL,
    [InputTextHash] nvarchar(128) NOT NULL,
    [InputTitle] nvarchar(300) NOT NULL,
    [InputAbstract] nvarchar(4000) NULL,
    [OriginalityScore] int NOT NULL,
    [SimilarityMax] decimal(18,2) NOT NULL,
    [ResultCategory] int NOT NULL,
    [Status] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CompletedAt] datetime2 NULL,
    [ErrorMessage] nvarchar(max) NULL,
    CONSTRAINT [PK_IdeaAnalyses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [IndexedDocuments] (
    [Id] int NOT NULL IDENTITY,
    [SourceType] int NOT NULL,
    [SourceEntityId] int NULL,
    [Title] nvarchar(300) NOT NULL,
    [Url] nvarchar(2048) NOT NULL,
    [Year] int NULL,
    [DepartmentId] int NULL,
    [Category] nvarchar(100) NOT NULL,
    [Embedding] nvarchar(max) NOT NULL,
    [MetadataJson] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_IndexedDocuments] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ProjectCategories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_ProjectCategories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [IdeaMatches] (
    [Id] int NOT NULL IDENTITY,
    [IdeaAnalysisId] uniqueidentifier NOT NULL,
    [IndexedDocumentId] int NOT NULL,
    [SourceType] int NOT NULL,
    [Similarity] decimal(18,2) NOT NULL,
    [Rank] int NOT NULL,
    [Title] nvarchar(300) NOT NULL,
    [Url] nvarchar(2048) NOT NULL,
    [Snippet] nvarchar(500) NULL,
    CONSTRAINT [PK_IdeaMatches] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_IdeaMatches_IdeaAnalyses_IdeaAnalysisId] FOREIGN KEY ([IdeaAnalysisId]) REFERENCES [IdeaAnalyses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_IdeaMatches_IndexedDocuments_IndexedDocumentId] FOREIGN KEY ([IndexedDocumentId]) REFERENCES [IndexedDocuments] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [FYPProjects] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(200) NOT NULL,
    [Description] nvarchar(2000) NOT NULL,
    [Year] int NOT NULL,
    [Semester] nvarchar(20) NOT NULL,
    [Category] nvarchar(50) NOT NULL,
    [Status] int NOT NULL,
    [DepartmentId] int NOT NULL,
    [SupervisorId] int NOT NULL,
    [DifficultyLevel] nvarchar(20) NOT NULL,
    [PerformanceScore] decimal(18,2) NOT NULL,
    [FinalGrade] nvarchar(5) NOT NULL,
    [DepartmentRank] int NULL,
    [OverallRank] int NULL,
    [Citations] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [ProjectCategoryId] int NULL,
    CONSTRAINT [PK_FYPProjects] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FYPProjects_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FYPProjects_ProjectCategories_ProjectCategoryId] FOREIGN KEY ([ProjectCategoryId]) REFERENCES [ProjectCategories] ([Id]),
    CONSTRAINT [FK_FYPProjects_Users_SupervisorId] FOREIGN KEY ([SupervisorId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [UserPreferences] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [PreferredCategories] nvarchar(max) NOT NULL,
    [EngagementLevel] int NOT NULL,
    [AverageRating] decimal(18,2) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [ProjectCategoryId] int NULL,
    CONSTRAINT [PK_UserPreferences] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserPreferences_ProjectCategories_ProjectCategoryId] FOREIGN KEY ([ProjectCategoryId]) REFERENCES [ProjectCategories] ([Id]),
    CONSTRAINT [FK_UserPreferences_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [DepartmentRankings] (
    [Id] int NOT NULL IDENTITY,
    [DepartmentId] int NOT NULL,
    [ProjectId] int NOT NULL,
    [RankPosition] int NOT NULL,
    [Year] int NOT NULL,
    [Semester] nvarchar(20) NOT NULL,
    [PerformanceScore] decimal(18,2) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_DepartmentRankings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DepartmentRankings_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_DepartmentRankings_FYPProjects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [FYPProjects] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProjectEvaluations] (
    [Id] int NOT NULL IDENTITY,
    [ProjectId] int NOT NULL,
    [EvaluatorId] int NOT NULL,
    [TechnicalScore] decimal(18,2) NOT NULL,
    [InnovationScore] decimal(18,2) NOT NULL,
    [ImplementationScore] decimal(18,2) NOT NULL,
    [PresentationScore] decimal(18,2) NOT NULL,
    [DocumentationScore] decimal(18,2) NOT NULL,
    [OverallScore] decimal(18,2) NOT NULL,
    [EvaluationType] int NOT NULL,
    [Comments] nvarchar(1000) NOT NULL,
    [Recommendations] nvarchar(1000) NOT NULL,
    [EvaluationDate] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_ProjectEvaluations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectEvaluations_FYPProjects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [FYPProjects] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectEvaluations_Users_EvaluatorId] FOREIGN KEY ([EvaluatorId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [UserInteractions] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [ProjectId] int NOT NULL,
    [InteractionType] int NOT NULL,
    [Rating] int NULL,
    [Timestamp] datetime2 NOT NULL,
    CONSTRAINT [PK_UserInteractions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserInteractions_FYPProjects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [FYPProjects] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserInteractions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [Categories] SET [CreatedAt] = '2026-01-17T16:27:13.2411932Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Categories] SET [CreatedAt] = '2026-01-17T16:27:13.2411935Z'
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [Categories] SET [CreatedAt] = '2026-01-17T16:27:13.2411936Z'
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

UPDATE [Categories] SET [CreatedAt] = '2026-01-17T16:27:13.2411937Z'
WHERE [Id] = 4;
SELECT @@ROWCOUNT;

GO

UPDATE [Categories] SET [CreatedAt] = '2026-01-17T16:27:13.2411938Z'
WHERE [Id] = 5;
SELECT @@ROWCOUNT;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'CreatedAt', N'Description', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Departments]'))
    SET IDENTITY_INSERT [Departments] ON;
INSERT INTO [Departments] ([Id], [Code], [CreatedAt], [Description], [Name], [UpdatedAt])
VALUES (1, N'CS', '2026-01-17T16:27:13.2411826Z', N'Computer Science and Software Engineering', N'Computer Science', '2026-01-17T16:27:13.2411830Z'),
(2, N'SE', '2026-01-17T16:27:13.2411834Z', N'Software Engineering and Development', N'Software Engineering', '2026-01-17T16:27:13.2411835Z'),
(3, N'DS', '2026-01-17T16:27:13.2411836Z', N'Data Science and Analytics', N'Data Science', '2026-01-17T16:27:13.2411836Z'),
(4, N'CYB', '2026-01-17T16:27:13.2411838Z', N'Cybersecurity and Information Security', N'Cybersecurity', '2026-01-17T16:27:13.2411839Z'),
(5, N'IT', '2026-01-17T16:27:13.2411840Z', N'Information Technology and Systems', N'Information Technology', '2026-01-17T16:27:13.2411840Z');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'CreatedAt', N'Description', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Departments]'))
    SET IDENTITY_INSERT [Departments] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[ProjectCategories]'))
    SET IDENTITY_INSERT [ProjectCategories] ON;
INSERT INTO [ProjectCategories] ([Id], [CreatedAt], [Description], [Name], [UpdatedAt])
VALUES (1, '2026-01-17T16:27:13.2411955Z', N'AI and ML related projects', N'Machine Learning', '2026-01-17T16:27:13.2411956Z'),
(2, '2026-01-17T16:27:13.2411959Z', N'Web applications and services', N'Web Development', '2026-01-17T16:27:13.2411959Z'),
(3, '2026-01-17T16:27:13.2411960Z', N'Mobile applications for iOS and Android', N'Mobile Development', '2026-01-17T16:27:13.2411961Z'),
(4, '2026-01-17T16:27:13.2411962Z', N'Internet of Things projects', N'IoT', '2026-01-17T16:27:13.2411962Z'),
(5, '2026-01-17T16:27:13.2411963Z', N'Blockchain and cryptocurrency projects', N'Blockchain', '2026-01-17T16:27:13.2411969Z'),
(6, '2026-01-17T16:27:13.2411970Z', N'Data analysis and visualization', N'Data Science', '2026-01-17T16:27:13.2411971Z'),
(7, '2026-01-17T16:27:13.2411972Z', N'Security and privacy related projects', N'Cybersecurity', '2026-01-17T16:27:13.2411972Z');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[ProjectCategories]'))
    SET IDENTITY_INSERT [ProjectCategories] OFF;
GO

UPDATE [Users] SET [CreatedAt] = '2026-01-17T16:27:13.3574919Z', [DepartmentId] = 1, [PasswordHash] = N'$2a$11$tPLVYPlKbef.2eJ0qpHO/uLQAp0Y04GkKRZ4uf/9iHB8rG5O5XNoW', [UpdatedAt] = '2026-01-17T16:27:13.3574923Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_Users_DepartmentId] ON [Users] ([DepartmentId]);
GO

CREATE INDEX [IX_ProjectMembers_FYPProjectId] ON [ProjectMembers] ([FYPProjectId]);
GO

CREATE INDEX [IX_DepartmentRankings_DepartmentId] ON [DepartmentRankings] ([DepartmentId]);
GO

CREATE INDEX [IX_DepartmentRankings_ProjectId] ON [DepartmentRankings] ([ProjectId]);
GO

CREATE UNIQUE INDEX [IX_Departments_Code] ON [Departments] ([Code]);
GO

CREATE INDEX [IX_FYPProjects_DepartmentId] ON [FYPProjects] ([DepartmentId]);
GO

CREATE INDEX [IX_FYPProjects_ProjectCategoryId] ON [FYPProjects] ([ProjectCategoryId]);
GO

CREATE INDEX [IX_FYPProjects_SupervisorId] ON [FYPProjects] ([SupervisorId]);
GO

CREATE INDEX [IX_IdeaMatches_IdeaAnalysisId] ON [IdeaMatches] ([IdeaAnalysisId]);
GO

CREATE INDEX [IX_IdeaMatches_IndexedDocumentId] ON [IdeaMatches] ([IndexedDocumentId]);
GO

CREATE INDEX [IX_IndexedDocuments_SourceType_Year] ON [IndexedDocuments] ([SourceType], [Year]);
GO

CREATE INDEX [IX_ProjectEvaluations_EvaluatorId] ON [ProjectEvaluations] ([EvaluatorId]);
GO

CREATE INDEX [IX_ProjectEvaluations_ProjectId] ON [ProjectEvaluations] ([ProjectId]);
GO

CREATE INDEX [IX_UserInteractions_ProjectId] ON [UserInteractions] ([ProjectId]);
GO

CREATE INDEX [IX_UserInteractions_UserId] ON [UserInteractions] ([UserId]);
GO

CREATE INDEX [IX_UserPreferences_ProjectCategoryId] ON [UserPreferences] ([ProjectCategoryId]);
GO

CREATE UNIQUE INDEX [IX_UserPreferences_UserId] ON [UserPreferences] ([UserId]);
GO

ALTER TABLE [ProjectMembers] ADD CONSTRAINT [FK_ProjectMembers_FYPProjects_FYPProjectId] FOREIGN KEY ([FYPProjectId]) REFERENCES [FYPProjects] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id]) ON DELETE SET NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260117162713_AddFYPEntities', N'8.0.8');
GO

COMMIT;
GO

