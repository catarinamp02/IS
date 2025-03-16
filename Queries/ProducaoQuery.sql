CREATE DATABASE Producao;
GO

USE Producao;
GO

-----------------------------------------
------------Create TABLES----------------
-----------------------------------------

CREATE TABLE Produto (
    ID_Produto INT IDENTITY(1,1) PRIMARY KEY,
    Codigo_Peca CHAR(8) NOT NULL UNIQUE CHECK (
        Codigo_Peca LIKE 'aa______' 
        OR Codigo_Peca LIKE 'ab______' 
        OR Codigo_Peca LIKE 'ba______' 
        OR Codigo_Peca LIKE 'bb______'
    ),
    Data_Producao DATE NOT NULL,
    Hora_Producao TIME(0) NOT NULL,
    Tempo_Producao INT CHECK (Tempo_Producao BETWEEN 10 AND 50) NOT NULL
);
GO


CREATE TABLE Testes (
    ID_Teste INT IDENTITY(1,1) PRIMARY KEY,
    ID_Produto INT NOT NULL,
    Codigo_Resultado CHAR(2) NOT NULL CHECK (Codigo_Resultado IN ('01', '02', '03', '04', '05', '06')),
    Data_Teste DATE NOT NULL,
    FOREIGN KEY (ID_Produto) REFERENCES Produto(ID_Produto)
);


-----------------------------------------
----------Create FUNCTIONS----------------
-----------------------------------------

--Produtos
CREATE FUNCTION ValidarCodigoPeca (@Codigo_Peca CHAR(8))
RETURNS BIT
AS
BEGIN
    IF @Codigo_Peca LIKE 'aa______' OR 
       @Codigo_Peca LIKE 'ab______' OR 
       @Codigo_Peca LIKE 'ba______' OR 
       @Codigo_Peca LIKE 'bb______'
       RETURN 1;  -- C�digo v�lido
    RETURN 0;  -- C�digo inv�lido
END;
GO

CREATE FUNCTION ExisteCodigoPeca (@Codigo_Peca CHAR(8))
RETURNS BIT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Produto WHERE Codigo_Peca = @Codigo_Peca)
        RETURN 1;  -- Existe
    RETURN 0;  -- N�o existe
END;
GO

CREATE FUNCTION ValidarDataProducao (@Data_Producao DATE)
RETURNS BIT
AS
BEGIN
    -- A data de produ��o n�o pode ser no futuro
    IF @Data_Producao <= CAST(GETDATE() AS DATE)
        RETURN 1;  -- Data v�lida
    RETURN 0;  -- Data inv�lida
END;
GO

CREATE FUNCTION ValidarHoraProducao (@Data_Producao DATE, @Hora_Producao TIME(0))
RETURNS BIT
AS
BEGIN
    -- Se a data da produ��o for hoje, a hora n�o pode estar no futuro
    IF @Data_Producao = CAST(GETDATE() AS DATE) AND @Hora_Producao > CAST(GETDATE() AS TIME(0))
        RETURN 0;  -- Hora inv�lida
    RETURN 1;  -- Hora v�lida
END;
GO

CREATE FUNCTION ValidarTempoProducao (@Tempo_Producao INT)
RETURNS BIT
AS
BEGIN
    IF @Tempo_Producao BETWEEN 10 AND 50
        RETURN 1;  -- Tempo v�lido
    RETURN 0;  -- Tempo inv�lido
END;
GO


--Testes
CREATE FUNCTION ExisteTesteParaProduto (@ID_Produto INT)
RETURNS BIT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Testes WHERE ID_Produto = @ID_Produto)
        RETURN 1;  -- J� existe um teste
    RETURN 0;  -- N�o existe teste
END;
GO

CREATE FUNCTION TesteExiste (@ID_Teste INT)
RETURNS BIT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Testes WHERE ID_Teste = @ID_Teste)
        RETURN 1;  -- O teste existe
    RETURN 0;  -- O teste n�o existe
END;
GO

CREATE FUNCTION ProdutoExiste (@ID_Produto INT)
RETURNS BIT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Produto WHERE ID_Produto = @ID_Produto)
        RETURN 1;  -- Produto existe
    RETURN 0;  -- Produto n�o existe
END;
GO

CREATE FUNCTION ValidarDataTeste (@Data_Teste DATE)
RETURNS BIT
AS
BEGIN
    IF @Data_Teste <= CAST(GETDATE() AS DATE)
        RETURN 1;  -- Data v�lida
    RETURN 0;  -- Data inv�lida (no futuro)
END;
GO
CREATE FUNCTION ValidarCodigoResultado (@Codigo_Resultado CHAR(2))
RETURNS BIT
AS
BEGIN
    IF @Codigo_Resultado IN ('01', '02', '03', '04', '05', '06')
        RETURN 1;  -- C�digo v�lido
    RETURN 0;  -- C�digo inv�lido
END;
GO


-----------------------------------------
------------Stored Procedures------------
-----------------------------------------


-- Inserir Novo Produto
CREATE PROCEDURE InserirProduto
    @Codigo_Peca CHAR(8),
    @Data_Producao DATE,
    @Hora_Producao TIME(0),
    @Tempo_Producao INT
AS
BEGIN

    SET NOCOUNT ON;

	-- Verifica se C�digo j� Existe
	IF dbo.ExisteCodigoPeca(@Codigo_Peca) = 1
	BEGIN
		PRINT 'Erro: C�digo da pe�a j� existe!';
		RETURN;
	END;

	-- Verifica se o C�digo � Inv�lido
	IF dbo.ValidarCodigoPeca(@Codigo_Peca) = 0
	BEGIN
		PRINT 'Erro: C�digo da pe�a inv�lido!';
		RETURN;
	END;

	-- Verifica se a Data de Produ��o � Inv�lida
	IF dbo.ValidarDataProducao(@Data_Producao) = 0
	BEGIN
		PRINT 'Erro: A data de produ��o n�o pode ser no futuro!';
		RETURN;
	END;

	-- Verifica se a Hora de Produ��o � Inv�lida
	IF dbo.ValidarHoraProducao(@Data_Producao, @Hora_Producao) = 0
    BEGIN
        PRINT 'Erro: A hora de produ��o n�o pode ser no futuro!';
        RETURN;
    END

	-- Verifica se o Tempo de Produ��o � Inv�lido
	IF dbo.ValidarTempoProducao(@Tempo_Producao) = 0
	BEGIN
		PRINT 'Erro: Tempo de produ��o inv�lido!';
		RETURN;
	END;

    -- Insere o novo produto
    INSERT INTO Produto (Codigo_Peca, Data_Producao, Hora_Producao, Tempo_Producao)
    VALUES (@Codigo_Peca, @Data_Producao, @Hora_Producao, @Tempo_Producao);

    PRINT 'Produto inserido com sucesso!';
END;
GO

-- Inserir Novo Teste
CREATE PROCEDURE InserirTeste
    @ID_Produto INT,
    @Codigo_Resultado CHAR(2),
    @Data_Teste DATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica se o Produto existe
    IF dbo.ProdutoExiste(@ID_Produto) = 0
    BEGIN
        PRINT 'Erro: O produto n�o existe!';
        RETURN;
    END
	
	-- Verifica se existe j� um Teste para um Produto existente (**** ver se j� existe um teste)
    IF dbo.ExisteTesteParaProduto(@ID_Produto) = 1
    BEGIN
        PRINT 'Erro: J� existe um teste para este produto!';
        RETURN;
    END

	 -- Verifica se a Data_Teste � v�lida
    IF dbo.ValidarDataTeste(@Data_Teste) = 0
    BEGIN
        PRINT 'Erro: Data do teste inv�lida (n�o pode estar no futuro)!';
        RETURN;
    END

    -- Valida o C�digo do Resultado do Teste
    IF dbo.ValidarCodigoResultado(@Codigo_Resultado) = 0
    BEGIN
        PRINT 'Aviso: C�digo do teste desconhecido!';
        SET @Codigo_Resultado = '06';
    END

    -- Insere o teste
    INSERT INTO Testes (ID_Produto, Codigo_Resultado, Data_Teste)
    VALUES (@ID_Produto, @Codigo_Resultado, @Data_Teste);

    PRINT 'Teste inserido com sucesso!';
END;
GO
-----------------------------------------

-- Consultar todos os Produtos
CREATE PROCEDURE ConsultarProdutos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Produto;
END;
GO

-- Consultar Todos os testes
CREATE PROCEDURE ConsultarTestes
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Testes;
END;
GO

CREATE PROCEDURE ConsultarUmProduto
    @ID_Produto INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica se o Produto existe
    IF dbo.ProdutoExiste(@ID_Produto) = 0
    BEGIN
        PRINT 'Erro: O produto n�o existe!';
        RETURN;
    END

    -- Retorna os dados do Produto
    SELECT ID_Produto, Codigo_Peca, Data_Producao, Hora_Producao, Tempo_Producao
    FROM Produto
    WHERE ID_Produto = @ID_Produto;
END;
GO

CREATE PROCEDURE ConsultarTestesPorProduto
    @ID_Produto INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica se o Produto existe
    IF dbo.ProdutoExiste(@ID_Produto) = 0
    BEGIN
        PRINT 'Erro: O produto n�o existe!';
        RETURN;
    END

	-- Verifica se existe Testes para um Produto existente
    IF dbo.ExisteTesteParaProduto(@ID_Produto) = 0
    BEGIN
        PRINT 'Erro: N�o existem testes para este produto!';
        RETURN;
    END

    -- Retorna os testes do Produto
    SELECT T.ID_Teste, T.ID_Produto, P.Codigo_Peca, T.Codigo_Resultado, T.Data_Teste
    FROM Testes T
    INNER JOIN Produto P ON T.ID_Produto = P.ID_Produto
    WHERE T.ID_Produto = @ID_Produto;
END;
GO
-----------------------------------------

--Atualizar Produto Existente
CREATE PROCEDURE AtualizarProduto
    @ID_Produto INT,
    @Codigo_Peca CHAR(8),
    @Data_Producao DATE,
    @Hora_Producao TIME(0),
    @Tempo_Producao INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica se o Produto existe
    IF dbo.ProdutoExiste(@ID_Produto) = 0
    BEGIN
        PRINT 'Erro: O produto n�o existe!';
        RETURN;
    END

    DECLARE @CodigoAtual CHAR(8);
    SELECT @CodigoAtual = Codigo_Peca FROM Produto WHERE ID_Produto = @ID_Produto;

    -- Se o c�digo da pe�a for diferente, verificar se j� existe em outro produto
    IF @Codigo_Peca <> @CodigoAtual
    BEGIN
        IF dbo.ExisteCodigoPeca(@Codigo_Peca) = 1
        BEGIN
            PRINT 'Erro: C�digo da pe�a j� existe!';
            RETURN;
        END
    END

	-- Verifica se o C�digo � Inv�lido
	IF dbo.ValidarCodigoPeca(@Codigo_Peca) = 0
	BEGIN
		PRINT 'Erro: C�digo da pe�a inv�lido!';
		RETURN;
	END;

	-- Verifica se a Data de Produ��o � Inv�lida
	IF dbo.ValidarDataProducao(@Data_Producao) = 0
	BEGIN
		PRINT 'Erro: A data de produ��o n�o pode ser no futuro!';
		RETURN;
	END;

	-- Verifica se a Hora de Produ��o � Inv�lida
	IF dbo.ValidarHoraProducao(@Data_Producao, @Hora_Producao) = 0
    BEGIN
        PRINT 'Erro: A hora de produ��o n�o pode ser no futuro!';
        RETURN;
    END

	-- Verifica se o Tempo de Produ��o � Inv�lido
	IF dbo.ValidarTempoProducao(@Tempo_Producao) = 0
	BEGIN
		PRINT 'Erro: Tempo de produ��o inv�lido!';
		RETURN;
	END;

    -- Atualiza o Produto
    UPDATE Produto
    SET Codigo_Peca = @Codigo_Peca, 
        Data_Producao = @Data_Producao,
        Hora_Producao = @Hora_Producao,
        Tempo_Producao = @Tempo_Producao
    WHERE ID_Produto = @ID_Produto;

    PRINT 'Produto atualizado com sucesso!';
END;
GO

CREATE PROCEDURE AtualizarTeste
    @ID_Teste INT,
    @ID_Produto INT,
    @Codigo_Resultado CHAR(2),
    @Data_Teste DATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica se o Teste existe
    IF dbo.TesteExiste(@ID_Teste) = 0
    BEGIN
        PRINT 'Erro: O teste n�o existe!';
        RETURN;
    END

    -- Verifica se o Produto existe
    IF dbo.ProdutoExiste(@ID_Produto) = 0
    BEGIN
        PRINT 'Erro: O produto associado n�o existe!';
        RETURN;
    END

	 -- Verifica se a Data_Teste � v�lida
    IF dbo.ValidarDataTeste(@Data_Teste) = 0
    BEGIN
        PRINT 'Erro: Data do teste inv�lida (n�o pode estar no futuro)!';
        RETURN;
    END

    -- Valida o C�digo do Resultado do Teste
    IF dbo.ValidarCodigoResultado(@Codigo_Resultado) = 0
    BEGIN
        PRINT 'Aviso: C�digo do teste desconhecido!';
        SET @Codigo_Resultado = '06';
    END

    -- Atualiza o Teste
    UPDATE Testes
    SET ID_Produto = @ID_Produto,
        Codigo_Resultado = @Codigo_Resultado,
        Data_Teste = @Data_Teste
    WHERE ID_Teste = @ID_Teste;

    PRINT 'Teste atualizado com sucesso!';
END;
GO

CREATE PROCEDURE RemoverProduto
    @ID_Produto INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica se o Produto existe
    IF dbo.ProdutoExiste(@ID_Produto) = 0
    BEGIN
        PRINT 'Erro: O produto n�o existe!';
        RETURN;
    END

    -- Remove primeiro os testes associados
    DELETE FROM Testes WHERE ID_Produto = @ID_Produto;

    -- Remove o Produto
    DELETE FROM Produto WHERE ID_Produto = @ID_Produto;

    PRINT 'Produto removido com sucesso!';
END;
GO

CREATE PROCEDURE RemoverTeste
    @ID_Teste INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica se o Teste existe
    IF dbo.TesteExiste(@ID_Teste) = 0
    BEGIN
        PRINT 'Erro: O teste n�o existe!';
        RETURN;
    END

    -- Remove o Teste
    DELETE FROM Testes WHERE ID_Teste = @ID_Teste;

    PRINT 'Teste removido com sucesso!';
END;
GO


-----------------------------------------
----------EXEC Stored Procedures---------
-----------------------------------------

EXEC InserirProduto 'aa124296', '2025-03-15', '12:30:00', 30; 

EXEC InserirTeste 2, '02', '2025-03-15'; 

EXEC AtualizarProduto 2, 'aa124296', '2025-03-02', '15:30:00', 30; 

EXEC AtualizarTeste 1, 1, '03', '2024-03-15';

EXEC ConsultarProdutos;

EXEC ConsultarUmProduto 1;

EXEC ConsultarTestesPorProduto 1;

EXEC ConsultarTestes;

EXEC RemoverProduto 1;

EXEC RemoverTeste 1;

