using MassTransit;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using WebApi.SagaA.Events;
using WebApi.SagaMiddle.Events;

namespace WebApi.SagaMiddle;

public class SagaMiddleDefinition: MassTransitStateMachine<SagaMiddleState>
{
    public SagaMiddleDefinition()
    {
        InstanceState(x => x.CurrentState);
        
        Initially(
            When(StepMiddle1)
                .Then(context =>
                {
                    context.Saga.EventType = context.Message.EventType;
                    context.Saga.CallingSagaName = context.Message.CallingSagaName;
                })
                .TransitionTo(StateMiddle1));
        
        During(StateMiddle1,
            When(StepMiddle2)
                .TransitionTo(StateMiddle2)
                .Then(ctx =>
                {
                    Console.WriteLine($"(SagaMiddle): Transitioned to {ctx.Saga.CurrentState} and finalizing. Going back to {ctx.Saga.CallingSagaName}");
                })
                // .Publish(context => context.Saga.EventToPublish
                //     // {
                //     //     Type evToCreate = context.Saga.EventType;
                //     //     var ev = Activator.CreateInstance(evToCreate, context.Saga.CorrelationId);
                //     //     return ev;
                //     // } 
                //         
                //     //((StepA2)context.Saga.FollowUpEvent) //not working
                //     //     new StepA2
                //     // {
                //     //     CorrelationId = context.Saga.CorrelationId 
                //     // }                        
                //     //context.Saga.FollowUpEvent as CorrelatedBy<Guid> //not working
                // )
                .Finalize());
    }
    
    public State StateMiddle1 { get; private set; }
    public State StateMiddle2 { get; private set; }
    
    public Event<StepMiddle1> StepMiddle1 { get; private set; }
    public Event<StepMiddle2> StepMiddle2 { get; private set; }
}