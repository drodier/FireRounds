using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    private Color inactiveColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);
    private Color activeColor = new Color(0.2f, 0.8f, 0.2f, 0.8f);
    private int currentUnitId = 0;
    private ArrayList cards = new ArrayList();

    public GameObject canvas;
    public UnitCard unitCard;
    public Unit[] units = new Unit[2];
    public GameObject[] unitTypes;
    public MapLogic.Tile[,] mapGrid;

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
        units[0] = Instantiate(unitTypes[0]).GetComponent<Unit>();
        units[0].position = new Vector2(0,0);
        units[0].currentTile = GameObject.Find("[0,0]").GetComponent<TileLogic>();
        units[0].currentTile.unitOnTile = units[0];
        units[0].name = "Mage";

        units[1] = Instantiate(unitTypes[1]).GetComponent<Unit>();
        units[1].position = new Vector2(3,3);
        units[1].currentTile = GameObject.Find("[3,3]").GetComponent<TileLogic>();
        units[1].currentTile.unitOnTile = units[1];
        units[1].name = "Warrior";

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
