using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Character",menuName ="Character")]
public class SO_Characters : ScriptableObject
{
    public float Health;
    public Characters type;
    public float movementSpeed;
    public float stoppingDistance;
    public int ATKDamage;
    public bool ismelee;
    public AnimationClip Idle;
    public AnimationClip Run;
    //public AnimationClip Hit;
}
