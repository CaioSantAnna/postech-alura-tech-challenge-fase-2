USE [master]

GO

SET NOCOUNT ON

GO

IF EXISTS (SELECT 1
           FROM   sys.databases
           WHERE  [Name] = 'GerenciadorAcervo')
  BEGIN
      ALTER DATABASE GerenciadorAcervo

      SET SINGLE_USER WITH

      ROLLBACK IMMEDIATE

      DROP DATABASE GerenciadorAcervo
  END

CREATE DATABASE GerenciadorAcervo

GO

USE GerenciadorAcervo

GO

IF NOT EXISTS (SELECT *
               FROM   SYS.tables
               WHERE  NAME = 'Usuarios')
  BEGIN
      CREATE TABLE Usuarios
        (
           UsuarioId INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
           Nome      VARCHAR(50) NOT NULL,
           Email     VARCHAR(100) UNIQUE NOT NULL,
           SenhaSalt VARCHAR(64) NOT NULL,
           SenhaHash VARCHAR(64) NOT NULL
        )
  END

GO

IF NOT EXISTS (SELECT *
               FROM   SYS.tables
               WHERE  NAME = 'Categorias')
  BEGIN
      CREATE TABLE Categorias
        (
           CategoriaId INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
           UsuarioId   INT NOT NULL,
           Nome        VARCHAR(100) NOT NULL,
           Descricao   VARCHAR(MAX) NULL,
           Imagem      VARCHAR(MAX) NULL,
           FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId)
        )
  END

GO

IF NOT EXISTS (SELECT *
               FROM   SYS.tables
               WHERE  NAME = 'SubCategorias')
  BEGIN
      CREATE TABLE SubCategorias
        (
           SubCategoriaId INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
           CategoriaId    INT NOT NULL,
           Nome           VARCHAR(100) NOT NULL,
           Descricao      VARCHAR(MAX) NULL,
           Imagem         VARCHAR(MAX) NULL,
           FOREIGN KEY (CategoriaId) REFERENCES Categorias(CategoriaId)
        )
  END

GO

IF NOT EXISTS (SELECT *
               FROM   SYS.tables
               WHERE  NAME = 'Atributos')
  BEGIN
      CREATE TABLE Atributos
        (
           AtributoId     INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
           AtributoTipoId INT NOT NULL,
           CategoriaId    INT NOT NULL,
           SubCategoriaId INT NULL,
           Nome           VARCHAR(50),
           Descricao      VARCHAR(200),
           FOREIGN KEY (CategoriaId) REFERENCES Categorias(CategoriaId),
           FOREIGN KEY (SubCategoriaId) REFERENCES SubCategorias(SubCategoriaId) ON DELETE CASCADE
        )
  END

GO

IF NOT EXISTS (SELECT *
               FROM   SYS.tables
               WHERE  NAME = 'Contatos')
  BEGIN
      CREATE TABLE Contatos
        (
           ContatoId INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
           Nome      VARCHAR(100) NOT NULL,
           UsuarioId INT NOT NULL,
           FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId)
        )
  END

GO

IF NOT EXISTS (SELECT *
               FROM   SYS.tables
               WHERE  NAME = 'ItensAcervo')
  BEGIN
      CREATE TABLE ItensAcervo
        (
           ItemAcervoId   INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
           CategoriaId    INT NOT NULL,
           SubCategoriaId INT NULL,
           Nome           VARCHAR(200) NULL,
           UsuarioId      INT NOT NULL,
           ContatoId      INT NULL
           FOREIGN KEY (CategoriaId) REFERENCES Categorias(CategoriaId),
           FOREIGN KEY (SubCategoriaId) REFERENCES SubCategorias(SubCategoriaId),
           FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId),
           FOREIGN KEY (ContatoId) REFERENCES Contatos(ContatoId)
        )
  END

GO

IF NOT EXISTS (SELECT *
               FROM   SYS.tables
               WHERE  NAME = 'ItensAcervoAtributosValores')
  BEGIN
      CREATE TABLE ItensAcervoAtributosValores
        (
           ItemAcervoId INT NOT NULL,
           AtributoId   INT NOT NULL,
           Valor        VARCHAR(MAX) NULL,
           PRIMARY KEY (ItemAcervoId, AtributoId),
		   FOREIGN KEY (AtributoId) REFERENCES Atributos(AtributoId) ON DELETE CASCADE,
           FOREIGN KEY (ItemAcervoId) REFERENCES ItensAcervo(ItemAcervoId) ON DELETE CASCADE,
        )
  END

GO

CREATE TRIGGER TG_CATEGORIAS_DELETE
ON Categorias
INSTEAD OF DELETE
AS
  BEGIN
      DELETE C
      FROM   deleted A
             INNER JOIN ItensAcervo B
                     ON a.CategoriaId = B.CategoriaId
                        AND A.UsuarioId = B.UsuarioId
             INNER JOIN ItensAcervoAtributosValores C
                     ON B.ItemAcervoId = C.ItemAcervoId

      DELETE B
      FROM   deleted A
             INNER JOIN ItensAcervo B
                     ON a.CategoriaId = B.CategoriaId
                        AND A.UsuarioId = B.UsuarioId

      DELETE B
      FROM   deleted A
             INNER JOIN Atributos B
                     ON a.CategoriaId = B.CategoriaId

      DELETE B
      FROM   deleted A
             INNER JOIN SubCategorias B
                     ON A.CategoriaId = B.CategoriaId

      DELETE B
      FROM   deleted A
             INNER JOIN Categorias B
                     ON A.CategoriaId = B.CategoriaId
  END

GO

CREATE TRIGGER TG_SUBCATEGORIAS_DELETE
ON SubCategorias
INSTEAD OF DELETE
AS
  BEGIN
      DELETE C
      FROM   deleted A
             INNER JOIN ItensAcervo B
                     ON a.SubCategoriaId = B.SubCategoriaId
             INNER JOIN ItensAcervoAtributosValores C
                     ON B.ItemAcervoId = C.ItemAcervoId

      DELETE B
      FROM   deleted A
             INNER JOIN ItensAcervo B
                     ON a.SubCategoriaId = B.SubCategoriaId

      DELETE B
      FROM   deleted A
             INNER JOIN Atributos B
                     ON a.SubCategoriaId = B.SubCategoriaId

      DELETE B
      FROM   deleted A
             INNER JOIN SubCategorias B
                     ON a.SubCategoriaId = B.SubCategoriaId
  END

GO

CREATE TRIGGER TG_CONTATOS_DELETE
ON Contatos
INSTEAD OF DELETE
AS
  BEGIN
      UPDATE A
      SET    ContatoId = NULL
      FROM   ItensAcervo A
             INNER JOIN deleted B
                     ON A.ContatoId = B.ContatoId

      DELETE B
      FROM   deleted A
             INNER JOIN Contatos B
                     ON a.ContatoId = B.ContatoId
  END 
