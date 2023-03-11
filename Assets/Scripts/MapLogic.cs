using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLogic : MonoBehaviour
{    
    [System.Serializable]
    public class TileList
    {
        public Tile[] tiles;
    }

    public TileList tiles = new TileList();
    private GameObject[,] mapTiles;
    private int mapSizeX;
    private int mapSizeY;

    public TextAsset mapFile;
    public GameObject defaultTile;

    // Start is called before the first frame update
    void Start()
    {
        mapTiles = LoadTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject[,] LoadTiles()
    {
        tiles = JsonUtility.FromJson<TileList>(mapFile.text);
        mapSizeX = tiles.tiles[tiles.tiles.Length-1].position[0]+1;
        mapSizeY = tiles.tiles[tiles.tiles.Length-1].position[1]+1;

        GameObject[,] map = new GameObject[mapSizeX,mapSizeY];

        for(int y = 0; y < mapSizeY; y++)
        {
            for(int x = 0; x < mapSizeX; x++)
            {
                Tile currentTileStats = tiles.tiles[(y*mapSizeX)+x];
                GameObject currentTile = Instantiate(defaultTile);

                currentTile.transform.parent = this.transform;
                currentTile.transform.position = new Vector3(x,0,y);
                currentTile.name = "["+x+","+y+"]";

                currentTile.GetComponent<TileLogic>().stats = currentTileStats;
                map[x,y] = currentTile;
            }
        }

        GameObject.Find("GameManager").GetComponent<UnitsManager>().LoadUnits();

        return map;
    }

    public void showMovementRange(Unit unit)
    {
        for(int y = 0; y < mapSizeY; y++)
        {
            for(int x = 0; x < mapSizeX; x++)
            {
                if(Mathf.Abs(unit.position.x - x) + Mathf.Abs(unit.position.y - y) <= unit.getMovement())
                {
                    mapTiles[x,y].GetComponent<TileLogic>().setActiveRange(unit);
                }
            }
        }
    }

    public void resetTiles()
    {
        for(int y = 0; y < mapSizeY; y++)
        {
            for(int x = 0; x < mapSizeX; x++)
            {
                mapTiles[x,y].GetComponent<TileLogic>().resetTile();
            }
        }
    }
}
