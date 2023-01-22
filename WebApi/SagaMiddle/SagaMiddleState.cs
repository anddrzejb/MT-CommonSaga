using MassTransit;

namespace WebApi.SagaMiddle;

public class SagaMiddleState: SagaStateMachineInstance, ISagaVersion
{
    public Guid CorrelationId { get; set; }
    public int Version { get; set; }
    public string CurrentState { get; set; } = default!;
    public Type EventType { get; set; }
    public string CallingSagaName { get; set; }
}