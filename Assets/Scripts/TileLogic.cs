using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic : MonoBehaviour
{
    private Color defaultColor = new Color(1.0f, 1.0f, 1.0f, 0.8f);
    private Color hoveredColor = new Color(0.4f, 1.0f, 0.4f, 0.8f);
    private Color activeColor = new Color(0.6f, 1.0f, 0.6f, 0.8f);
    private SpriteRenderer tileRenderer;
    private bool hovered = false;

    public UnitController unitOnTile;

    // Start is called before the first frame update
    void Start()
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
        tileRenderer.color = hovered ? hoveredColor : defaultColor;
    }

    void OnMouseDown()
    {
        tileRenderer.color = activeColor;
        if(unitOnTile != null)
        {
            unitOnTile.toggleMenu();
            Camera.main.GetComponent<CameraController>().toggleCameraLock();
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
}
