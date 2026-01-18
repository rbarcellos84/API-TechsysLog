TechsysLog - Sistema de Gestão Logística

Este projeto é uma solução full-stack composta por um Dashboard em Angular e uma API robusta em .NET 8, focada na rastreabilidade e gestão de pedidos e notificações logísticas, utilizando MongoDB como base de dados principal.



Arquitetura e Padrões

O projeto utiliza os princípios da Clean Architecture, garantindo o desacoplamento total entre a lógica de negócio e os detalhes de infraestrutura.



CQRS (Command Query Responsibility Segregation): Separação clara entre operações de escrita e leitura.



Clean Code \& SOLID: Código escrito priorizando a legibilidade e baixo acoplamento.



Global Exception Handling: Respostas JSON padronizadas via ApiExceptionFilter.



Dependency Injection: Configuração centralizada para facilitar a evolução e testabilidade.



Como Executar com Docker (Recomendado)

Esta é a forma mais rápida de subir todo o ecossistema (Frontend, API e Banco de Dados).



Pré-requisitos

Docker Desktop instalado.



Node.js \& Angular CLI (apenas para o primeiro build do frontend).



Passo a Passo

Build do Frontend (Angular): Navegue até a pasta WebTechsys e gere os arquivos de produção para que o Nginx possa servi-los:



Bash



npm install

ng build

Construir as Imagens: No diretório raiz (onde está o docker-compose.yml), execute:



Bash



\# Cria a imagem do dashboard

docker build -t techsys-web ./WebTechsys



\# Cria a imagem da API

docker build -t techsyslog-api ./API-TechsysLog

Subir os Containers: Execute o comando para orquestrar os serviços:



Bash



docker-compose up -d

Endereços de Acesso

Dashboard (Frontend): http://localhost:4200



Swagger (API Documentation): http://localhost:7223/swagger/index.html



MongoDB: mongodb://localhost:27017



Configuração Local (Sem Docker)

Se optar por rodar os serviços manualmente:



MongoDB: Certifique-se de que o serviço está rodando em localhost:27017.



API Connection String: Ajuste o arquivo appsettings.json na pasta da API:



JSON



"MongoDbSettings": {

  "ConnectionString": "mongodb://localhost:27017/",

  "DatabaseName": "TechsysLogDB"

}

Execução:



Bash



dotnet restore

dotnet run --project TechsysLog.Web.Api

Tecnologias e Decisões Técnicas

C# 12 / .NET 8 e MongoDB Driver.



EmailService (Simulação): Opera atualmente via Console para agilidade no desenvolvimento, permitindo troca futura via interface para AWS SES ou SendGrid.



Mapeamento Manual: Uso de DTOs para evitar a exposição de documentos internos da base de dados.



Status de Testes: A estrutura suporta testes unitários e de integração em todas as camadas, embora não implementados nesta fase inicial de entrega arquitetural.





