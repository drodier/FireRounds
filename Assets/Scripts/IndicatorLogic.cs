using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorLogic : MonoBehaviour
{
    public TileLogic tile;

    void OnMouseDown()
    {
        if(tile.activeMovement)
        {
            if(tile.unitOnTile == null)
            {
                tile.unitOnTile = tile.unitMoving;
                tile.unitOnTile.move(tile);
                FindObjectOfType<MapLogic>().resetTiles();
            }
        }
        else
        {
            tile.tileIndicator.GetComponent<Renderer>().material = tile.indicatorMaterials[tile.ACTIVE];
            tile.toggleUnitMenu();
        }
    }

    void OnMouseOver()
    {
        tile.isHovered = true;
    }

    void OnMouseExit()
    {
        tile.isHovered = false;
    }
}
