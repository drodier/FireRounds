using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{    
    [System.Serializable]
    public class UnitList
    {
        public UnitData[] units;
    }
    public UnitList unitsData = new UnitList();
    public TextAsset team;

    private Color inactiveColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);
    private Color activeColor = new Color(0.2f, 0.8f, 0.2f, 0.8f);
    private int currentUnitId = 0;
    private ArrayList cards = new ArrayList();

    public GameObject canvas;
    public UnitCard unitCard;
    public ArrayList units = new ArrayList();
    public GameObject[] unitTypes;
    public Tile[,] mapGrid;

    public void StartIniative()
    {
        Array.Sort(units, new UnitComparer());

        for(int i=0; i<units.Length; i++)
        {
            UnitCard currentCard = Instantiate(unitCard);
            currentCard.transform.SetParent(canvas.transform, false);
            currentCard.SetUnit(units[i]);
            currentCard.transform.position += new Vector3(i*75, 0, 0);
            cards.Add(currentCard);
        }

        currentUnitId = 0;
        units[currentUnitId].toggleActive();
        ((UnitCard)cards[currentUnitId]).GetComponent<Image>().color = activeColor;
    }

    public void LoadUnits()
    {
        unitsData = JsonUtility.FromJson<UnitList>(team.text);
        foreach(UnitData data in units)
        {
            GameObject currentUnit;
            switch(data._class)
            {
                case "Mage":
                    currentUnit = Instantiate(unitTypes[0]);
                    currentUnit.AddComponent(new Mage(data._level));
                break;
                case "Warrior":
                    currentUnit = Instantiate(unitTypes[1]);
                    currentUnit.AddComponent(new Mage(data._level));
                break;
            }

            currentUnit.name = data._class;
            currentUnit.GetComponent<Unit>().position = new Vector2(data._position[0],data._position[1]);
            currentUnit.GetComponent<Unit>().currentTile = GameObject.Find("["+data._position[0]+","+data._position[1]+"]").GetComponent<TileLogic>();
            currentUnit.GetComponent<Unit>().currentTile.unitOnTile = currentUnit;

            units.Add(currentUnit);
        }

        StartIniative();
    }

    public void NextTurn()
    {
        units[currentUnitId].toggleActive();
        ((UnitCard)cards[currentUnitId]).GetComponent<Image>().color = inactiveColor;

        currentUnitId = currentUnitId + 1 >= units.Length ? 0 : currentUnitId + 1;

        units[currentUnitId].toggleActive();
        ((UnitCard)cards[currentUnitId]).GetComponent<Image>().color = activeColor;
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
}
