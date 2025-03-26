CREATE TRIGGER novoRegisto ON Testes
	AFTER INSERT
AS
BEGIN

	DECLARE @ID_Produto INTEGER
	DECLARE @Codigo_Resultado INTEGER
	DECLARE @Codigo_Peca CHAR(8)
	DECLARE @Tempo_Producao INT
	DECLARE @Custo_Producao MONEY
	DECLARE @Lucro MONEY
	DECLARE @Prejuizo MONEY

	SELECT 
		@ID_Produto = ID_Produto,
		@Codigo_Resultado = Codigo_Resultado
    FROM INSERTED;

	SELECT TOP 1 
		@Codigo_Peca = Codigo_Peca,
		@Tempo_Producao = Tempo_Producao
	FROM Produto
	WHERE ID_Produto = @ID_Produto;
	
	SET @Custo_Producao = 
	CASE
		WHEN LEFT(@Codigo_Peca, 2)  = 'aa' THEN 1.9 * @Tempo_Producao
		WHEN LEFT(@Codigo_Peca, 2)  = 'ab' THEN 1.3 * @Tempo_Producao
		WHEN LEFT(@Codigo_Peca, 2)  = 'ba' THEN 1.7 * @Tempo_Producao
		WHEN LEFT(@Codigo_Peca, 2) = 'bb' THEN 1.2 * @Tempo_Producao
		ELSE 0
	END

	DECLARE @PrejuizoSemFalha FLOAT;
	DECLARE @CustoFalha FLOAT;

	SET @PrejuizoSemFalha =
    CASE LEFT(@Codigo_Peca, 2)
        WHEN 'aa' THEN 0.9
        WHEN 'ab' THEN 1.1
        WHEN 'ba' THEN 1.2
        WHEN 'bb' THEN 1.3
		ELSE 0
    END;

	SET @CustoFalha =
    CASE @Codigo_Resultado
        WHEN 1 THEN 0
        WHEN 2 THEN 3
        WHEN 3 THEN 2
        WHEN 4 THEN 2.4
        WHEN 5 THEN 1.7
        WHEN 6 THEN 4.5
		ELSE 0
    END;

	SET @Prejuizo = (@PrejuizoSemFalha * @Tempo_Producao) + @CustoFalha;

	SET @Lucro = 
	CASE
		WHEN LEFT(@Codigo_Peca, 2)  = 'aa' THEN 120 - (@Custo_Producao + @Prejuizo)
		WHEN LEFT(@Codigo_Peca, 2)  = 'ab' THEN 100 - (@Custo_Producao + @Prejuizo)
		WHEN LEFT(@Codigo_Peca, 2)  = 'ba' THEN 110 - (@Custo_Producao + @Prejuizo)
		WHEN LEFT(@Codigo_Peca, 2)  = 'bb' THEN 90 - (@Custo_Producao + @Prejuizo)
		ELSE 0
	END

	INSERT INTO Contabilidade.dbo.Custo_Peca
	VALUES (@ID_Produto, @Codigo_Peca, @Tempo_Producao, @Custo_Producao, @Lucro, @Prejuizo)
END

