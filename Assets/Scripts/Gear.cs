using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public enum GearTypes
{
    one,
    two,
    four,
    eight,
    x12,
    x125,
    x2,
    warrior,
    archer,
    count
}
public class Gear : DrawableItem
{
    //public GearTypes Type;//Testing only
    public TextMeshProUGUI Text;
    public TextMeshProUGUI RateText;
    SO_GearData MyData;
    public bool isConnectedToRotor;
    Coroutine RotationCoroutine;
   public Entity prefab;
    //Summoning Gear properites
    float currentFillRate;
    float TickRate;
    public void Start()
    {
        //MyData= GameManager.Instance.GetSpecficData(Type);
        //SetGear();
    }
    public void Init(GearTypes type)
    {
        MyData = GameManager.Instance.GetSpecficData(type);
        SetGear();
    }
    public void AfterPlacingOnGrid(bool isConnected)
    {
        isConnectedToRotor = isConnected;
        RateText.gameObject.SetActive(MyData.isSpawnerGear);
        if (MyData.isSpawnerGear)
        {
            GameManager.ModRefresher -= GetModValue;
            GameManager.ModRefresher += GetModValue;
            //var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            //if (entityManager.Exists(Spa))
            //{
            //    if (entityManager.HasComponent<Spawner>(ECS_SpawnerEntity))
            //    {
            //        var spawner = entityManager.GetComponentData<Spawner>(ECS_SpawnerEntity);
            //        spawner.SpawnRate = TickRate < 0.6f ? 1 : (int)TickRate;
            //        spawner.Ticker = true;
            //        entityManager.SetComponentData(ECS_SpawnerEntity, spawner);
            //    }
            //}
        }
    }

    public GearTypes GetType() => MyData.type;
    public float GetAddAmount() => MyData.addamount;
    public float GetMultiplyAmount() => MyData.multipleamount;
    public bool isSpawner() => MyData.isSpawnerGear;
    public bool CanMerge(GearTypes type) => MyData.type == type && MyData.type != GearTypes.eight && MyData.type != GearTypes.x2;
    public void Merge(bool isDestory)
    {
        Debug.Log($"Merging {MyData.type},{isDestory}");
        if (isDestory) { Destroy(this.gameObject); }
        MyData = GameManager.Instance.MergeCombination(MyData.type);
        //Reset the Asset and value
        SetGear();
    }
    public void SetGear()
    {
        Text.text = MyData.displayname;
        image.sprite = MyData.sprite;
        GameManager.ModRefresher -= GetModValue;
    }
    public void OnHitAction()
    {
        //spawner here
        currentFillRate += TickRate;
        if (currentFillRate > 1)
        {
            
            GameManager.Instance.SpwanerQueue.Enqueue(new SpwanerQueueElements
            {
                type=MyData.type==GearTypes.warrior?Characters.Warrior:Characters.Archer,
                SpawnRate=(int)currentFillRate,
            });

            currentFillRate = 0;
        }
    }
    void GetModValue()
    {
        var temp = new List<GearPositionUI>();
        float add = 0f;
        float Multiple = 1f;
        this.transform.parent.GetComponent<GearPositionUI>().GetModValue(temp, ref add, ref Multiple);
        TickRate= (MyData.SpawnRate + add)*Multiple;
        RateText.text = isConnectedToRotor? $"{TickRate.ToString("f2")}u/t":$"0u/t";
        //Debug.Log($"Mod Value{add * Multiple} on {this.gameObject.name}");
    }
    public void PlayAnimation(bool dir)
    {
        var _dir = dir ? -1 : 1;
        if (RotationCoroutine != null)
            StopCoroutine(RotationCoroutine);
        RotationCoroutine = StartCoroutine(RotateOverTime(40f * _dir, 0.35f, Vector3.forward));
    }
    public IEnumerator RotateOverTime(float angleAmount, float duration, Vector3 axis)
    {
        Quaternion startRotation = image.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.AngleAxis(angleAmount, axis);
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            image.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }
        transform.rotation = endRotation;
    }
}
