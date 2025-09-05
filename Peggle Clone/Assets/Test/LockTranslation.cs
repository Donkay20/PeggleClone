using Unity.Entities;
using UnityEngine;
using Unity.Physics;
using Unity.Mathematics;
using Unity.Burst;

//Locks Z movement
[BurstCompile]
public class FreezeZAuthoring : MonoBehaviour
{
    public bool FreezePositionZ = true;
    public bool FreezeRotationZ = true;

    class Baker : Baker<FreezeZAuthoring>
    {
        public override void Bake(FreezeZAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new FreezeZ
            {
                Position = authoring.FreezePositionZ,
                Rotation = authoring.FreezeRotationZ
            });
        }
    }
}

[BurstCompile]
public struct FreezeZ : IComponentData
{
    public bool Position;
    public bool Rotation;
}