using MassTransit;
using WebApi.SagaMiddle;

namespace WebApi.SagaA.Events;

public sealed class StepA2 :  CorrelatedBy<Guid>
{
    public StepA2(Guid correlationId)
    {
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
}