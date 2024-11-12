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

API -> Banco (Code First)
<ol>
  <li>Instalar o EntityFramework para o banco que será usado</li>
  <li>Instalar o Designer do EntityFramework</li>
  <li>Criar as Models</li>
  <li>Criar o contexto do banco de dados, herdando a interface DBContext do Entity</li>
  <li>Fazer o mapeamento das classes, dentro do contexto criado</li>
  <li>No Program.cs acrescentar o uso do DbContext e passar a connection string</li>
  <li>Antes, é preciso ter o SQL Server instalado. Pode ser feito usando o Docker, para não instalar localmente, e usar imagens prontas: https://blog.balta.io/docker-instalacao-configuracao-e-primeiros-passos/</li>
  <li>Imagem do SQL Server: https://balta.io/blog/sql-server-docker</li>
  <li>Foi usado o Azure Data Studio para conectar com o banco e executar queries diretamente</li>
  <li>Para remover a Connection String do arquivo AppSettings.json:
    <ul>
      <li><code>dotnet user-secrets init</code>, estando no projeto da API.</li>
      <li><code>dotnet user-secrets set "ConnectionStrings:Default" "*** Conection string do banco ***"</code> O caminho ConnectionStrings:Default é de acordo com o AppSettings.json, e este deve existir, tendo uma string vazia.</li>
    </ul>
  </li>
  <li>Fazer as Migrations:
    <ul>
      <li>
        <code>dotnet tool install --global dotnet-ef</code> no projeto da API
      </li>
      <li>
        <code>dotnet ef migrations add initial</code>
      </li>
      <li>
        <code>dotnet ef database update</code> Para atualizar o banco de dados de acordo com as migrations
      </li>
    </ul>
  </li>
</ol>

<hr>

<h4>Anotações</h4>
<ul>
  <li>Código fonte Balta IO: https://github.com/balta-io/3054</li>
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
