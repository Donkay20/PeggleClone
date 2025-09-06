using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct LauncherRotationSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var cam = Camera.main;
        if (cam == null) return;

        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMouse = cam.ScreenToWorldPoint(mousePos);
        worldMouse.z = 0f;

        foreach (var (transform, launcher) in
                 SystemAPI.Query<RefRW<LocalTransform>, RefRO<Launcher>>())
        {
            float3 pos = transform.ValueRO.Position;
            float3 dir = worldMouse - (Vector3)pos;

            float angle = math.degrees(math.atan2(dir.y, dir.x)) + 90f;

            angle = math.clamp(angle, launcher.ValueRO.MinAngle, launcher.ValueRO.MaxAngle);

            transform.ValueRW.Rotation = quaternion.EulerXYZ(0f, 0f, math.radians(angle));
        }
    }
}