using MassTransit;
using WebApi.SagaA;
using WebApi.SagaA.Consumers;
using WebApi.SagaA.Events;
using WebApi.SagaB;
using WebApi.SagaB.Consumers;
using WebApi.SagaB.Events;
using WebApi.SagaMiddle;
using WebApi.SagaMiddle.Consumers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(cfg =>
{
    cfg.AddSagaStateMachine<SagaADefinition, SagaAState>().InMemoryRepository();
    cfg.AddConsumersFromNamespaceContaining<StepA1Consumer>();
    
    cfg.AddSagaStateMachine<SagaBDefinition, SagaBState>().InMemoryRepository();
    cfg.AddConsumersFromNamespaceContaining<StepB1Consumer>();    
    
    cfg.AddSagaStateMachine<SagaMiddleDefinition, SagaMiddleState>().InMemoryRepository();
    cfg.AddConsumersFromNamespaceContaining<StepMiddle2Consumer>();
    
    
    cfg.UsingInMemory((context, opt) =>
    {
        opt.UseNewtonsoftJsonSerializer();
        opt.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseSwagger();

app.MapGet("/", () => "Hello World!");
app.MapPost("/saga/a", async (IPublishEndpoint publishEndpoint) =>
{
    publishEndpoint.Publish(new StepA1()
    {
        CorrelationId = Guid.NewGuid()
    });
    return Results.Ok();
});

app.MapPost("/saga/b", async (IPublishEndpoint publishEndpoint) =>
{
    publishEndpoint.Publish(new StepB1()
    {
        CorrelationId = Guid.NewGuid()
    });
    return Results.Ok();
});


app.UseSwaggerUI();
app.Run();