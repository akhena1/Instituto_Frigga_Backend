--DDL

CREATE DATABASE InstitutoFrigga
--exe ok
USE InstitutoFrigga;

CREATE TABLE Tipo_usuario(
	Tipo_usuario_id INT IDENTITY PRIMARY KEY NOT NULL,
	Tipo VARCHAR(255)
);
CREATE TABLE Categoria_produto(
	Categoria_produto_id INT IDENTITY PRIMARY KEY NOT NULL,
	Tipo_produto VARCHAR (255)
);
CREATE TABLE Categoria_receita(
	Categoria_receita_id INT IDENTITY PRIMARY KEY NOT NULL,
	Tipo_receita VARCHAR(255)
);
--exe ok
CREATE TABLE Usuario(
	Usuario_id INT IDENTITY PRIMARY KEY NOT NULL,
	Nome VARCHAR(255),
	Telefone VARCHAR(255),
	Email VARCHAR(255),
	Biografia VARCHAR(255),
	Senha VARCHAR(255),
	Cnpj_cpf VARCHAR(14),
	Tipo_usuario_id INT FOREIGN KEY REFERENCES Tipo_usuario(Tipo_usuario_id)
);
--exe ok
CREATE TABLE Produto(
	Produto_id INT IDENTITY PRIMARY KEY NOT NULL,
	Tipo VARCHAR(255),
	Categoria_produto_id INT FOREIGN KEY REFERENCES Categoria_produto(Categoria_produto_id)
);
--exe ok
CREATE TABLE Receita(
	Receita_id INT IDENTITY PRIMARY KEY NOT NULL,
	Nome VARCHAR(255),
	Ingredientes TEXT,
	Modo_de_preparo TEXT,
	Imagem_receita VARCHAR(255),
	Categoria_receita_id INT FOREIGN KEY REFERENCES Categoria_receita(Categoria_receita_id),
	Usuario_id INT FOREIGN KEY REFERENCES Usuario(Usuario_id)
);
--exe ok
CREATE TABLE Endereco(
	Endereco_id INT IDENTITY PRIMARY KEY NOT NULL,
	Nome VARCHAR(255),
	Numero VARCHAR(10),
	Bairro VARCHAR (100),
	Complemento VARCHAR(255),
	Cep CHAR(8),
	Cidade VARCHAR(255),
	Usuario_id INT FOREIGN KEY REFERENCES Usuario(Usuario_id)
);
--exe ok
CREATE TABLE Oferta(
	Oferta_id INT IDENTITY PRIMARY KEY NOT NULL,
	Preco FLOAT,
	Peso FLOAT,
	Imagem_produto VARCHAR(255),
	Quantidade INT,
	Usuario_id INT FOREIGN KEY REFERENCES Usuario(Usuario_id),
	Produto_id INT FOREIGN KEY REFERENCES Produto(Produto_id)

);
--exe ok
