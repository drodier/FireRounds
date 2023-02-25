using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    public Unit[] units;
    public MapLogic.Tile[,] mapGrid;

    // Start is called before the first frame update
    void Start()
    {
        units = FindObjectsOfType<Unit>();
        StartIniative();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartIniative()
    {
        Array.Sort(units, new UnitComparer());

        foreach(Unit unit in units)
        {
            Debug.Log(unit.name + "\n" +
                        "Hp : " + unit.getHealth() + "/" + unit.getMaxHealth() + "\n" +
                        "Mana : " + unit.getMana() + "/" + unit.getMaxMana() + "\n" +
                        "Initiative : " + unit.getInitiative());
        }

        units[0].toggleMenu();
    }
}
