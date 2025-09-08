using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;

[BurstCompile]
public partial struct BallSpawnerSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            
            foreach (var (ltw, tipEntity) in SystemAPI.Query<RefRO<LocalToWorld>>().WithEntityAccess().WithAll<BarrelTip>())
            {
                var ballPrefab = SystemAPI.GetSingleton<BallPrefab>().Value;
                var ball = ecb.Instantiate(ballPrefab);

                ecb.SetComponent(ball, new LocalTransform
                {
                    Position = ltw.ValueRO.Position,
                    Rotation = quaternion.LookRotationSafe(ltw.ValueRO.Forward, ltw.ValueRO.Up),
                    Scale = 1f
                });

                float3 launchDir = -ltw.ValueRO.Up;
                UnityEngine.Debug.Log("LaunchDir: " + launchDir);
                ecb.SetComponent(ball, new PhysicsVelocity
                {
                    Linear = launchDir * 40f,
                    Angular = float3.zero
                });
            }
        }
        ecb.Playback(state.EntityManager);
    }
}
