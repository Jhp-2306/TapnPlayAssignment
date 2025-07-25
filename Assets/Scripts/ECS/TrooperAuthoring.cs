using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class TrooperAuthoring : MonoBehaviour
{
    public float MoveSpeed;
    public float TrooperDamage;
    public float StoppingDistance;
    public int StartingHealth;
    public class BakeMe : Baker<TrooperAuthoring>
    {
        public override void Bake(TrooperAuthoring authoring)
        {
            Entity entity= GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Trooper
            {
                MoveSpeed=authoring.MoveSpeed,
                TrooperDamage=authoring.TrooperDamage,
                StoppingDistance=authoring.StoppingDistance,
                Health=authoring.StartingHealth,
            });
        }
    }


}
public struct Trooper : IComponentData
{
    public int Health;
    public float StoppingDistance;
    public float MoveSpeed;
    public float TrooperDamage;
}

partial struct TroopmoverSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //if (GameManager.Instance.isGamePaused) return;
        //EntityCommandBuffer commandBuffer= SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
        //foreach(var (localTransform,trooper,PVelocity, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<Trooper>,RefRW<PhysicsVelocity>>().WithEntityAccess())
        //{
        //    float3 tragetpos= GameManager.Instance.Tower.gameObject.transform.position;
        //    //Basic Movement
        //    if (math.distancesq(tragetpos, localTransform.ValueRO.Position) > trooper.ValueRO.StoppingDistance)
        //    {
        //        float3 dir=math.normalize(tragetpos-localTransform.ValueRO.Position);
        //    //Debug.Log("System call");
        //    PVelocity.ValueRW.Linear=dir*trooper.ValueRO.MoveSpeed;
        //    PVelocity.ValueRW.Angular=float3.zero;
        //    }
        //    //Attacking the tower
        //    if (math.distancesq(tragetpos, localTransform.ValueRO.Position) < trooper.ValueRO.StoppingDistance)
        //    {
        //        PVelocity.ValueRW.Linear = 0;
        //        GameManager.Instance.ReduceTowerValue(trooper.ValueRO.TrooperDamage);
        //        //Testing: Damage taking over time 
        //        trooper.ValueRW.Health -= 1;
        //    }
        //    //dying
        //    if (trooper.ValueRO.Health <= 0)
        //    {
        //        commandBuffer.DestroyEntity(entity);
        //    }
            

        //}
    }
}
