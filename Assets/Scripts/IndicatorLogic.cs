using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorLogic : MonoBehaviour
{
    public TileLogic tile;

    void OnMouseDown()
    {
        tile.ClickEvent();
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
