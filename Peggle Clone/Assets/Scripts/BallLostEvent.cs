using Unity.Entities;

public struct BallLostEvent : IComponentData
{
    public Entity BallEntity;
}