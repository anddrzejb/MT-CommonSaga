using MassTransit;

namespace WebApi.SagaB;

public class SagaBState: SagaStateMachineInstance, ISagaVersion
{
    public Guid CorrelationId { get; set; }
    public int Version { get; set; }
    public string CurrentState { get; set; } = default!;
}