CREATE DATABASE Producao;
GO

USE Producao;
GO
drop database Producao
-----------------------------------------
------------Create TABLES----------------
-----------------------------------------

CREATE TABLE Produto (
    ID_Produto INT IDENTITY(1,1) PRIMARY KEY,
	Codigo_Peca CHAR(8) NOT NULL UNIQUE CHECK (
    Codigo_Peca LIKE '[a-b][a-b][0-9][0-9][0-9][0-9][0-9][0-9]'),
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
GO

-----------------------------------------
----------Create FUNCTIONS----------------
-----------------------------------------

--Produtos
CREATE FUNCTION ValidarCodigoPeca (@Codigo_Peca CHAR(8))
RETURNS BIT
AS
BEGIN
 IF @Codigo_Peca LIKE '[a-b][a-b][0-9][0-9][0-9][0-9][0-9][0-9]'
    
	RETURN 1;  -- Código válido
    RETURN 0;  -- Código inválido
END;
GO

CREATE FUNCTION ExisteCodigoPeca (@Codigo_Peca CHAR(8))
RETURNS BIT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Produto WHERE Codigo_Peca = @Codigo_Peca)
        RETURN 1;  -- Existe
    RETURN 0;  -- Não existe
END;
GO

CREATE FUNCTION ValidarDataProducao (@Data_Producao DATE)
RETURNS BIT
AS
BEGIN
    -- A data de produção não pode ser no futuro
    IF @Data_Producao <= CAST(GETDATE() AS DATE)
        RETURN 1;  -- Data válida
    RETURN 0;  -- Data inválida
END;
GO

CREATE FUNCTION ValidarHoraProducao (@Data_Producao DATE, @Hora_Producao TIME(0))
RETURNS BIT
AS
BEGIN
    -- Se a data da produção for hoje, a hora não pode estar no futuro
    IF @Data_Producao = CAST(GETDATE() AS DATE) AND @Hora_Producao > CAST(GETDATE() AS TIME(0))
        RETURN 0;  -- Hora inválida
    RETURN 1;  -- Hora válida
END;
GO

CREATE FUNCTION ValidarTempoProducao (@Tempo_Producao INT)
RETURNS BIT
AS
BEGIN
    IF @Tempo_Producao BETWEEN 10 AND 50
        RETURN 1;  -- Tempo válido
    RETURN 0;  -- Tempo inválido
END;
GO


--Testes
CREATE FUNCTION ExisteTesteParaProduto (@ID_Produto INT)
RETURNS BIT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Testes WHERE ID_Produto = @ID_Produto)
        RETURN 1;  -- Já existe um teste
    RETURN 0;  -- Não existe teste
END;
GO

CREATE FUNCTION TesteExiste (@ID_Teste INT)
RETURNS BIT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Testes WHERE ID_Teste = @ID_Teste)
        RETURN 1;  -- O teste existe
    RETURN 0;  -- O teste não existe
END;
GO

CREATE FUNCTION ProdutoExiste (@ID_Produto INT)
RETURNS BIT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Produto WHERE ID_Produto = @ID_Produto)
        RETURN 1;  -- Produto existe
    RETURN 0;  -- Produto não existe
END;
GO

CREATE FUNCTION ValidarDataTeste (@Data_Teste DATE)
RETURNS BIT
AS
BEGIN
    IF @Data_Teste <= CAST(GETDATE() AS DATE)
        RETURN 1;  -- Data válida
    RETURN 0;  -- Data inválida (no futuro)
END;
GO
CREATE FUNCTION ValidarCodigoResultado (@Codigo_Resultado CHAR(2))
RETURNS BIT
AS
BEGIN
    IF @Codigo_Resultado IN ('01', '02', '03', '04', '05', '06')
        RETURN 1;  -- Código válido
    RETURN 0;  -- Código inválido
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
    @Tempo_Producao INT,
	@Mensagem NVARCHAR(100) OUTPUT
AS
BEGIN

    SET NOCOUNT ON;

	-- Verifica se Código já Existe
	IF dbo.ExisteCodigoPeca(@Codigo_Peca) = 1
	BEGIN
		RAISERROR('Código da Peça já existe!',16,1);	
		RETURN;
	END;

	-- Verifica se o Código é Inválido
	IF dbo.ValidarCodigoPeca(@Codigo_Peca) = 0
	BEGIN
		RAISERROR('Código da Peça inválido!',16,1);
		RETURN;
	END;

	-- Verifica se a Data de Produção é Inválida
	IF dbo.ValidarDataProducao(@Data_Producao) = 0
	BEGIN
		RAISERROR('Data de Produção inválida! A Data de Produção não pode ser superior à Data de hoje e/ou tem de estar no formato yyyy-mm-dd !',16,1);
		RETURN;
	END;

	-- Verifica se a Hora de Produção é Inválida
	IF dbo.ValidarHoraProducao(@Data_Producao, @Hora_Producao) = 0
    BEGIN
        RAISERROR('Hora de Produção inválida! A Hora de Produção não pode ser superior à Hora de hoje e/ou tem de estar no formato hh:mm:ss !',16,1);
        RETURN;
    END

	-- Verifica se o Tempo de Produção é Inválido
	IF dbo.ValidarTempoProducao(@Tempo_Producao) = 0
	BEGIN
		RAISERROR('Tempo de produção inválido! Tem de ser entre 10 e 50 segundos!',16,1);
		RETURN;
	END;

    -- Insere o novo produto
    INSERT INTO Produto (Codigo_Peca, Data_Producao, Hora_Producao, Tempo_Producao)
    VALUES (@Codigo_Peca, @Data_Producao, @Hora_Producao, @Tempo_Producao);

     SET @Mensagem =  'Produto inserido com sucesso!';
END;
GO

-- Inserir Novo Teste
CREATE PROCEDURE InserirTeste
    @ID_Produto INT,
    @Codigo_Resultado CHAR(2),
    @Data_Teste DATE,
	@Mensagem NVARCHAR(100) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica se o Produto existe
    IF dbo.ProdutoExiste(@ID_Produto) = 0
    BEGIN
        RAISERROR('O produto não existe!',16,1);
        RETURN;
    END
	
	-- Verifica se existe já um Teste para um Produto existente (**** ver se já existe um teste)
    IF dbo.ExisteTesteParaProduto(@ID_Produto) = 1
    BEGIN
        RAISERROR('Já existe um teste para este produto!',16,1);
        RETURN;
    END

	 -- Verifica se a Data_Teste é válida
    IF dbo.ValidarDataTeste(@Data_Teste) = 0
    BEGIN
        RAISERROR('Data de Teste inválida! A Data de Teste não pode ser superior à Data de hoje e/ou tem de estar no formato yyyy-mm-dd !',16,1);
        RETURN;
    END

    -- Valida o Código do Resultado do Teste
    IF dbo.ValidarCodigoResultado(@Codigo_Resultado) = 0
    BEGIN
        --PRINT 'Aviso: Código do teste desconhecido!';
        SET @Codigo_Resultado = '06';
    END

    -- Insere o teste
    INSERT INTO Testes (ID_Produto, Codigo_Resultado, Data_Teste)
    VALUES (@ID_Produto, @Codigo_Resultado, @Data_Teste);

    SET @Mensagem = 'Teste inserido com sucesso!';
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
        RAISERROR('O produto não existe!',16,1);
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
        RAISERROR('O produto não existe!',16,1);
        RETURN;
    END

	-- Verifica se existe Testes para um Produto existente
    IF dbo.ExisteTesteParaProduto(@ID_Produto) = 0
    BEGIN
		RAISERROR('Não existem testes para este produto!',16,1);
        RETURN;
    END

	SELECT 
        T.ID_Teste, 
        T.ID_Produto, 
        T.Codigo_Resultado, 
        T.Data_Teste
    FROM Testes T
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
    @Tempo_Producao INT,
	@Mensagem NVARCHAR(100) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica se o Produto existe
    IF dbo.ProdutoExiste(@ID_Produto) = 0
    BEGIN
        RAISERROR('O produto não existe!',16,1);
        RETURN;
    END

    DECLARE @CodigoAtual CHAR(8);
    SELECT @CodigoAtual = Codigo_Peca FROM Produto WHERE ID_Produto = @ID_Produto;

    -- Se o código da peça for diferente, verificar se já existe em outro produto
    IF @Codigo_Peca <> @CodigoAtual
    BEGIN
        IF dbo.ExisteCodigoPeca(@Codigo_Peca) = 1
        BEGIN
            RAISERROR('Código da Peça já existe!',16,1);
            RETURN;
        END
    END

	-- Verifica se o Código é Inválido
	IF dbo.ValidarCodigoPeca(@Codigo_Peca) = 0
	BEGIN
		RAISERROR('Código da Peça inválido!',16,1);
		RETURN;
	END;

	-- Verifica se a Data de Produção é Inválida
	IF dbo.ValidarDataProducao(@Data_Producao) = 0
	BEGIN
		RAISERROR('Data de Produção inválida! A Data de Produção não pode ser superior à Data de hoje e/ou tem de estar no formato yyyy-mm-dd !',16,1);
		RETURN;
	END;

	-- Verifica se a Hora de Produção é Inválida
	IF dbo.ValidarHoraProducao(@Data_Producao, @Hora_Producao) = 0
    BEGIN
        RAISERROR('Hora de Produção inválida! A Hora de Produção não pode ser superior à Hora de hoje e/ou tem de estar no formato hh:mm:ss !',16,1);
        RETURN;
    END

	-- Verifica se o Tempo de Produção é Inválido
	IF dbo.ValidarTempoProducao(@Tempo_Producao) = 0
	BEGIN
		RAISERROR('Tempo de produção inválido! Tem de ser entre 10 e 50 segundos!',16,1);
		RETURN;
	END;

    -- Atualiza o Produto
    UPDATE Produto
    SET Codigo_Peca = @Codigo_Peca, 
        Data_Producao = @Data_Producao,
        Hora_Producao = @Hora_Producao,
        Tempo_Producao = @Tempo_Producao
    WHERE ID_Produto = @ID_Produto;

    SET @Mensagem = 'Produto atualizado com sucesso!';
END;
GO

CREATE PROCEDURE AtualizarTeste
    @ID_Teste INT,
    @ID_Produto INT,
    @Codigo_Resultado CHAR(2),
    @Data_Teste DATE,
	@Mensagem NVARCHAR(100) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

	    -- Verifica se o Produto existe
    IF dbo.ProdutoExiste(@ID_Produto) = 0
    BEGIN
		RAISERROR('O produto não existe!',16,1);
        RETURN;
    END

    -- Verifica se o Teste existe
    IF dbo.TesteExiste(@ID_Teste) = 0
    BEGIN
		RAISERROR('O teste para este produto, ainda não existe!',16,1);
        RETURN;
    END

	 -- Verifica se a Data_Teste é válida
    IF dbo.ValidarDataTeste(@Data_Teste) = 0
    BEGIN
        RAISERROR('Data de Teste inválida! A Data de Teste não pode ser superior à Data de hoje e/ou tem de estar no formato yyyy-mm-dd !',16,1);
        RETURN;
    END

    -- Valida o Código do Resultado do Teste
    IF dbo.ValidarCodigoResultado(@Codigo_Resultado) = 0
    BEGIN
        --PRINT 'Aviso: Código do teste desconhecido!';
        SET @Codigo_Resultado = '06';
    END

    -- Atualiza o Teste
    UPDATE Testes
    SET ID_Produto = @ID_Produto,
        Codigo_Resultado = @Codigo_Resultado,
        Data_Teste = @Data_Teste
    WHERE ID_Teste = @ID_Teste;

    SET @Mensagem = 'Teste atualizado com sucesso!';
END;
GO

CREATE PROCEDURE RemoverProduto
    @ID_Produto INT,
	@Mensagem NVARCHAR(100) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica se o Produto existe
    IF dbo.ProdutoExiste(@ID_Produto) = 0
    BEGIN
        RAISERROR('O produto não existe!',16,1);
        RETURN;
    END

    -- Remove primeiro os testes associados
    DELETE FROM Testes WHERE ID_Produto = @ID_Produto;

    -- Remove o Produto
    DELETE FROM Produto WHERE ID_Produto = @ID_Produto;

    SET @Mensagem = 'Produto removido com sucesso!';
END;
GO

CREATE PROCEDURE RemoverTeste
    @ID_Teste INT,
	@Mensagem NVARCHAR(100) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica se o Teste existe
    IF dbo.TesteExiste(@ID_Teste) = 0
    BEGIN
		RAISERROR('O teste não existe!',16,1);
        RETURN;
    END

    -- Remove o Teste
    DELETE FROM Testes WHERE ID_Teste = @ID_Teste;

    SET @Mensagem = 'Teste removido com sucesso!';
END;
GO