using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PegTagAuthoring : MonoBehaviour
{
    public class Baker : Baker<PegTagAuthoring>
    {
        public override void Bake(PegTagAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PegTag>(entity);
        }
    }
}

public struct PegTag : IComponentData
{
    
}