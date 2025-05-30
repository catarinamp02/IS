# Projeto de Integração de Sistemas

O projeto desenvolvido consiste na integração de sistemas com o objetivo de transmitir dados gerados por uma linha de produção a diferentes consumidores. A aplicação que simula a linha de produção é designada **Production Line** e gera dados aleatórios sobre peças em processo de fabrico. Esta aplicação consiste numa consola em C# que comunica de forma assíncrona, através do **Rabbit MQ**, com duas outras aplicações: uma aplicação de consola em C# denominada **Data Transmitter**, e uma aplicação com interface gráfica denominada **GUI Falhas**.
A aplicação **Data Transmitter** tem como função receber os dados da produção e enviá-los para uma API REST, que, por sua vez, insere os dados na **base de dados Producao**. Por outro lado, a aplicação  **GUI Falhas**, desenvolvida em Windows Forms, apresenta apenas os dados das peças cuja inspeção revelou falhas.
Paralelamente, a consola **Production Line** envia dados em tempo real para a aplicação **GUI Analytics**, utilizando o **RabbitMQ Stream**. Esta aplicação, também desenvolvida em Windows Forms, apresenta métricas analíticas sobre a produção com base nos dados recebidos via stream. 
Por fim, o sistema integra ainda uma **API SOAP** para disponibilizar dados financeiros. Esta API está ligada à **base de dados Contabilidade**, a qual é automaticamente atualizada com dados provenientes da base de dados Producao, por meio de um **trigger** numa das suas tabelas.

Em seguida apresenta-se um diagrama representativo do projeto:

![Diagrama representativo do projeto](Diagramas/IntegrationDiagram.png)

## 🐰 RabbitMQ
A aplicação Production Line publica mensagens num exchange do tipo *topic* designado **productionLine**. Cada mensagem é publicada com uma *routing key* específica, que descreve o tipo de dado transmitido:
- dados.producao.falha -> sempre que o resultado do teste da peça está entre 2 e 5, inclusivé;
- dados.producao.sucesso -> sempre que o resultado do teste da peça é 1;
- dados.producao.desconhecido -> para qualquer outro resultado do teste da peça;

Esta exchange distribui as mensagens por diferentes filas com base nas *routing keys*:
- A fila **DadosProd** está configurada para receber todas as mensagens com o padrão **dados.produca.#**, ou seja, qualquer mensagens cuja *routing key* comece por dados.producao. Esta fila é consumida pela API REST, uma vez que o objetivo é inserir todas as peças produzidas na base de dados Prdicao;
- A fila **FalhasProd** recebe mensagens com *routing keys* mais específicas: **dados.producao.falha** e **dados.producao.desconhecido**. Ou seja, todos os dados de peças com falhas. Por esse motivo esta fila é consumida pela aplicação **GUI Falhas**, que apresenta apenas os dados de peças com falha;

Em seguida apresenta-se um diagrama que representa o fluxo de dados entre os componentes do sistema através do RabbitMQ:

![Diagrama representativo do projeto](Diagramas/RabbitMQDiagram.png)

## 🐰 RabbitMQ Stream



## 🧼 SOAP API
