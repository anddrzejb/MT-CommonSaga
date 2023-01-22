using MassTransit;
using WebApi.SagaA.Events;

namespace WebApi.SagaA.Consumers;

public class StepA2Consumer : IConsumer<StepA2>
{
    public async Task Consume(ConsumeContext<StepA2> context)
    {
        Console.WriteLine("(SagaA): StepA2");
        await Task.Delay(100);
    }
}