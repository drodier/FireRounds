using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic : MonoBehaviour
{
    private Color defaultColor = new Color(1.0f, 1.0f, 1.0f, 0.8f);
    private Color hoveredColor = new Color(0.4f, 1.0f, 0.4f, 0.8f);
    private Color activeColor = new Color(0.6f, 1.0f, 0.6f, 0.8f);
    private Color activeRangeColor = new Color(0.6f, 0.6f, 1.0f, 0.8f);
    private Unit unitMoving;
    private SpriteRenderer tileRenderer;
    private bool hovered = false;
    private bool activeMovement = false;

    public Unit unitOnTile;
    public int tileType;
    public Vector2 position;
    public float height = 1;
    public bool walkable = true;
    public bool flyable = true;
    public bool slowing;
    public bool damaging;

    // Start is called before the first frame update
    void Awake()
    {
        tileRenderer = GetComponent<SpriteRenderer>();
        tileRenderer.color = defaultColor;
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
