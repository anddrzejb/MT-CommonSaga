using MassTransit;
using WebApi.SagaB.Events;
using WebApi.SagaMiddle.Events;

namespace WebApi.SagaB.Consumers;

public class StepB1Consumer : IConsumer<StepB1>
{
    public async Task Consume(ConsumeContext<StepB1> context)
    {
        Console.WriteLine("(SagaB): StepB1");
        await context.Publish(new StepMiddle1()
        {
            CorrelationId = context.Message.CorrelationId,
            CallingSagaName = "SagaB",
            EventType = typeof(StepB2)
        });
        await Task.Delay(100);
    }
}