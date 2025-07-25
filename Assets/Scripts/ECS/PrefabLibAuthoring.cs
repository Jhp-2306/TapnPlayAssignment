using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PrefabLibAuthoring : MonoBehaviour
{
    public GameObject prefab;

    public class Baker : Baker<PrefabLibAuthoring>
    {
        public override void Bake(PrefabLibAuthoring authoring)
        {
            Entity mainEntity = GetEntity(TransformUsageFlags.None);
            Entity prefabEntity = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic);

            AddComponent(mainEntity, new PrefabLibReference
            {
                prefabEntity = prefabEntity
            });
        }
    }

}
public struct PrefabLibReference : IComponentData
{
    public Entity prefabEntity;
}
