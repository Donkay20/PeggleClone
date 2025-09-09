using Unity.Entities;
using UnityEngine;

public class PegHitEventListener : MonoBehaviour
{
    private EntityManager manager;
    private EntityQuery query;
    public GameManager gameManager;

    void Start()
    {
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        query = manager.CreateEntityQuery(typeof(PegHitEvent));
    }

    void LateUpdate()
    {
        using var events = query.ToEntityArray(Unity.Collections.Allocator.Temp);
        foreach (var evtEntity in events)
        {
            gameManager.PegHit();
            manager.DestroyEntity(evtEntity);
        }
    }
}
