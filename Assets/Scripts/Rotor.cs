using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotor : MonoBehaviour
{
   public AnimationCurve curve;
    float time;
    [Range(0f, 1000f)]
    public float speed = 1000;
    public float dir = 0;
    private void Update()
    {
        gameObject.transform.eulerAngles += Vector3.forward * dir*speed* curve.Evaluate(time) * Time.deltaTime;
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.H))
        {
            time = 0;
        }
    }
    
    void hit()
    {
        time = 0;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit();
        var temp=new List<GearPositionUI>();
        
        collision.transform.parent.GetComponent<GearPositionUI>().Hit(temp);
    }
}
