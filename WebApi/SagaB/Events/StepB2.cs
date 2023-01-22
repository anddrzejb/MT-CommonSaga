using MassTransit;

namespace WebApi.SagaB.Events;

public sealed class StepB2 : CorrelatedBy<Guid>
{
    public StepB2(Guid correlationId)
    {
        this.CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
}