using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct BrickMovementSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {   
        foreach (var (transform, brick) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<PegValue>>())
        {
            if (brick.ValueRO.movementType != PegMovementType.None)
            {
                float t = (float)SystemAPI.Time.ElapsedTime;
                float3 pos = brick.ValueRO.startPos;
                float s = brick.ValueRO.speed;
                float a = brick.ValueRO.amplitude;

                switch (brick.ValueRO.movementType)
                {
                    case PegMovementType.Figure8:
                        pos.x += math.sin(t * s) * a;
                        pos.y += math.sin(t * s * 2) * a * 0.5f;
                        break;

                    case PegMovementType.MShape:
                        pos.x += math.sin(t * s) * a;
                        pos.y += math.abs(math.sin(t * s * 2)) * a;
                        break;

                    case PegMovementType.ZShape:
                        float raw = t * (s * .25f);
                        float cycle = math.abs(math.frac(raw) * 2f - 1f);
                        float x, y;

                        if (cycle < 1f / 3f)
                        {
                            float u = cycle / (1f / 3f);
                            x = math.lerp(-a, a, u);
                            y = a * 0.5f;
                        }
                        else if (cycle < 2f / 3f)
                        {
                            float u = (cycle - 1f / 3f) / (1f / 3f);
                            x = math.lerp(a, -a, u);
                            y = math.lerp(a * 0.5f, -a * 0.5f, u);
                        }
                        else
                        {
                            float u = (cycle - 2f / 3f) / (1f / 3f);
                            x = math.lerp(-a, a, u);
                            y = -a * 0.5f;
                        }

                        pos.x += x;
                        pos.y += y;
                        transform.ValueRW.Position = pos;
                        break;
                }
                transform.ValueRW.Position = pos;
            }
        }
    }
}