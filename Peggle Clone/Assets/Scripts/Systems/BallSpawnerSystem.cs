using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;

[BurstCompile]
[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct BallSpawnerSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
        var prefab = SystemAPI.GetSingleton<BallPrefab>().Value;

        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            foreach (var (entity, LaunchBallEvent) in SystemAPI.Query<RefRO<LaunchBallEvent>>().WithEntityAccess())
            {
                foreach (var (ltw, tipEntity) in SystemAPI.Query<RefRO<LocalToWorld>>().WithEntityAccess().WithAll<BarrelTip>())
                {
                    var ball = ecb.Instantiate(prefab);
                    ecb.SetComponent(ball, new LocalTransform
                    {
                        Position = ltw.ValueRO.Position,
                        Rotation = quaternion.LookRotationSafe(ltw.ValueRO.Forward, ltw.ValueRO.Up),
                        Scale = 1f
                    });

                    float3 launchDir = -ltw.ValueRO.Up;
                    ecb.SetComponent(ball, new PhysicsVelocity
                    {
                        Linear = launchDir * 40f,
                        Angular = float3.zero
                    });
                }
                ecb.DestroyEntity(LaunchBallEvent);
            }
        }
        ecb.Playback(state.EntityManager);
    }
}