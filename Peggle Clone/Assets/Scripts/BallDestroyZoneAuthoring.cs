using Unity.Entities;
using UnityEngine;

public class BallDestroyZoneAuthoring : MonoBehaviour
{
    class Baker : Baker<BallDestroyZoneAuthoring>
    {
        public override void Bake(BallDestroyZoneAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent<BallDestroyZone>(entity);
        }
    }
}

public struct BallDestroyZone : IComponentData
{
    
}
