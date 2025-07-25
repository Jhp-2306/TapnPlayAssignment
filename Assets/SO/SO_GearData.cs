using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GearData",menuName ="Gear")]
public class SO_GearData : ScriptableObject
{
    public GearTypes type;
    public string displayname;
    public float addamount;
    public float multipleamount;
    public Sprite sprite;
    public bool isSpawnerGear;
    public float SpawnRate;
}
