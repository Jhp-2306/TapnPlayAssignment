using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : MonoBehaviour
{
    GearPositionUI[,] grid;
    public Transform parent;
    public List<GearPositionUI> list;
    public GameObject RotorPrefab;
    public void Start()
    {
        var t = gameObject.GetComponentsInChildren<GearPositionUI>();
        list = t.ToList();
        grid = new GearPositionUI[6, 6];
        int idx = 0;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                list[idx].init(false);
                grid[i, j] = list[idx];
                grid[i,j].isClockWise = (i % 2 == 0 && j % 2 == 0)||(i%2!=0&&j % 2 !=0);
                idx++;
            }
        }
        //Assign Rotor to position
        var x = Random.Range(1, 5);
        var y = Random.Range(1, 5);
        RotorPrefab.transform.SetParent(grid[x,y].transform);
        RotorPrefab.GetComponent<Rotor>().dir=grid[x,y].isClockWise?-1:1;
        grid[x, y].init(true);
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                //Top 
                if (i - 1 >= 0)
                {
                    grid[i,j].AddNeighbour(grid[i-1,j]);
                }
                //bottom
                if (i + 1 < 6)
                {
                    grid[i, j].AddNeighbour(grid[i + 1, j]);
                }
                //Right
                if(j + 1 < 6)
                {
                    grid[i, j].AddNeighbour(grid[i,j+1]);
                }
                //Left
                if (j - 1 >= 0) {
                    grid[i, j].AddNeighbour(grid[i, j - 1]);
                }
                grid[i, j].name = $"{i},{j},N-{grid[i,j].neighbour.Count},{grid[i, j].IsRotorHere},dir{grid[i,j].isClockWise}";
            }
        }
    }

    public void OnRoundOver()
    {
        GameManager.Instance.Canspawn = false;
        GameManager.Instance.MySummoner.DeactivateAll();
        GameManager.Instance.MyShop.open();
    }
    public void DeactivateAll()
    {
        GameManager.Instance.MySummoner.DeactivateAll();
    }
}

