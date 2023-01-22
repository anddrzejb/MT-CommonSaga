using MassTransit;
using WebApi.SagaB.Events;

namespace WebApi.SagaB;

public class SagaBDefinition: MassTransitStateMachine<SagaBState>
{
    public SagaBDefinition()
    {
        InstanceState(x => x.CurrentState);
        
        Initially(
            When(StepB1)
                .TransitionTo(StateB1));
        During(StateB1,
            When(StepB2)
                .TransitionTo(StateB2)
                .Then(ctx =>
                {
                    Console.WriteLine($"(SagaB): Transitioned to {ctx.Saga.CurrentState} and finalizing.");
                })
                .Finalize());
    }
    
    public State StateB1 { get; private set; }
    public State StateB2 { get; private set; }
    
    public Event<StepB1> StepB1 { get; private set; }
    public Event<StepB2> StepB2 { get; private set; }
}