using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BarrelTipAuthoring : MonoBehaviour
{
    public class Baker : Baker<BarrelTipAuthoring>
    {
        public override void Bake(BarrelTipAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<BarrelTip>(entity);
        }
    }
}

public struct BarrelTip : IComponentData
{
    
}