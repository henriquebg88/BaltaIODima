//Minimal API
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( SW => {
    //Para evitar que o Swagger considere iguais e agrupe as classes com mesmo nome, mas de namespaces difentes.
    SW.CustomSchemaIds( n => n.FullName );
});

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

app.MapGet("/", () => "Hello World!");
app.MapPost("/", () => "Hello World!");
app.MapPut("/", () => "Hello World!");
app.MapDelete("/", () => "Hello World!");

app.Run();
