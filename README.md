<h1>Dima - BaltaIO Blazor</h1>
<h2>Dima.sln</h2>
Solução agrupadora de projetos

<h2>Dima.Core</h2>

Projeto compartilhado entre os projetos da solution
Não deixar informação sensível pois será compartilhado com o projeto dr Frontend, além do Backend

Criar a Biblioteca:<br>
<code>dotnet new classlib -o Dima.Core</code>

Colocar na solução<br>
<code>dotnet sln add ./Dima.Core</code>

<h2>Dima.API</h2>

Criar a API:<br>
<code>dotnet new web -o Dima.API</code>

Colocar na solução:<br>
<code>dotnet sln add ./Dima.API</code>

Pacote para criar a documentação da API:<br>
<code>dotnet add package Microsoft.AspNetCore.OpenApi</code><br>
<code>dotnet add package Swashbuckle.AspNetCore</code>

<code>dotnet run</code> ou botão de Run

Api rodando (por padrão) em <code>http://localhost:5139</code><br>
Swagger: <code>http://localhost:5139/swagger</code>

<hr>

<h4>Anotações</h4>
<ul>
  <li>
    Os projetos de Frontend, Blazor WASM, que roda no lado do Cliente e de Backend, a API, que roda no Servidor, ambos vão referenciar a Class Library (Dima.Core). 
    Se for referenciado algo como Entity Framework na Biblioteca Compartilhada, o Entity será referenciado em ambos os projetos. Ou seja, no projeto de Frontend também.
    Este projeto de Biblioteca deve ser o mais limpo possível.
  </li>
  <li>
    Se a arquitetura do projeto fará uso de Entity Framework em mais de um projeto na Solução, como a API e um Blazor Server, pode ser criada uma outra Class Library que faz referência ao Entity Framework, para ser usado por ambos os projetos.
    Isso evita com que o projeto Blazor Server tenha que fazer uma requisição HTTP para o projeto da API para fazer uma consulta ao banco. Ele faria diretamente pela Biblioteca Compartilhada.
  </li>
</ul>
