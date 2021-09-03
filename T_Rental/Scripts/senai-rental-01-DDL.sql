CREATE DATABASE T_Rental;
GO

USE T_Rental;
GO

CREATE TABLE EMPRESA (
  idEmpresa INT PRIMARY KEY IDENTITY(1,1),
  nomeEmpresa CHAR(50) NOT NULL
);
GO

CREATE TABLE MARCA (
  idMarca TINYINT PRIMARY KEY IDENTITY(1,1),
  nomeMarca CHAR(50) NOT NULL
);
GO

CREATE TABLE MODELO (
  idModelo INT PRIMARY KEY IDENTITY(1,1),
  idMarca TINYINT FOREIGN KEY REFERENCES MARCA(idMarca),
  nomeModelo CHAR(50) NOT NULL
);
GO

CREATE TABLE CLIENTE (
  idCliente INT PRIMARY KEY IDENTITY(1,1),
  nomeCliente VARCHAR(50) NOT NULL,
  sobrenomeCliente VARCHAR(50) NOT NULL,
  rgCliente CHAR(9) NOT NULL,
  cpfCliente CHAR(11) NOT NULL
);
GO

CREATE TABLE VEICULO (
  idVeiculo SMALLINT PRIMARY KEY IDENTITY(1,1),
  idEmpresa INT FOREIGN KEY REFERENCES EMPRESA(idEmpresa),
  idModelo INT FOREIGN KEY REFERENCES MODELO(idModelo),
  placa CHAR(7) NOT NULL
);
GO

CREATE TABLE ALUGUEL (
  idAluguel INT PRIMARY KEY IDENTITY(1,1),
  idCliente INT FOREIGN KEY REFERENCES CLIENTE(idCliente),
  idVeiculo SMALLINT FOREIGN KEY REFERENCES VEICULO(idVeiculo),
  dataRetirada DATE,
  dataDevolucao DATE
);
GO