using System.Linq.Expressions;
using MassTransit;
using WebApi.SagaA.Events;
using WebApi.SagaMiddle.Events;

namespace WebApi.SagaMiddle.Consumers;

public class StepMiddle2Consumer: IConsumer<StepMiddle2>
{
    public async Task Consume(ConsumeContext<StepMiddle2> context)
    {
        Console.WriteLine("(SagaMiddle): StepMiddle2");

        await Task.Delay(100);
    }
}