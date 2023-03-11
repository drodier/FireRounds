using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic : MonoBehaviour
{
    public int DEFAULT = 0;
    public int GRASS = 1;
    public int WATER = 2;
    public int LAVA = 3;

    public int DISABLED = 0;
    public int HOVERED = 1;
    public int ACTIVE = 2;
    public int ACTIVE_RANGE = 3;    
    
    private Renderer tileRenderer;

    public Material[] indicatorMaterials;
    public Material[] tileMaterials;
    public GameObject tileIndicator;
    public Tile stats;
    public Unit unitOnTile;
    public Unit unitMoving;
    public bool isHovered = false;
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
        if(!activeMovement)
            tileIndicator.GetComponent<Renderer>().material = isHovered ? indicatorMaterials[HOVERED] : indicatorMaterials[DISABLED];

        tileRenderer.material = tileMaterials[stats.tileType];
    }

    public void setActiveRange(Unit unit)
    {
        tileIndicator.GetComponent<Renderer>().material = indicatorMaterials[ACTIVE_RANGE];
        activeMovement = true;
        unitMoving = unit;
    }

    public void resetTile()
    {
        tileIndicator.GetComponent<Renderer>().material = indicatorMaterials[DISABLED];
        activeMovement = false;
    }

    void OnMouseDown()
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
}
