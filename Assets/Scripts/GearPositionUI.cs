using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class GearPositionUI : MonoBehaviour, IDropHandler
{
    public bool IsRotorHere;
    public List<GearPositionUI> neighbour;
    public bool isClockWise;
    public bool isGearhere { get => GetComponentInChildren<Gear>(); }

    public Gear MyGear
    {
        get
        {
            if (isGearhere)
            {
                return GetComponentInChildren<Gear>();
            }
            return null;
        }
    }
    public void init(bool _isrotorhere)
    {
        IsRotorHere = _isrotorhere;
        //if (IsRotorHere)
        //{
        //    GameManager.ModRefresher -= Refresher;
        //    GameManager.ModRefresher += Refresher;
        //}
        neighbour = new List<GearPositionUI>();
    }
    private void OnDisable()
    {
        //if (IsRotorHere) { 
        //GameManager.ModRefresher-= Refresher;
        //}
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (transform.childCount == 1)
        {
            DrawableItem draggableItem = dropped.GetComponent<DrawableItem>();
            draggableItem.parentAfterDrag = transform;
            //here Check is neighbour is Rotor or connected to rotor
            dropped.GetComponent<Gear>().AfterPlacingOnGrid(isRotorOrConnected());
            if (isRotorOrConnected()) { 
            //Self Aline the gear
            }

        }
        else //check for Merger here
        {
            Gear gear = dropped.GetComponent<Gear>();
            if (isGearhere)
                if (MyGear.CanMerge(gear.GetType()))
                {
                    gear.Merge(true);
                    MyGear.Merge(false);
                    GameManager.ModRefresher?.Invoke();
                }
        }
    }
    private void Refresher()
    {
        if (IsRotorHere) {
            var temp=new List<GearPositionUI>();
            float add = 0f;
            float Multiple = 1f;
            GetModValue(temp,ref add,ref Multiple);
            Debug.Log(add*Multiple);
            List<GearPositionUI> connection1 = new List<GearPositionUI>();
            if (neighbour[0].isGearhere) neighbour[0].GetconnectionList(ref connection1);
            List<GearPositionUI> connection2 = new List<GearPositionUI>();
            if (neighbour[1].isGearhere) neighbour[1].GetconnectionList(ref connection2);
            List<GearPositionUI> connection3 = new List<GearPositionUI>();
            if (neighbour[2].isGearhere) neighbour[2].GetconnectionList(ref connection3);
            List<GearPositionUI> connection4 = new List<GearPositionUI>();
            if (neighbour[3].isGearhere) neighbour[3].GetconnectionList(ref connection4);
            
            //TODO: Misplace Debuff Mod Value here
            //if (connection1.Contains(connection2[0])
        }
    }
    bool isRotorOrConnected()
    {
        List<GearPositionUI> sender=new List<GearPositionUI>();
        foreach (var t in neighbour)
        {
            if (t.IsRotorHere) UpdatetheConnections(sender);
            if (t.IsRotorHere ||( t.MyGear!=null && t.MyGear.isConnectedToRotor))
            {
                return true;
            }
        }
        return false;
    }
    public void UpdatetheConnections(List<GearPositionUI> sender)
    {
        sender.Add(this);
        foreach (var t in neighbour) {
            if (t.MyGear != null && !t.MyGear.isConnectedToRotor && !sender.Contains(t))
            {
                t.MyGear.isConnectedToRotor = true;
                t.UpdatetheConnections(sender);
            }
        }
        if (sender.IndexOf(this) == 0)
        {
            GameManager.ModRefresher?.Invoke();
        }
    }
    public void AddNeighbour(GearPositionUI gear)
    {
        neighbour.Add(gear);
    }
    public void Hit(List<GearPositionUI> sender)
    {
        //Debug.Log($"Check From {gameObject.name}");
        if (MyGear.isSpawner()&&GameManager.Instance.Canspawn)
        {
            MyGear.OnHitAction();
        }
        MyGear.PlayAnimation(isClockWise);
        sender.Add(this);
        foreach(var t in neighbour)
        {
            if (t.isGearhere&&(!sender.Contains(t)||sender==null))
            {
                t.Hit(sender);
                //return;
            }
        }
        return;
        
    }
    public void GetModValue(List<GearPositionUI> sender,ref float add,ref float multi)
    {
        var value = 0f;
        if (!IsRotorHere)
        {
        add += MyGear.GetAddAmount();
        multi += MyGear.GetMultiplyAmount();
        }
        sender.Add(this);
        foreach (var t in neighbour)
        {
            if (t.isGearhere && (!sender.Contains(t) || sender == null))
            {
                t.GetModValue(sender,ref add,ref multi);
                //return;
            }
        }
        //return value;
    }
    public void GetconnectionList(ref List<GearPositionUI> sender) {

        if (!sender.Contains(this)) {
            sender.Add(this);
        }
        if(!IsRotorHere)
            foreach (var t in neighbour)
            {
                if (t.isGearhere && (!sender.Contains(t) || sender == null))
                {
                    //t.GetModValue(sender, ref add, ref multi);
                    t.GetconnectionList(ref sender);
                    //return;
                }
            }
    }
}
