CREATE TABLE Custo_Peca(
	ID_Custo INTEGER IDENTITY PRIMARY KEY,
	ID_Produto INTEGER NOT NULL,
	Codigo_Peca CHAR(8) NOT NULL,
	Tempo_Producao INT NOT NULL,
	Custo_Producao MONEY,
	Lucro MONEY, 
	Prejuizo MONEY
);
