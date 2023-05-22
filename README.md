<h1 align="center">SofttekCashflow</h1>

<p align="center">
	Aplicação de fluxo de caixa para teste na empresa <b>Softtek</b> .
  <br>
</p>

<h2>Sumário</h2>
<ul>
    <li><a href="#ins">Instalação e Configuração</a></li>
    <li><a href="#arq">Arquitetura</a></li>
    <li><a href="#fer">Ferramentas</a></li>
</ul>
<h2 id="ins">Instalação e Configuração</h2>
<p>Dependências: <a href="https://dotnet.microsoft.com/en-us/download/dotnet/7.0">.Net 7.0.5</a>, <a href="https://nodejs.org/dist/v18.16.0/">Node 18.16.0</a> e <a href="https://www.docker.com/products/docker-desktop/">Docker</a>.</p>

Clone

    git clone https://github.com/jpnobregaster/SofttekCashflow.git

<p>Abra um console na raiz da aplicação, para executarmos o <b>docker-composer.yml</b>. No console execute:

    docker-compose build
O comando acima, irá criar duas imagens. Em seguida, executamos as imagens com o comando:

    docker-compose up -d
Após isso, será instalado dois containers: <b>softtekcashflow-frontend</b> e <b>softtekcashflow-backend</b> que já serão iniciados automaticamente.

Cada aplicação, frontend e backend é iniciada em uma porta pré configurada:
frontend na porta http://localhost:4200 e backend na porta: http://localhost:5000 da sua máquina local.
Essas portas podem ser alteradas no arquivo <b>docker-composer.yml</b>.

O banco de dados roda em memória, não sendo necessário nenhum tipo de configuração.
Para acessar o swagger da api, utilize o link:  http://localhost:5000/swagger/index.html.

As depêndencias de componentes, tanto do frontend quanto do backend são instaladas automáticamente durante a execução dos scripts docker.

A Consolidação foi configurada para rodar a cada 1min, para uma melhor visualização da tarefa agendada, e pode ser alterado facilmente no arquivo <b>appsettings.json</b>, na raiz da API.

    "SchaduledJob": {
       "JobKey": "ConsolidationJobKey",
       "TriggerKey": "ConsolidationJobTriggerKey",
       "Cron": "0 0/1 * * * ?"
    }
</p>

<h2 id="arq">Arquitetura</h2>
As aplicações seguem design patterns como DDD, Solid, Clean Code, Repository e IoC, MVC, ViewModels.
O backend é dividido em: api, domin, application, infra e IoC. As requisições são baseadas em commands e queries utilizado um conceito do pattern CQRC. O frontend está estruturado em camadas, seguindo principios de Solid, Clean Code e um guia de estilo fornecida pelo Angular.

**Frontend**
```
├── angular.json                             
├── Dockerfile
├── package.json
├── src
|  ├── app
|  |  ├── main
|  |  |  ├── app.module.ts
|  |  |  ├── app.routes.ts
|  |  |  └── apresentation
|  |  |     ├── app.component.html
|  |  |     ├── app.component.scss
|  |  |     └── app.component.ts
|  |  ├── operation
|  |  |  ├── domain
|  |  |  |  └── cashflow
|  |  |  |     ├── dto
|  |  |  |     |  └── cashflow-daily-balance.dto.ts
|  |  |  |     ├── model
|  |  |  |     |  └── cashflow.model.ts
|  |  |  |     └── service
|  |  |  |        └── cashflow.service.contract.ts
|  |  |  ├── index.ts
|  |  |  ├── infrastruture
|  |  |  |  └── service
|  |  |  |     └── cashflow.service.imp.ts
|  |  |  ├── operation.module.ts
|  |  |  ├── operation.routes.ts
|  |  |  └── presentation
|  |  |     ├── entry
|  |  |     |  ├── entry.component.html
|  |  |     |  └── entry.component.ts
|  |  |     ├── home
|  |  |     |  ├── home.component.html
|  |  |     |  └── home.component.ts
|  |  |     └── report
|  |  |        ├── report.component.html
|  |  |        └── report.component.ts
|  |  └── shared
|  |     ├── domain
|  |     |  ├── error
|  |     |  |  └── error.handler.contract.ts
|  |     |  ├── model
|  |     |  |  ├── domain.entity.builder.model.ts
|  |     |  |  └── domain.entity.model.ts
|  |     |  └── service
|  |     |     ├── alert.service.contract.ts
|  |     |     └── service.base.contract.ts
|  |     ├── index.ts
|  |     ├── infrastructure
|  |     |  ├── error
|  |     |  |  └── error.handler.imp.ts
|  |     |  └── service
|  |     |     ├── alert.service.imp.ts
|  |     |     └── service.base.imp.ts
|  |     ├── presentation
|  |     |  ├── footer
|  |     |  |  ├── footer.component.html
|  |     |  |  ├── footer.component.scss
|  |     |  |  └── footer.component.ts
|  |     |  └── header
|  |     |     ├── header.component.html
|  |     |     ├── header.component.scss
|  |     |     └── header.component.ts
|  |     ├── shared.module.ts
|  |     └── shared.routes.ts
|  ├── assets
|  ├── environments
|  |  ├── environment.development.ts
|  |  └── environment.ts
|  ├── favicon.ico
|  ├── index.html
|  ├── main.ts
|  └── styles.css
├── tsconfig.app.json
├── tsconfig.json
└── tsconfig.spec.json
```
**Backend**
```
├── Dockerfile
├── Softtek.Cashflow.Api
|  ├── appsettings.Development.json
|  ├── appsettings.json
|  ├── Controllers
|  |  └── CashflowController.cs
|  ├── Program.cs
|  ├── Properties
|  |  └── launchSettings.json
├── Softtek.Cashflow.Application
|  ├── Entry
|  |  ├── Command
|  |  |  ├── CashflowCommand.cs
|  |  |  └── CashflowHandler.cs
|  |  ├── Mapper
|  |  |  └── MappingProfile.cs
|  |  └── Query
|  |     ├── ConsolidatedBalanceHandler.cs
|  |     └── ConsolidatedBalanceQuery.cs
|  └── Softtek.Cashflow.Application.csproj
├── Softtek.Cashflow.Domain
|  ├── Common
|  |  ├── Entities
|  |  |  └── Entity.cs
|  |  ├── Repository
|  |  |  └── IBaseRepository.cs
|  |  └── ViewModel
|  |     ├── UnknowErrorViewModel.cs
|  |     └── ValidateErrorViewModel.cs
|  ├── Entities
|  |  └── Cashflow
|  |     ├── Model
|  |     |  └── CashflowModel.cs
|  |     └── Repository
|  |        └── ICashflowRepository.cs
|  ├── Jobs
|  |  └── IConsolidationSchaduleJob.cs
|  ├── Softtek.Cashflow.Domain.csproj
|  └── ViewModel
|     └── Cashflow
|        └── ConsolidatedBalanceViewModel.cs
├── Softtek.Cashflow.Infra
|  ├── Common
|  |  └── Repository
|  |     └── BaseRepository.cs
|  ├── Jobs
|  |  └── ConsolidationSchaduleJob.cs
|  ├── Persistence
|  |  └── Context
|  |     └── SqlServer
|  |        ├── Configuration
|  |        |  └── CashflowConfiguration.cs
|  |        └── Context
|  |           └── CashflowSqlServerContext.cs
|  ├── Repositories
|  |  └── Cashflow
|  |     └── CashflowRepository.cs
|  └── Softtek.Cashflow.Infra.csproj
├── Softtek.Cashflow.Infra.IoC
|  ├── DependencyInjection
|  |  └── DependencyInjectionIoC.cs
|  └── Softtek.Cashflow.Infra.IoC.csproj
├── Softtek.Cashflow.Tests
|  ├── Controllers
|  |  └── CashflowControllerTest.cs
|  └── Softtek.Cashflow.Tests.csproj
```
<h2 id="fer">Ferramentas</h2>

 - .Net/Core 7
 - Angular 16
 - Entity Framework In Memory
 - Automapper
 - Fluent Validator
 - Quartz
 - MediatoR
 - Moq
 - NUnit
 - Swagger