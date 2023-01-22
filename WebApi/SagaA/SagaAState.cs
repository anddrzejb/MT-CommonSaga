using MassTransit;

namespace WebApi.SagaA;

public class SagaAState: SagaStateMachineInstance, ISagaVersion
{
    public Guid CorrelationId { get; set; }
    public int Version { get; set; }
    public string CurrentState { get; set; } = default!;
}