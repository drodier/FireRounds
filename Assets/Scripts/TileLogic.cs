using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic : MonoBehaviour
{
    private Color defaultColor;
    private Color hoveredColor = new Color(0.4f, 1.0f, 0.4f, 0.8f);
    private Color activeColor = new Color(0.6f, 1.0f, 0.6f, 0.8f);
    private Color activeRangeColor = new Color(0.6f, 0.6f, 1.0f, 0.8f);
    private Unit unitMoving;
    private SpriteRenderer tileRenderer;
    private bool hovered = false;
    private bool activeMovement = false;
    private Unit unitOnTile;

    public MapLogic.Tile stats;

    // Start is called before the first frame update
    void Awake()
    {
        tileRenderer = GetComponent<SpriteRenderer>();
        defaultColor = tileRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(!activeMovement)
            tileRenderer.color = hovered ? hoveredColor : defaultColor;
    }

    public void setActiveRange(Unit unit)
    {
        tileRenderer.color = activeRangeColor;
        activeMovement = true;
        unitMoving = unit;
    }

    public void resetTile()
    {
        tileRenderer.color = defaultColor;
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
            tileRenderer.color = activeColor;
            toggleUnitMenu();
        }
    }

    void OnMouseOver()
    {
        hovered = true;
    }

    void OnMouseExit()
    {
        hovered = false;
    }

    public void toggleUnitMenu()
    {
        if(unitOnTile != null)
        {
            unitOnTile.toggleMenu();
        }
    }
}
