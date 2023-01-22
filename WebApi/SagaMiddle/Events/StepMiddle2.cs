using MassTransit;

namespace WebApi.SagaMiddle.Events;

public class StepMiddle2: CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; init; }
    public Type EventType { get; set; }
}