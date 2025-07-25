using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject WarriorPrefab;
    public GameObject ArcherPrefab;
    public int SpawnRate=0;
    public bool Ticker;
    //public float IntervalSeconds = 1f;
    //public Queue<SpawnerDetails> QueueDetails;

   
    public class BakeMe : Baker<SpawnerAuthoring>
    {
        
        public override void Bake(SpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Spawner
            {
                Warrior = GetEntity(authoring.WarriorPrefab),
                Archer= GetEntity(authoring.ArcherPrefab),
                Ticker = authoring.Ticker,
            });
        }
    }
}
public struct Spawner : IComponentData
{
    //public float Timer, IntervalSeconds;
    public bool Ticker;
    public Entity Warrior;
    public Entity Archer;
    
}


partial struct SpawerSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //if (GameManager.Instance.isGamePaused) return;
        //foreach (var(localTranfrom,spawner) in SystemAPI.Query<RefRO<LocalTransform>, RefRW<Spawner>>())
        //{
        //    //spawner.ValueRW.Timer-=SystemAPI.Time.DeltaTime;
        //    //if (spawner.ValueRO.Timer > 0)
        //    //{
        //    //    continue;
        //    //}
        //    //spawner.ValueRW.Timer=spawner.ValueRO.IntervalSeconds;
        //    //if (!spawner.ValueRO.Ticker) { 
        //    //continue;
        //    //}
        //    //spawner.ValueRW.Ticker = false;
        //    //if(spawner.ValueRO.SpawnRate>0)
        //    var Queue = GameManager.Instance.SpwanerQueue;
        //    if (Queue.Count>0&& Queue!=null)
        //    {
        //        var count = Queue.Count;
        //        for (int j=0; j< count; j++)
        //        { 
        //        var temp= Queue.Dequeue();
        //        for (int i = 0; i < temp.SpawnRate; i++)
        //        {
        //            if (temp.type == Characters.Warrior)
        //            {
        //            Entity trooper = state.EntityManager.Instantiate(spawner.ValueRO.Warrior);
        //            SystemAPI.SetComponent(trooper, LocalTransform.FromPosition(localTranfrom.ValueRO.Position));
        //            }
        //            if(temp.type == Characters.Archer)
        //            {
        //                Entity trooper = state.EntityManager.Instantiate(spawner.ValueRO.Archer);
        //                SystemAPI.SetComponent(trooper, LocalTransform.FromPosition(localTranfrom.ValueRO.Position));
        //            }
        //        }
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("Spawnrate is Low");
        //    }
        //}
    }
}
