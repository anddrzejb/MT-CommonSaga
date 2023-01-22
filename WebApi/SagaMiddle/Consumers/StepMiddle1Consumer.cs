using MassTransit;
using WebApi.SagaA.Events;
using WebApi.SagaMiddle.Events;

namespace WebApi.SagaMiddle.Consumers;

public class StepMiddle1Consumer: IConsumer<StepMiddle1>
{
    public async Task Consume(ConsumeContext<StepMiddle1> context)
    {
        Console.WriteLine("(SagaMiddle): StepMiddle1");
        await context.Publish(new StepMiddle2()
        {
            CorrelationId = context.Message.CorrelationId,
            EventType = context.Message.EventType
        });
        await Task.Delay(100);
    }
}