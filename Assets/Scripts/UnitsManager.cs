using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    public Unit[] units = new Unit[2];
    public GameObject[] unitTypes;
    public MapLogic.Tile[,] mapGrid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartIniative()
    {
        Array.Sort(units, new UnitComparer());
    }

    public void DebugUnits()
    {
        foreach(Unit unit in units)
        {
            Debug.Log(unit.name + "\n" +
                        "Hp : " + unit.getHealth() + "/" + unit.getMaxHealth() + "\n" +
                        "Mana : " + unit.getMana() + "/" + unit.getMaxMana() + "\n" +
                        "Initiative : " + unit.getInitiative());
        }
    }

    public void LoadUnits()
    {
        Debug.Log(units.Length + " - " + unitTypes.Length);
        units[0] = Instantiate(unitTypes[0]).GetComponent<Unit>();
        units[0].position = new Vector2(0,0);
        units[0].currentTile = GameObject.Find("[0,0]").GetComponent<TileLogic>();
        units[0].currentTile.unitOnTile = units[0];

        units[1] = Instantiate(unitTypes[1]).GetComponent<Unit>();
        units[1].position = new Vector2(3,3);
        units[1].currentTile = GameObject.Find("[3,3]").GetComponent<TileLogic>();
        units[1].currentTile.unitOnTile = units[1];

        StartIniative();
    }
}
