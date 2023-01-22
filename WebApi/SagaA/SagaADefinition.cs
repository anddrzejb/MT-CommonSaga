using MassTransit;
using WebApi.SagaA.Events;

namespace WebApi.SagaA;

public class SagaADefinition: MassTransitStateMachine<SagaAState>
{
    public SagaADefinition()
    {
        InstanceState(x => x.CurrentState);
        
        Initially(
            When(StepA1)
                .TransitionTo(StateA1));
        During(StateA1,
            When(StepA2)
                .TransitionTo(StateA2)
                .Then(ctx =>
                {
                    Console.WriteLine($"(SagaA): Transitioned to {ctx.Saga.CurrentState} and finalizing.");
                })
                .Finalize());
    }
    
    public State StateA1 { get; private set; }
    public State StateA2 { get; private set; }
    
    public Event<StepA1> StepA1 { get; private set; }
    public Event<StepA2> StepA2 { get; private set; }
}