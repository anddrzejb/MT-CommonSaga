using System.Linq.Expressions;
using MassTransit;
using WebApi.SagaMiddle.Events;

namespace WebApi.SagaMiddle;

public class SagaRedirectActivity: IStateMachineActivity<SagaMiddleState, StepMiddle2>
{
    public void Probe(ProbeContext context)
    {
    }

    public void Accept(StateMachineVisitor visitor)
    {
        visitor.Visit(this);
    }

    public async Task Execute(BehaviorContext<SagaMiddleState, StepMiddle2> context, IBehavior<SagaMiddleState, StepMiddle2> next)
    {
        Console.WriteLine("(SagaMiddle): SagaRedirectActivity.Execute");
        
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

        await context.Publish(ev);
        await next.Execute(context); 
    }

    public Task Faulted<TException>(BehaviorExceptionContext<SagaMiddleState, StepMiddle2, TException> context, IBehavior<SagaMiddleState, StepMiddle2> next) where TException : Exception
    {
        return next.Faulted(context);
    }
}