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

CREATE TABLE [AspNetRoles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] uniqueidentifier NOT NULL,
    [Endereco] varchar(300) NULL,
    [NomeDeUtilizador] varchar(100) NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Categorias] (
    [Id] uniqueidentifier NOT NULL,
    [Descricao] varchar(100) NULL,
    [DataCadastro] datetime2 NOT NULL,
    CONSTRAINT [PK_Categorias] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Clientes] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(100) NULL,
    [Telefone] varchar(50) NULL,
    [Email] varchar(255) NULL,
    [Endereco] varchar(500) NULL,
    [Observacao] varchar(max) NULL,
    [DataCadastro] datetime2 NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Produtos] (
    [Id] uniqueidentifier NOT NULL,
    [CategoriaId] uniqueidentifier NULL,
    [Nome] varchar(100) NULL,
    [Preco] decimal(18,2) NULL,
    [Estocavel] bit NULL,
    [EstoqueMinimo] int NULL,
    [Imagem] varchar(255) NULL,
    [Descricao] varchar(max) NULL,
    [DataCadastro] datetime2 NOT NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Produtos_Categorias_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categorias] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Vendas] (
    [Id] uniqueidentifier NOT NULL,
    [ClienteId] uniqueidentifier NULL,
    [UsuarioId] uniqueidentifier NULL,
    [Total] decimal(18,2) NULL,
    [Observacao] varchar(max) NULL,
    [Status] bit NULL DEFAULT CAST(1 AS bit),
    [DataCadastro] datetime2 NOT NULL,
    CONSTRAINT [PK_Vendas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Vendas_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Estoques] (
    [Id] uniqueidentifier NOT NULL,
    [ProdutoId] uniqueidentifier NULL,
    [UsuarioId] uniqueidentifier NULL,
    [Quantidade] int NULL,
    [Observacao] varchar(max) NULL,
    [DataCadastro] datetime2 NOT NULL,
    CONSTRAINT [PK_Estoques] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Estoques_Produtos_ProdutoId] FOREIGN KEY ([ProdutoId]) REFERENCES [Produtos] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [VendaProdutos] (
    [Id] uniqueidentifier NOT NULL,
    [ProdutoId] uniqueidentifier NULL,
    [VendaId] uniqueidentifier NULL,
    [Quantidade] int NULL,
    [PrecoUnitario] decimal(18,2) NULL,
    [Subtotal] decimal(18,2) NULL,
    CONSTRAINT [PK_VendaProdutos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_VendaProdutos_Produtos_ProdutoId] FOREIGN KEY ([ProdutoId]) REFERENCES [Produtos] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_VendaProdutos_Vendas_VendaId] FOREIGN KEY ([VendaId]) REFERENCES [Vendas] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Estoques_ProdutoId] ON [Estoques] ([ProdutoId]) WHERE [ProdutoId] IS NOT NULL;
GO

CREATE INDEX [IX_Produtos_CategoriaId] ON [Produtos] ([CategoriaId]);
GO

CREATE INDEX [IX_VendaProdutos_ProdutoId] ON [VendaProdutos] ([ProdutoId]);
GO

CREATE INDEX [IX_VendaProdutos_VendaId] ON [VendaProdutos] ([VendaId]);
GO

CREATE INDEX [IX_Vendas_ClienteId] ON [Vendas] ([ClienteId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220107063444_Initial', N'5.0.12');
GO

COMMIT;
GO

