<h1>Dima - BaltaIO Blazor</h1>
<h4>Dima.sln</h4>
Solução agrupadora de projetos

<h4>Dima.Core</h4>
Projeto compartilhado entrs os projetos da solution
Não deixar informação sensível pois será compartilhado com o projeto dr Frontend, além do Backend
[dotnet new classlib -o Dima.Core]
[dotnet sln add ./Dima.Core]

<h5>Anotações</h5>
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
