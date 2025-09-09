using Unity.Entities;
using UnityEngine;

public class BallPrefabAuthoring : MonoBehaviour
{
    public GameObject prefab; 

    class Baker : Baker<BallPrefabAuthoring>
    {
        public override void Bake(BallPrefabAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            var prefabEntity = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic);

            AddComponent(entity, new BallPrefab { Value = prefabEntity });
        }
    }
}

public struct BallPrefab : IComponentData
{
    public Entity Value;
}
