CREATE TABLE Custo_Peca(
	ID_Custo INTEGER IDENTITY PRIMARY KEY,
	ID_Produto INTEGER NOT NULL,
	Codigo_Peca CHAR(8) NOT NULL,
	Tempo_Producao INT NOT NULL,
	Custo_Producao MONEY,
	Lucro MONEY, 
	Prejuizo MONEY
);

--StoredProceduresTR2

USE Contabilidade;
GO

CREATE PROCEDURE PecaComMaiorPrejuizo
AS
BEGIN
    SELECT TOP 1 
        Codigo_Peca,
        Prejuizo
    FROM Custo_Peca
    ORDER BY Prejuizo DESC
END
GO


CREATE PROCEDURE CustoTotalPeriodo
    @DataInicio DATE,
    @HoraInicio TIME(0),
    @DataFim DATE,
    @HoraFim TIME(0)
AS
BEGIN
    DECLARE @DataHoraInicio DATETIME = CAST(@DataInicio AS DATETIME) + CAST(@HoraInicio AS DATETIME);
    DECLARE @DataHoraFim DATETIME = CAST(@DataFim AS DATETIME) + CAST(@HoraFim AS DATETIME);

    SELECT 
        SUM(C.Custo_Producao) AS Custo_Total
    FROM Custo_Peca C
    INNER JOIN Producao.dbo.Produto P 
        ON C.ID_Produto = P.ID_Produto
    WHERE 
        (CAST(P.Data_Producao AS DATETIME) + CAST(P.Hora_Producao AS DATETIME))
        BETWEEN @DataHoraInicio AND @DataHoraFim
END
GO


CREATE PROCEDURE LucroTotalPeriodo
    @DataInicio DATE,
    @HoraInicio TIME(0),
    @DataFim DATE,
    @HoraFim TIME(0)
AS
BEGIN
    DECLARE @DataHoraInicio DATETIME = CAST(@DataInicio AS DATETIME) + CAST(@HoraInicio AS DATETIME);
    DECLARE @DataHoraFim DATETIME = CAST(@DataFim AS DATETIME) + CAST(@HoraFim AS DATETIME);

    SELECT 
        SUM(C.Lucro) AS Lucro_Total
    FROM Contabilidade.dbo.Custo_Peca C
    INNER JOIN Producao.dbo.Produto P 
        ON C.ID_Produto = P.ID_Produto
    WHERE 
        (CAST(P.Data_Producao AS DATETIME) + CAST(P.Hora_Producao AS DATETIME))
        BETWEEN @DataHoraInicio AND @DataHoraFim
END
GO



CREATE PROCEDURE PrejuizoPorPecaPeriodo
    @DataInicio DATE,
    @HoraInicio TIME(0),
    @DataFim DATE,
    @HoraFim TIME(0)
AS
BEGIN
    DECLARE @DataHoraInicio DATETIME = CAST(@DataInicio AS DATETIME) + CAST(@HoraInicio AS DATETIME);
    DECLARE @DataHoraFim DATETIME = CAST(@DataFim AS DATETIME) + CAST(@HoraFim AS DATETIME);

    SELECT 
        C.Codigo_Peca,
        SUM(C.Prejuizo) AS Prejuizo_Total
    FROM Contabilidade.dbo.Custo_Peca C
    INNER JOIN Producao.dbo.Produto P 
        ON C.ID_Produto = P.ID_Produto
    WHERE 
        (CAST(P.Data_Producao AS DATETIME) + CAST(P.Hora_Producao AS DATETIME))
        BETWEEN @DataHoraInicio AND @DataHoraFim
    GROUP BY C.Codigo_Peca
END
GO


CREATE PROCEDURE FinanceiroPorPeca
    @CodigoPeca CHAR(8)
AS
BEGIN
    SELECT TOP 1
        --ID_Produto,
        Codigo_Peca,
        Tempo_Producao,
        Custo_Producao,
        Lucro,
        Prejuizo
    FROM Custo_Peca
    WHERE Codigo_Peca = @CodigoPeca
END
GO


--Drop procedure PrejuizoPorPecaPeriodo

--exec FinanceiroPorPeca 'ab398032'


--exec PrejuizoPorPecaPeriodo
--    @DataInicio = '2022-06-08',
--    @HoraInicio = '16:05:51',
--    @DataFim = '2024-09-26',
--    @HoraFim = '19:05:51';

