using MassTransit;
using WebApi.SagaB.Events;

namespace WebApi.SagaB.Consumers;

public class StepB2Consumer : IConsumer<StepB2>
{
    public async Task Consume(ConsumeContext<StepB2> context)
    {
        Console.WriteLine("(SagaB): StepB2");
        await Task.Delay(100);
    }
}