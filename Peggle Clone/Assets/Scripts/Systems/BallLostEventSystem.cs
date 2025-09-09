using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct BallLostEventSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var manager = GameObject.FindObjectOfType<GameManager>();
        if (manager == null) return;

        // Build the query manually
        var query = SystemAPI.QueryBuilder()
            .WithAll<BallLostEvent>()
            .Build();

        var entities = query.ToEntityArray(Allocator.Temp);

        foreach (var entity in entities)
        {
            manager.BallFell();
            state.EntityManager.DestroyEntity(entity);
        }
        entities.Dispose();
    }
}