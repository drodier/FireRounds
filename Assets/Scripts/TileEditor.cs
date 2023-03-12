using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileEditor : MonoBehaviour
{
    private TileLogic selectedTile = null;

    void FixedUpdate()
    {
    }

    public void setTile(TileLogic tile)
    {
        selectedTile = tile;

        if(selectedTile != null)
        {
            GameObject.Find("TilePosition").GetComponent<TMP_Text>().text = selectedTile.name;

            GameObject.Find("TileTypeInput").GetComponent<TMP_Dropdown>().value = selectedTile.stats.tileType;
            GameObject.Find("HeightInput").GetComponent<TMP_InputField>().text = selectedTile.stats.height.ToString();
            GameObject.Find("WalkableInput").GetComponent<Toggle>().isOn = selectedTile.stats.walkable;
            GameObject.Find("FlyableInput").GetComponent<Toggle>().isOn = selectedTile.stats.flyable;
            GameObject.Find("SlowingInput").GetComponent<TMP_InputField>().text = selectedTile.stats.slowing.ToString();
            GameObject.Find("DamagingInput").GetComponent<TMP_InputField>().text = selectedTile.stats.damaging.ToString();
        }
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
