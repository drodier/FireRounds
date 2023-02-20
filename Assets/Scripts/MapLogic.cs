using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLogic : MonoBehaviour
{
    private TileLogic[,] mapTiles;
    private int mapSize;

    public TileLogic[] tileTypes;
    public TextAsset mapFile;

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
        TileLogic[] tiles = JsonUtility.FromJson<TileLogic[]>(mapFile.text);
        mapSize = tileTypes.Length/4;
        TileLogic[,] map = new TileLogic[mapSize,mapSize];

        //foreach(TileLogic tile in tiles)
        //{
        //    Debug.Log(tile.name);
        //}

        for(int i=0; i<mapSize; i++)
        {
            for(int y = 0; y <= mapSize; y++)
            {
                for(int x = 0; x <= mapSize; x++)
                {
                    int currentTileType = tiles[(y*mapSize)+x].tileType;
                    TileLogic currentTile = Instantiate(tileTypes[currentTileType]);

                    currentTile.transform.position = currentTile.position;
                    currentTile.position = new Vector2(x, y);
                    currentTile.name = "["+x+","+y+"]";

                    map[x,y] = currentTile;
                }
            }
        }
        return map;
    }

    public void showMovementRange(Unit unit)
    {
        for(int y = 0; y <= mapSize; y++)
        {
            for(int x = 0; x <= mapSize; x++)
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
        for(int y = 0; y <= mapSize; y++)
        {
            for(int x = 0; x <= mapSize; x++)
            {
                mapTiles[x,y].resetTile();
            }
        }
    }
}
