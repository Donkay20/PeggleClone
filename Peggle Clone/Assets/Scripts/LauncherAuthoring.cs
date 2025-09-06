using Unity.Entities;
using UnityEngine;

public class LauncherAuthoring : MonoBehaviour
{
    public float MinAngle = -65f;
    public float MaxAngle = 65f;

    class Baker : Baker<LauncherAuthoring>
    {
        public override void Bake(LauncherAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Launcher
            {
                MinAngle = authoring.MinAngle,
                MaxAngle = authoring.MaxAngle
            });
        }
    }
}

public struct Launcher : IComponentData
{
    public float MinAngle;
    public float MaxAngle;
}