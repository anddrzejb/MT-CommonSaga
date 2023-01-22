using System.Linq.Expressions;
using MassTransit;
using WebApi.SagaA.Events;
using WebApi.SagaMiddle.Events;

namespace WebApi.SagaMiddle.Consumers;

public class StepMiddle2Consumer: IConsumer<StepMiddle2>
{
    private readonly IBus _bus;
    public StepMiddle2Consumer(IBus bus)
    {
        _bus = bus;
    }
    
    public async Task Consume(ConsumeContext<StepMiddle2> context)
    {
        Console.WriteLine("(SagaMiddle): StepMiddle2");
        
        Type evToCreate = context.Message.EventType;
        //Simpler
        //var ev = Activator.CreateInstance(evToCreate, context.Message.CorrelationId);
        
        //Faster
        var parameter = Expression.Parameter(typeof(Guid), "correlationId");
        var constructor = evToCreate.GetConstructor(new Type[] { typeof(Guid) });
        NewExpression constructorExpression = Expression.New(constructor, new Expression[] { parameter});
        Expression<Func<Guid, object>> creatorExpression = Expression.Lambda<Func<Guid, object>>(constructorExpression, parameter);
        Func<Guid, object> createEventObject = creatorExpression.Compile();  
        object ev = createEventObject(context.Message.CorrelationId);
        
        
        await _bus.Publish(ev);

        await Task.Delay(100);
    }
}