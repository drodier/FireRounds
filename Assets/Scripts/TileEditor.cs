using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileEditor : MonoBehaviour
{
    private TMP_Text tilePositionText;
    private TMP_Dropdown tileTypeInput;
    private TMP_InputField tileHeightInput;
    private Toggle tileWalkableInput;
    private Toggle tileFlyableInput;
    private TMP_InputField tileSlowingInput;
    private TMP_InputField tileDamagingInput;

    private TileLogic selectedTile = null;

    public Vector2 openPosition;
    public Vector2 closedPosition;
    public float animationSpeed = 100;
    public bool isOpen = false;
    public RectTransform drawer;

    void Start()
    {
        tilePositionText = GameObject.Find("TilePosition").GetComponent<TMP_Text>();
        tileTypeInput = GameObject.Find("TileTypeInput").GetComponent<TMP_Dropdown>();
        tileHeightInput = GameObject.Find("HeightInput").GetComponent<TMP_InputField>();
        tileWalkableInput = GameObject.Find("WalkableInput").GetComponent<Toggle>();
        tileFlyableInput = GameObject.Find("FlyableInput").GetComponent<Toggle>();
        tileSlowingInput =  GameObject.Find("SlowingInput").GetComponent<TMP_InputField>();
        tileDamagingInput = GameObject.Find("DamagingInput").GetComponent<TMP_InputField>();
    }

    void FixedUpdate()
    {
        animateMenu();
    }

    private void animateMenu()
    {
        if(isOpen)
        {
            if(drawer.anchoredPosition.x > openPosition.x)
            {
                drawer.anchoredPosition -= new Vector2(animationSpeed * Time.deltaTime, 0);
            }
            else
            {
                drawer.anchoredPosition = openPosition;
            }
        }
        else
        {
            if(drawer.anchoredPosition.x < closedPosition.x)
            {
                drawer.anchoredPosition += new Vector2(animationSpeed * Time.deltaTime, 0);
            }
            else
            {
                drawer.anchoredPosition = closedPosition;
            }
        }
    }

    public void setTile(TileLogic tile)
    {
        selectedTile = tile;

        if(selectedTile != null)
        {
            tilePositionText.text = selectedTile.name;
            tileTypeInput.value = selectedTile.stats.tileType;
            tileHeightInput.text = selectedTile.stats.height.ToString();
            tileWalkableInput.isOn = selectedTile.stats.walkable;
            tileFlyableInput.isOn = selectedTile.stats.flyable;
            tileSlowingInput.text = selectedTile.stats.slowing.ToString();
            tileDamagingInput.text = selectedTile.stats.damaging.ToString();
        }

        openMenu();
    }

    public void openMenu()
    {
        GetComponent<Canvas>().enabled = true;
    }
    
    public void toggleMenu()
    {
        isOpen = !isOpen;
    }

    public void ChangeType()
    {
        if(selectedTile != null)
            selectedTile.stats.tileType = GameObject.Find("TileTypeInput").GetComponent<TMP_Dropdown>().value;
    }

    public void ChangeHeight()
    {
        if(selectedTile != null)
        {
            string tmp = GameObject.Find("HeightInput").GetComponent<TMP_InputField>().text;
            selectedTile.stats.height = tmp == null ? 1 : int.Parse(tmp);
        }
    }

    public void ChangeWalkable()
    {
        if(selectedTile != null)
            selectedTile.stats.walkable = GameObject.Find("WalkableInput").GetComponent<Toggle>().isOn;
    }

    public void ChangeFlyable()
    {
        if(selectedTile != null)
            selectedTile.stats.flyable = GameObject.Find("FlyableInput").GetComponent<Toggle>().isOn;
    }

    public void ChangeSlowing()
    {
        if(selectedTile != null)
        {
            string tmp = GameObject.Find("SlowingInput").GetComponent<TMP_InputField>().text;
            selectedTile.stats.slowing = tmp == null ? 1 : int.Parse(tmp);
        }
    }

    public void ChangeDamaging()
    {
        if(selectedTile != null)
        {
            string tmp = GameObject.Find("DamagingInput").GetComponent<TMP_InputField>().text;
            selectedTile.stats.damaging = tmp == null ? 1 : int.Parse(tmp);
        }
    }
}
