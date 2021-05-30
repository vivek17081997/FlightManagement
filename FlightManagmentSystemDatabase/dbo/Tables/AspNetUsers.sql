CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   BIGINT             IDENTITY (1, 1) NOT NULL,
    [UserName]             NVARCHAR (256)     NULL,
    [Email]                NVARCHAR (256)     NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [PasswordSalt]         NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [CreatedBy]            BIGINT             NOT NULL,
    [CreatedDate]          DATETIME2 (7)      NOT NULL,
    [ModifyBy]             BIGINT             NOT NULL,
    [Modifydate]           DATETIME2 (7)      NOT NULL,
    [DeletedBy]            BIGINT             NOT NULL,
    [DeletedDate]          DATETIME2 (7)      NOT NULL,
    [IsDeleted]            BIT                NOT NULL,
    [IsActive]             BIT                NOT NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [EmailIndex]
    ON [dbo].[AspNetUsers]([NormalizedEmail] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([NormalizedUserName] ASC) WHERE ([NormalizedUserName] IS NOT NULL);

