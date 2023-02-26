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
    public GameObject unitType;
    public Tile[,] mapGrid;

    public void StartIniative()
    {
        SortUnits();

        for(int i=0; i<units.Count; i++)
        {
            UnitCard currentCard = Instantiate(unitCard);
            currentCard.transform.SetParent(canvas.transform, false);
            currentCard.SetUnit(((GameObject)units[i]).GetComponent<Unit>());
            currentCard.transform.position += new Vector3(i*75, 0, 0);
            cards.Add(currentCard);
        }

        currentUnitId = 0;
        ((GameObject)units[currentUnitId]).GetComponent<Unit>().toggleActive();
        ((UnitCard)cards[currentUnitId]).GetComponent<Image>().color = activeColor;
    }

    public void LoadUnits()
    {
        unitsData = JsonUtility.FromJson<UnitList>(team.text);

        foreach(UnitData data in unitsData.units)
        {
            GameObject currentUnit = Instantiate(unitType);

            switch(data._class)
            {
                case "Mage":
                    currentUnit.GetComponent<Unit>().unitClass = new Mage(data._level);
                break;
                case "Warrior":
                    currentUnit.GetComponent<Unit>().unitClass = new Warrior(data._level);
                break;
            }

            currentUnit.name = data._class;
            currentUnit.GetComponent<Unit>().position = new Vector2(data._position[0],data._position[1]);
            currentUnit.GetComponent<Unit>().currentTile = GameObject.Find("["+data._position[0]+","+data._position[1]+"]").GetComponent<TileLogic>();
            currentUnit.GetComponent<Unit>().currentTile.unitOnTile = currentUnit.GetComponent<Unit>();
            currentUnit.GetComponent<Unit>().currentHealth = currentUnit.GetComponent<Unit>().unitClass.maxHealth;
            currentUnit.GetComponent<Unit>().currentMana = currentUnit.GetComponent<Unit>().unitClass.maxMana;
            currentUnit.transform.SetParent(transform);

            units.Add(currentUnit);
        }

        StartIniative();
    }

    public void NextTurn()
    {
        ((GameObject)units[currentUnitId]).GetComponent<Unit>().toggleActive();
        ((UnitCard)cards[currentUnitId]).GetComponent<Image>().color = inactiveColor;

        currentUnitId = currentUnitId + 1 >= units.Count ? 0 : currentUnitId + 1;

        ((GameObject)units[currentUnitId]).GetComponent<Unit>().toggleActive();
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

    private void SortUnits()
    {
        for(int i=0;i<units.Count-1;i++)
        {
            for(int j=0;j<units.Count-(i+1);j++)
            {
                if(((GameObject)units[j]).GetComponent<Unit>().getInitiative() >= ((GameObject)units[j+1]).GetComponent<Unit>().getInitiative())
                {
                    GameObject tempUnit = (GameObject)units[j];
                    units.RemoveAt(j);
                    units.Add(tempUnit);
                }
            }
        }
    }
}
