using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;

[UpdateInGroup(typeof(PhysicsSystemGroup))]
[UpdateAfter(typeof(PhysicsSimulationGroup))]
public partial struct BallDestroySystem : ISystem
{
    private ComponentLookup<BallDestroyZone> destroyLookup;
    private ComponentLookup<BallComponent> ballLookup;

    public void OnCreate(ref SystemState state)
    {
        destroyLookup = state.GetComponentLookup<BallDestroyZone>(true);
        ballLookup = state.GetComponentLookup<BallComponent>(true);
    }
    
    public void OnUpdate(ref SystemState state)
    {
        state.Dependency.Complete();

        var sim = SystemAPI.GetSingleton<SimulationSingleton>().AsSimulation();
        var triggers = sim.TriggerEvents;

        destroyLookup.Update(ref state);
        ballLookup.Update(ref state);

        var ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach (var te in triggers)
        {
            var a = te.EntityA;
            var b = te.EntityB;

            bool aIsZone = destroyLookup.HasComponent(a);
            bool bIsZone = destroyLookup.HasComponent(b);

            if (aIsZone && ballLookup.HasComponent(b))
            {
                var evt = ecb.CreateEntity();
                ecb.AddComponent(evt, new BallLostEvent { BallEntity = b });
            }
            else if (bIsZone && ballLookup.HasComponent(a))
            {
                var evt = ecb.CreateEntity();
                ecb.AddComponent(evt, new BallLostEvent { BallEntity = a });
            }
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
