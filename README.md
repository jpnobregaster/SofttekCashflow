<h1 align="center">SofttekCashflow</h1>

<p align="center">
  <i>Aplicação de fluxo de caixa para teste na empresa <b>Softtek<n> .
  <br>
</p>

<h2>Sumário</h2>
<ul>
    <li><a href="#ins">Instalação</a></li>
    <li><a href="#arq">Arquitetura</a></li>
    <li><a href="#fer">Ferramentas</a> 
</ul>
<h2 id="ins">Instalação</h2>
<p>Dependências: <a href="https://dotnet.microsoft.com/en-us/download/dotnet/7.0">.Net 7.0.5</a>, <a href="https://nodejs.org/dist/v18.16.0/">Node 18.16.0</a> e <a href="https://www.docker.com/products/docker-desktop/">Docker</a>.</p>
<p>Abra um console na raiz da aplicação, para executarmos o <b>docker-composer.yml</b>. No console execute:

    docker-compose build
O comando acima, irá criar duas imagens. Em seguida, executamos as imagens com o comando:

    docker-compose up -d
Após isso, será instalado dois containers: <b>softtekcashflow-frontend</b> e <b>softtekcashflow-backend</b> que já serão iniciados automaticamente.

Cada aplicação, frontend e backend é iniciada em uma porta pré configurada:
frontend na porta http://localhost:4200 e backend na porta: http://localhost:5000 da sua máquina local.
Essas portas podem ser alteradas no arquivo <b>docker-composer.yml</b>.

O banco de dados roda em memória, não sendo necessário nenhum tipo de configuração.
Para acessar o swagger da api, utilize o link:  http://localhost:5000/index.html.

As depêndencias de componentes, tanto do frontend quanto do backend são instaladas automáticamente durante a execução dos scripts docker.
</p>

<h2 id="arq">Arquitetura</h2>
As aplicações seguem design patterns como DDD, Solid, Clean Code, Repository e IoC, MVC, ViewModels.
O backend é dividido em: api, domin, application, infra e IoC. As requisições são baseadas em commands e queries utilizado um conceito do pattern CQRC. O frontend está estruturado em camadas, seguindo principios de Solid, Clean Code e um guia de estilo fornecida pelo Angular.
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

