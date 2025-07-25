using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<Transform> Slots;
    public GameObject GearPrefab;
    private void Start()
    {
        CreateGear();//Testing only
    }
    public void open()
    {
        this.gameObject.SetActive(true);
        Init();
    }
    public void Init()
    {
        CreateGear();

    }
    public void OnAttack()
    {
        //Start the Game Mode
        this.gameObject.SetActive(false);
        GameManager.Instance.Canspawn = true;
    }
    public void OnReRoll()
    {
        CreateGear();
        
    }
    void CreateGear()
    {
        foreach (Transform slot in Slots)
        {
            if (slot.childCount == 0)
            {
                Instantiate(GearPrefab, slot);
            }
            var temp = (GearTypes)Random.Range(0, (int)GearTypes.count);
            Debug.Log(temp);
            slot.GetComponentInChildren<Gear>().Init(temp);
        }
    }
    
}
