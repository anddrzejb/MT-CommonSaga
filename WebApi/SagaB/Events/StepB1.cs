using MassTransit;

namespace WebApi.SagaB.Events;

public sealed class StepB1 : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; init; }
}