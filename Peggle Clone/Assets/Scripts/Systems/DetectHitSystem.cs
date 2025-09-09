using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

[BurstCompile]
[UpdateInGroup(typeof(PhysicsSystemGroup))]
[UpdateAfter(typeof(PhysicsSimulationGroup))]
public partial struct DetectHitSystem : ISystem
{
    private ComponentLookup<BallComponent> ballLookup;
    private ComponentLookup<PegTag> pegLookup;

    public void OnCreate(ref SystemState state)
    {
        ballLookup = state.GetComponentLookup<BallComponent>(true);
        pegLookup = state.GetComponentLookup<PegTag>(true);
    }

    public void OnUpdate(ref SystemState state)
    {
        ballLookup.Update(ref state);
        pegLookup.Update(ref state);

        var sim = SystemAPI.GetSingleton<SimulationSingleton>().AsSimulation();
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach (var ce in sim.CollisionEvents)
        {
            var a = ce.EntityA;
            var b = ce.EntityB;

            bool aIsBall = ballLookup.HasComponent(a);
            bool bIsBall = ballLookup.HasComponent(b);

            bool aIsPeg = pegLookup.HasComponent(a);
            bool bIsPeg = pegLookup.HasComponent(b);

            if (aIsBall && bIsPeg)
            {
                var evt = ecb.CreateEntity();
                ecb.AddComponent(evt, new PegHitEvent());
                ecb.DestroyEntity(b); 
            }
            else if (bIsBall && aIsPeg)
            {
                var evt = ecb.CreateEntity();
                ecb.AddComponent(evt, new PegHitEvent());
                ecb.DestroyEntity(a); 
            }
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
