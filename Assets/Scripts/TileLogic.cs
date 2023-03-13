using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(!activeMovement)
            tileIndicator.GetComponent<Renderer>().material = isHovered ? indicatorMaterials[HOVERED] : indicatorMaterials[DISABLED];

        tileRenderer.material = tileMaterials[stats.tileType];

        if(isEditable)
        {
            transform.localScale = new Vector3(transform.localScale.x, 1+(stats.height-1)/4, transform.localScale.z);
            transform.transform.position = new Vector3(transform.transform.position.x, (stats.height-1)/8, transform.transform.position.z);
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

    public void ClickEvent()
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
            showTileMenu();
        }
    }

    void OnMouseDown()
    {
        ClickEvent();
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
        TileEditor TileMenu = GameObject.Find("TileCanvas").GetComponent<TileEditor>();

        TileMenu.setTile(this);
    }
}
