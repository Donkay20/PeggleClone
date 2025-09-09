using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class Peg : MonoBehaviour
{
    public int scoreValue;
    public PegMovementType movementType;
    public float speed;
    public float amplitude;
    public float3 startPos;

    private class Baker : Baker<Peg>
    {
        public override void Bake(Peg authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PegValue
            {
                scoreValue   = authoring.scoreValue,
                movementType = authoring.movementType,
                speed        = authoring.speed,
                amplitude    = authoring.amplitude,
                startPos     = authoring.transform.position
            });
        }
    }
}

public struct PegValue : IComponentData
{
    public int scoreValue;
    public PegMovementType movementType;
    public float speed;
    public float amplitude;
    public float3 startPos;
}