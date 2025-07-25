using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    Queue<GG_Character> m_CharactersQueue;
    public bool ispreSpawn;
    public int PreSpawnValue;
    public GameObject Character;
    public List<SO_Characters> m_CharactersData;
    private void Start()
    {
        m_CharactersQueue = new Queue<GG_Character>();
        if (ispreSpawn)
            MassSpawn(PreSpawnValue);
    }
    private void Update()
    {
        var count = GameManager.Instance.SpwanerQueue.Count;
        if (count > 0)
        {
            for (int i = 0; i < count; i++) { 
            var data=GameManager.Instance.SpwanerQueue.Dequeue();
                SpawnCharacter(data);
            }
        }
        //DeactivateAll();
    }
    public void MassSpawn(int SpawnValue)
    {
        for (int i = 0; i < SpawnValue; i++) { 
        var t=Instantiate(Character,this.transform).GetComponent<GG_Character>();
            t.Deactivate();
            //m_CharactersQueue.Enqueue(t);
        }
        
    }
    public void SpawnCharacter(SpwanerQueueElements details)
    {
        if (details.SpawnRate > m_CharactersQueue.Count) { 
        MassSpawn((int)details.SpawnRate-m_CharactersQueue.Count);
        }

        for(int i = 0; i < details.SpawnRate; i++)
        {
            var t= m_CharactersQueue.Dequeue();
            t.transform.position=this.transform.position;
            t.Init(GetCharacterData(details.type));
            t.SetTarget(GameManager.Instance.Tower.gameObject);
        }
    }
    public void AddCharacterTotheQueue(GG_Character character)
    {
        m_CharactersQueue.Enqueue(character);
    }
    public SO_Characters GetCharacterData(Characters type)
    {
        foreach (var c in m_CharactersData)
        {
            if(c.type == type) return c;
        }
        return null;
    }

    public void DeactivateAll()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++) {
            gameObject.transform.GetChild(i).GetComponent<GG_Character>().Deactivate();
        }
    }
}
