using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLogic : MonoBehaviour
{
    [System.Serializable]
    public class Tile
    {
        public int tileType;
        public int[] position;
        public float height = 1;
        public bool walkable = true;
        public bool flyable = true;
        public bool slowing = false;
        public bool damaging = false;
    }
    
    [System.Serializable]
    public class TileList
    {
        public Tile[] tiles;
    }

    public TileList tiles = new TileList();
    private TileLogic[,] mapTiles;
    private int mapSizeX;
    private int mapSizeY;

    public GameObject[] tileTypes;
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
        tiles = JsonUtility.FromJson<TileList>(mapFile.text);
        mapSizeX = tiles.tiles[tiles.tiles.Length-1].position[0]+1;
        mapSizeY = tiles.tiles[tiles.tiles.Length-1].position[1]+1;
        TileLogic[,] map = new TileLogic[mapSizeX,mapSizeY];

        for(int y = 0; y < mapSizeY; y++)
        {
            for(int x = 0; x < mapSizeX; x++)
            {
                Tile currentTileStats = tiles.tiles[(y*mapSizeX)+x];
                GameObject currentTile = Instantiate(tileTypes[currentTileStats.tileType]);
                currentTile.transform.parent = this.transform;
                TileLogic currentLogic = currentTile.AddComponent<TileLogic>();
                currentLogic.stats = currentTileStats;

                currentTile.transform.position = new Vector2(x,y);
                currentLogic.stats = currentTileStats;
                currentTile.name = "["+x+","+y+"]";

                map[x,y] = currentLogic;
            }
        }
        return map;
    }

    public void showMovementRange(Unit unit)
    {
        for(int y = 0; y <= mapSizeY; y++)
        {
            for(int x = 0; x <= mapSizeX; x++)
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
        for(int y = 0; y <= mapSizeY; y++)
        {
            for(int x = 0; x <= mapSizeX; x++)
            {
                mapTiles[x,y].resetTile();
            }
        }
    }
}
