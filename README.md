# Projeto de Integração de Sistemas

O projeto desenvolvido consiste na integração de sistemas com o objetivo de transmitir dados gerados por uma linha de produção a diferentes consumidores. A aplicação que simula a linha de produção é designada **Production Line** e gera dados aleatórios sobre peças em processo de fabrico. Esta aplicação consiste numa consola em C# que comunica de forma assíncrona, através do **Rabbit MQ**, com duas outras aplicações: uma aplicação de consola em C# denominada **Data Transmitter**, e uma aplicação com interface gráfica denominada **GUI Falhas**.
A aplicação **Data Transmitter** tem como função receber os dados da produção e enviá-los para uma API REST, que, por sua vez, insere os dados na **base de dados Producao**. Por outro lado, a aplicação  **GUI Falhas**, desenvolvida em Windows Forms, apresenta apenas os dados das peças cuja inspeção revelou falhas.
Paralelamente, a consola **Production Line** envia dados em tempo real para a aplicação **GUI Analytics**, utilizando o **RabbitMQ Stream**. Esta aplicação, também desenvolvida em Windows Forms, apresenta métricas analíticas sobre a produção com base nos dados recebidos via stream. 
Por fim, o sistema integra ainda uma **API SOAP** para disponibilizar dados financeiros. Esta API está ligada à **base de dados Contabilidade**, a qual é automaticamente atualizada com dados provenientes da base de dados Producao, por meio de um **trigger** numa das suas tabelas.

Em seguida apresenta-se um diagrama representativo do projeto:

![Diagrama representativo do projeto](Diagramas/IntegrationDiagram.png)

🐰 ##RabbitMQ

![Diagrama representativo do projeto](Diagramas/RabbitMQDiagram.png)
