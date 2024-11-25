//Minimal API
using Dima.API.Data;
using Dima.API.Endpoints;
using Dima.API.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default") ?? "";

builder.Services.AddDbContext<AppDbContext>( x => {
    x.UseSqlServer(connectionString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( SW => {
    //Para evitar que o Swagger considere iguais e agrupe as classes com mesmo nome, mas de namespaces difentes.
    SW.CustomSchemaIds( n => n.FullName );
});

#region Explicação AddTransient/Singleton/Scoped
    /**
    AddTransient<Handler>():
        Isso registra o tipo Handler no contêiner de injeção de dependência com o tempo de vida transitório. 
        Ou seja, toda vez que o ASP.NET Core precisar de uma instância do tipo Handler, ele criará uma nova instância dessa classe.
        A instância de Handler será criada e destruída a cada vez que for solicitada. 
        Por exemplo, se o Handler for injetado em uma requisição HTTP, uma nova instância será criada para cada requisição.
        -Fluxo de vida:
            Se você pedir o serviço duas vezes (em momentos diferentes ou no mesmo contexto), o ASP.NET Core criará duas instâncias distintas do serviço.

    AddSingleton<Handler>(): 
        A instância do serviço é criada uma única vez e compartilhada em toda a vida da aplicação.

    AddScoped<Handler>(): 
        A instância do serviço é criada uma vez por requisição (escopo de requisição). 
        Ou seja, a mesma instância é reutilizada durante o processamento de uma única requisição HTTP, mas uma nova instância será criada para cada nova requisição.
        -Fluxo de vida:
            Se você pedir o serviço dentro do mesmo contexto de requisição (por exemplo, dentro do mesmo controlador ou serviço), o ASP.NET Core vai retornar a mesma instância do serviço.
            Se a requisição acabar (por exemplo, a resposta HTTP for enviada ao cliente), a instância do serviço será descartada.
    **/
#endregion

//Injeção de Dependência:
//Registrar um tipo de serviço (no caso, o handler) no container de dependências (para injeção de dependência), 
//com tempo de vida transitório (Ou seja, toda vez que o ASP.NET Core precisar de uma instância do tipo Handler, ele criará uma nova instância dessa classe.)
builder.Services.AddTransient<ICaterogyHandler, CategoryHandler>(); // O Aspnet consegue inferir que o Request vem do Body, mas não sabe de onde vem o Handler

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

#region Anotações
//Requisição (REQUEST) -> Cabeçalho e Corpo | Request => Header e Body
//CRUD -> POST, PUT, DELETE -> Normalmente tão do tipo JSON | GET não tem corpo (body)
//Binding -> Transformar JSON para Objeto C#

//Resposta (RESPONSE) - resposta da requisição
//Cabeçalho e Corpo
//Status code - 400, 401, 403, 404, 200, 201, 500

//Request: vai fazer um get para URL "/algo/x"
//Respose: "Heello world"
//app.MapGet("/algo/", () => "Hello World!");

//Endpoints -> URLs para acesso
// "/" -> http://localhost:0000/
// "/livro" -> http://localhost:0000/livros

//Convenções de mercado
// Get, Post, Put e Delete todos pra mesma rota. ex: http://localhost:0000/livros
// Objeto no plural (livroS)

// Versão
// A API pode ser consumida por diversos Fronts
// Ex: Atualizou um parametro obrigatorio de uma rota. Isso pode quebrar o sistema de quem consume a API.
// Endpoints funcionam também como contrato. Quebrou o endpoint -> quebrou o contrato.
// Antes não precisava do parametro, agora precisa
// "/v1/produtos"
// "/v2/produtos"
// 1.0.0 -> 1.0.1 -> 1.1.0
// 1.3.0 -> 2.0.0 (Breaking changes) Quebra o contrato

// Por rota: Request | Response | Handler

// app.MapPost("/produtos", (Produto produto) => new { message: "ok"}); Utilizando o objeto anônimo, será retornado um JSON
// Este JSON não será reconhecido pelo Swagger para documentar a API.
// app.MapPost("/produtos", (Produto produto) => new Response { message: "ok"}); Será utlizado pelo Swagger
// app.MapPost("/produtos", (Produto produto) => new Response { message: "ok"})
//                                                              .WithName("Produtos: Create")
//                                                              .WithSummary("Cria um novo produto")
//                                                              .Produces<Response>(); (classe Response, ou qualquer outra)
// estes métodos no final são para melhorar a documentação da API no Swagger
#endregion

app.MapGet("/", () => new {message = "Estou vivo."});

//Método de extensão criado para colocar os mapeamentos
app.MapEndpoints();

app.Run();