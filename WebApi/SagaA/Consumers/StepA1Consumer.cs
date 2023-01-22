using MassTransit;
using WebApi.SagaA.Events;
using WebApi.SagaMiddle.Events;

namespace WebApi.SagaA.Consumers;

public class StepA1Consumer : IConsumer<StepA1>
{
    public async Task Consume(ConsumeContext<StepA1> context)
    {
        Console.WriteLine("(SagaA): StepA1");
        await context.Publish(new StepMiddle1()
        {
            CorrelationId = context.Message.CorrelationId,
            CallingSagaName = "SagaA",
            EventType = typeof(StepA2)
        });
        await Task.Delay(100);
    }
}