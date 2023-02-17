using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLogic : MonoBehaviour
{
    private TileLogic[,] mapTiles;

    // Start is called before the first frame update
    void Start()
    {
        mapTiles = LoadTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private TileLogic[,] LoadTiles()
    {
        TileLogic[] tiles = GetComponentsInChildren<TileLogic>();
        TileLogic[,] map = new TileLogic[20,11];

        for(int y = 0; y <= 10; y++)
        {
            for(int x = 0; x <= 19; x++)
            {
                TileLogic currentTile = tiles[(y*20)+x];
                map[x,y] = currentTile;
            }
        }
        return map;
    }

    public void showMovementRange(Unit unit)
    {
        for(int y = 0; y <= 10; y++)
        {
            for(int x = 0; x <= 19; x++)
            {
                if(Mathf.Abs(unit.position.x - x) + Mathf.Abs(unit.position.y - y) <= unit.getMovement())
                {
                    mapTiles[x,y].setActiveRange(unit);
                }
            }
        }
    }

    public void resetTiles()
    {
        for(int y = 0; y <= 10; y++)
        {
            for(int x = 0; x <= 19; x++)
            {
                mapTiles[x,y].resetTile();
            }
        }
    }

    private void DebugMap()
    {
        for(int y = 0; y < 10; y++)
        {
            Debug.Log(mapTiles[0,y].name + "-" + 
                        mapTiles[1,y].name + "-" + 
                        mapTiles[2,y].name + "-" + 
                        mapTiles[3,y].name + "-" + 
                        mapTiles[4,y].name + "-" + 
                        mapTiles[5,y].name + "-" + 
                        mapTiles[6,y].name + "-" + 
                        mapTiles[7,y].name + "-" + 
                        mapTiles[8,y].name + "-" + 
                        mapTiles[9,y].name + "-" + 
                        mapTiles[10,y].name + "-" + 
                        mapTiles[11,y].name + "-" + 
                        mapTiles[12,y].name + "-" + 
                        mapTiles[13,y].name + "-" + 
                        mapTiles[14,y].name + "-" + 
                        mapTiles[15,y].name + "-" + 
                        mapTiles[16,y].name + "-" + 
                        mapTiles[17,y].name + "-" + 
                        mapTiles[18,y].name + "-" + 
                        mapTiles[19,y].name);
        }
    }
}
