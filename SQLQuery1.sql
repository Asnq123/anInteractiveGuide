CREATE TABLE [dbo].[dise] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [title]   VARCHAR (50)  NULL,
    [info]    VARCHAR (MAX) NULL,
    [symp]    VARCHAR (MAX) NULL,
    [img] VARCHAR (MAX) NULL,
    [treat]   VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[organs] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [title]     VARCHAR (50)  NULL,
    [info]      VARCHAR (MAX) NULL,
    [poss_dis]  VARCHAR (MAX) NULL,
    [img]   VARCHAR (MAX) NULL,
    [video] VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[users] (
    [Id]    INT          IDENTITY (1, 1) NOT NULL,
    [name]  VARCHAR (50) NULL,
    [pass]  VARCHAR (50) NULL,
    [role]  VARCHAR (50) NULL,
    [email] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
