TechsysLog - Sistema de Gestão Logística



Este projeto é uma API robusta desenvolvida em .NET 8 focada na rastreabilidade e gestão de pedidos e notificações logísticas, utilizando MongoDB como base de dados principal.



Arquitetura e Padrões

O projeto utiliza os princípios da Clean Architecture, garantindo o desacoplamento total entre a lógica de negócio e os detalhes de infraestrutura.



Conceitos Implementados:

&nbsp;   CQRS (Command Query Responsibility Segregation): Separação clara entre operações de escrita (Commands) e leitura (Queries).

&nbsp;   Clean Code \& SOLID: Código escrito priorizando a legibilidade, responsabilidade única e baixo acoplamento.

&nbsp;   Global Exception Handling: Uso do ApiExceptionFilter para garantir respostas JSON padronizadas em caso de erro.

&nbsp;   Dependency Injection: Configuração centralizada para facilitar a evolução e testabilidade do sistema.



Configuração do MongoDB

A aplicação utiliza o MongoDB para persistência de dados. Antes de executar o projeto, é necessário configurar a string de conexão.

Localização: TechsysLog.Web.Api/appsettings.json

Ajuste as credenciais, IP e porta no campo ConnectionString e o nome da base de dados em DatabaseName:



JSON

"MongoDbSettings": {

&nbsp; "ConnectionString": "mongodb://localhost:27017/",

&nbsp; "DatabaseName": "TechsysLogDB"

}



Qualidade e Testes Automatizados

A estrutura do projeto foi concebida para suportar uma pirâmide de testes completa em todas as camadas (Domínio, Aplicação e Infraestrutura).



&nbsp;   Nota de Status: Devido ao cronograma e priorização da entrega da arquitetura base, os testes automatizados não foram implementados nesta fase. No entanto, o código foi escrito seguindo padrões de testabilidade (Injeção de Dependência e Interfaces), permitindo a inclusão de testes unitários e de integração de forma imediata e sem necessidade de refatoração.



Infraestrutura de Notificações

&nbsp;   Decisão Técnica: O EmailService opera atualmente em Modo de Simulação (Console). Esta escolha prioriza a agilidade no desenvolvimento. Graças ao uso de interfaces, a migração para um provedor real (SendGrid, SMTP ou AWS SES) é feita apenas alterando a classe de infraestrutura na camada de IoC.



Tecnologias e Clean Code

&nbsp;   C# 12 / .NET 8

&nbsp;   MongoDB Driver (NoSQL)

&nbsp;   Mapeamento Manual \& DTOs: Para evitar a exposição de documentos internos da base de dados.

&nbsp;   Summaries XML: Documentação técnica embutida diretamente no código para facilitar a manutenção.



Como Executar

&nbsp;   Certifique-se de que o MongoDB Compass ou o serviço do Mongo está rodando em localhost:27017.

&nbsp;   Verifique o appsettings.json conforme as instruções acima.

&nbsp;   No terminal, execute: dotnet restore.

&nbsp;   Inicie a API: dotnet run --project TechsysLog.Web.Api.

