using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Action ModRefresher = delegate { };
    public Tower Tower;
    public SpawnManager MySummoner;
    public Shop MyShop;
    public bool Canspawn;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SpwanerQueue = new Queue<SpwanerQueueElements>();
    }
    //[Serializable]
    //public struct GearData
    //{
    //    public GearTypes type;
    //    public string displayname;
    //    public float amount;
    //    public Sprite sprite;
    //    public bool isSpawnerGear;
    //}
    //[SerializeField] GearData One, Two, Four, Eight, x12, x125, x2;
    public List<SO_GearData> GearDatas;
    public Entity ArcherEntity;
    public Entity WarriorEntity;

    public bool isGamePaused { get; internal set; }

    public Queue<SpwanerQueueElements> SpwanerQueue;
    public SO_GearData MergeCombination(GearTypes type)
    {
        switch (type)
        {
            case GearTypes.one: return GetGearData(GearTypes.two);
            case GearTypes.two: return GetGearData(GearTypes.four);
            case GearTypes.four: return GetGearData(GearTypes.eight);
            case GearTypes.x12: return GetGearData(GearTypes.x125);
            case GearTypes.x125: return GetGearData(GearTypes.x2);
        }
        return null;
    }
    public SO_GearData GetSpecficData(GearTypes type)
    {
        return GetGearData(type);
    }
    
    public SO_GearData GetGearData(GearTypes type)
    {
        foreach (var t in GearDatas)
        {
            if (t.type == type)
            {
                return t;
            }
        }
        return null;
    }
    private void Update()
    {
        
    }
    public void ReduceTowerValue(float trooperDamage)
    {

    }
    void check()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        // Get the prefab entity from the component
        Entity prefabEntity = entityManager.CreateEntityQuery(typeof(PrefabLibReference))
                                           .GetSingleton<PrefabLibReference>().prefabEntity;

        // Instantiate it
        Entity spawned = entityManager.Instantiate(prefabEntity);
        Debug.Log($"created here {spawned == null}");
    }



}
public struct SpwanerQueueElements
{
    public Characters type;
    public float SpawnRate;
}
public enum Characters
{
    Warrior,Archer
}
