using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Peg : MonoBehaviour
{
    public int scoreValue;

    private class Baker : Baker<Peg>
    {
        public override void Bake(Peg authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PegValue
            {
                scoreValue = authoring.scoreValue
            });
        }
    }
}

public struct PegValue : IComponentData
{
    public int scoreValue;
}