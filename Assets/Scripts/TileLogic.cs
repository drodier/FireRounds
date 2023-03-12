using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileLogic : MonoBehaviour
{
    public const int DEFAULT = 0;
    public const int GRASS = 1;
    public const int WATER = 2;
    public const int LAVA = 3;

    public const int DISABLED = 0;
    public const int HOVERED = 1;
    public const int ACTIVE = 2;
    public const int ACTIVE_RANGE = 3;    
    
    private Renderer tileRenderer;

    public Material[] indicatorMaterials;
    public Material[] tileMaterials;
    public GameObject tileIndicator;
    public Tile stats;
    public Unit unitOnTile;
    public Unit unitMoving;
    public bool isHovered = false;
    public bool isEditable = false;
    public bool activeMovement = false;


    void Start()
    {
        tileRenderer = GetComponent<Renderer>();
        if(stats.tileType <= DEFAULT)
            stats.tileType = DEFAULT;
        tileRenderer.material = tileMaterials[DEFAULT];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(!isEditable)
        {
            if(!activeMovement)
                tileIndicator.GetComponent<Renderer>().material = isHovered ? indicatorMaterials[HOVERED] : indicatorMaterials[DISABLED];

            tileRenderer.material = tileMaterials[stats.tileType];
        }
        else
        {

        }
    }

    public void setActiveRange(Unit unit)
    {
        if(!isEditable)
        {
            tileIndicator.GetComponent<Renderer>().material = indicatorMaterials[ACTIVE_RANGE];
            activeMovement = true;
            unitMoving = unit;
        }
    }

    public void resetTile()
    {
        tileIndicator.GetComponent<Renderer>().material = indicatorMaterials[DISABLED];
        activeMovement = false;
    }

    void OnMouseDown()
    {
        if(!isEditable)
        {
            if(activeMovement)
            {
                if(unitOnTile == null)
                {
                    unitOnTile = unitMoving;
                    unitOnTile.move(this);
                    FindObjectOfType<MapLogic>().resetTiles();
                }
            }
            else
            {
                tileIndicator.GetComponent<Renderer>().material = indicatorMaterials[ACTIVE];
                toggleUnitMenu();
            }
        }
        else
        {

        }
    }

    void OnMouseOver()
    {
        isHovered = true;
    }

    void OnMouseExit()
    {
        isHovered = false;
    }

    public void toggleUnitMenu()
    {
        if(unitOnTile != null)
        {
            unitOnTile.toggleMenu();
        }
    }

    public void editTile()
    {

    }

    public void showTileMenu()
    {
        GameObject.Find("tileType").GetComponent<TMP_Dropdown>().value = stats.tileType;
        GameObject.Find("height").GetComponent<TMP_InputField>().text = stats.height.ToString();
        GameObject.Find("walkable").GetComponent<Toggle>().isOn = stats.walkable;
        GameObject.Find("flyable").GetComponent<Toggle>().isOn = stats.flyable;
        GameObject.Find("slowing").GetComponent<TMP_InputField>().text = stats.slowing.ToString();
        GameObject.Find("damaging").GetComponent<TMP_InputField>().text = stats.damaging.ToString();
    }
}
