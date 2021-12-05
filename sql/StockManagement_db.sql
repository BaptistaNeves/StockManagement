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
    [DataVenda] datetime2 NOT NULL,
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
    [DataEntrada] datetime2 NOT NULL,
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

CREATE INDEX [IX_Estoques_ProdutoId] ON [Estoques] ([ProdutoId]);
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
VALUES (N'20211204194320_PrimeiraMigração', N'5.0.12');
GO

COMMIT;
GO

