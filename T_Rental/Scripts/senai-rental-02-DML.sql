USE T_Rental;
GO

INSERT INTO EMPRESA (nomeEmpresa)
VALUES ('Localiza'), ('Unidas')

INSERT INTO MARCA (nomeMarca)
VALUES ('Fiat'),('VW'),('Ford'),('Mercedes');

INSERT INTO MODELO (idMarca, nomeModelo)
VALUES (3, 'Fiesta'), (4, 'C180'), (2, 'Gol'), (1, 'Argo');

INSERT INTO VEICULO (idModelo,idEmpresa,placa)
VALUES (2,1,'DAF3944'), (3,2,'LPS4731'), (4,1,'SAP3292'), (1,2,'WOP1434');

INSERT INTO CLIENTE (nomeCliente,sobrenomeCliente,rgCliente,cpfCliente)
VALUES ('Joao','Silva',419341915,74791446996), ('Samuel','Oliveira',914121646,31447691735),('Lucas','Santos',256146147,47964747774),('Ana','Souza',367119814,25874178989);

INSERT INTO ALUGUEL (idVeiculo,idCliente,dataRetirada,dataDevolucao)
VALUES (2,2,'2021/03/20','2021/03/25'), (4,3,'2021/04/21','2021/04/26'), (2,4,'2021/05/22','2021/05/27'), (3,1,'2021/06/23','2021/06/28');
