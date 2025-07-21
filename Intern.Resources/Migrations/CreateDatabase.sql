-- Create the database if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Intern')
BEGIN
    CREATE DATABASE Intern;
END
GO

USE Intern;
GO

-- Create Role table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Role](
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [Name] NVARCHAR(50) NOT NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
        [UpdatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
        [IsDeleted] BIT NOT NULL DEFAULT 0
    );

    -- Insert default roles
    INSERT INTO [dbo].[Role] ([Name]) VALUES ('Admin'), ('User');
END
GO

-- Create User table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[User](
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [FirstName] NVARCHAR(100) NOT NULL,
        [LastName] NVARCHAR(100) NOT NULL,
        [Username] NVARCHAR(50) NOT NULL UNIQUE,
        [Email] NVARCHAR(100) NOT NULL UNIQUE,
        [PasswordHash] NVARCHAR(MAX) NOT NULL,
        [FKRoleId] INT NOT NULL FOREIGN KEY REFERENCES [Role](Id),
        [ResetToken] NVARCHAR(100) NULL,
        [ResetTokenExpiry] DATETIME NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
        [UpdatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
        [IsDeleted] BIT NOT NULL DEFAULT 0
    );

    -- Insert default admin user with password "admin123"
    INSERT INTO [dbo].[User] (FirstName, LastName, Username, Email, PasswordHash, FKRoleId)
    VALUES ('Admin', 'User', 'admin', 'admin@example.com', '$2a$11$jqXpf7KJjH2qnFG6lGWwXeqpqhQvq3Y6kQf4QJo3gxbTVBqTj7wqe', 1);
END
GO

-- Create College table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[College]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[College](
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [Name] NVARCHAR(200) NOT NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
        [UpdatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
        [IsDeleted] BIT NOT NULL DEFAULT 0
    );

    -- Insert some sample colleges
    INSERT INTO [dbo].[College] ([Name])
    VALUES ('Sample College 1'), ('Sample College 2');
END
GO

-- Create Student table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Student](
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [FKUserId] INT NOT NULL FOREIGN KEY REFERENCES [User](Id),
        [FKCollegeId] INT NOT NULL FOREIGN KEY REFERENCES [College](Id),
        [Course] NVARCHAR(100) NOT NULL,
        [YearOfStudy] INT NOT NULL,
        [ResumeBase64] NVARCHAR(MAX) NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
        [UpdatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
        [IsDeleted] BIT NOT NULL DEFAULT 0
    );
END
GO

-- Create Internship table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Internship]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Internship](
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [Title] NVARCHAR(200) NOT NULL,
        [Description] NVARCHAR(MAX) NOT NULL,
        [Requirements] NVARCHAR(MAX) NOT NULL,
        [Duration] INT NOT NULL, -- in months
        [StartDate] DATE NOT NULL,
        [EndDate] DATE NOT NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
        [UpdatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
        [IsDeleted] BIT NOT NULL DEFAULT 0
    );
END
GO

-- Create ApplicationStatus table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ApplicationStatus]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[ApplicationStatus](
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [Name] NVARCHAR(50) NOT NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
        [UpdatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
        [IsDeleted] BIT NOT NULL DEFAULT 0
    );

    -- Insert default statuses
    INSERT INTO [dbo].[ApplicationStatus] ([Name])
    VALUES ('Pending'), ('Under Review'), ('Accepted'), ('Rejected');
END
GO

-- Create Application table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Application]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Application](
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [FKStudenId] INT NOT NULL FOREIGN KEY REFERENCES [Student](Id),
        [FKInternshipId] INT NOT NULL FOREIGN KEY REFERENCES [Internship](Id),
        [FKApplicationStatusId] INT NOT NULL FOREIGN KEY REFERENCES [ApplicationStatus](Id),
        [CreatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
        [UpdatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
        [IsDeleted] BIT NOT NULL DEFAULT 0
    );
END
GO 