using MassTransit;

namespace WebApi.SagaMiddle.Events;

public class StepMiddle1: CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; init; }
    
    public string CallingSagaName { get; set; }
    
    public Type EventType { get; set; }
}