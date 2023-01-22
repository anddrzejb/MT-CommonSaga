using MassTransit;

namespace WebApi.SagaA.Events;

public sealed class StepA1 : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; init; }
}