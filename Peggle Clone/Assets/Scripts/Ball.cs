using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public class Ball : MonoBehaviour
{
    class Baker : Baker<Ball>
    {
        public override void Bake(Ball authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent<BallComponent>(entity);
        }
    }
}

public struct BallComponent : IComponentData
{

}