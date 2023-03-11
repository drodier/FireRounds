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

    private GameObject[,] mapTiles;
    private int mapSizeX;
    private int mapSizeY;

    public TileList tiles = new TileList();
    public TextAsset mapFile;
    public GameObject defaultTile;
    public CameraController cam;

    // Start is called before the first frame update
    void Start()
    {
        mapTiles = LoadTiles();

        cam.mapSize = new Vector2(mapSizeX, mapSizeY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject[,] LoadTiles()
    {
        tiles = JsonUtility.FromJson<TileList>(mapFile.text);
        mapSizeX = int.Parse(mapFile.name.Split(" ")[1]);
        mapSizeY = int.Parse(mapFile.name.Split(" ")[2]);

        GameObject[,] map = new GameObject[mapSizeX,mapSizeY];

        for(int y = 0; y < mapSizeY; y++)
        {
            for(int x = 0; x < mapSizeX; x++)
            {
                Tile currentTileStats = tiles.tiles[x + (mapSizeX * y)];
                GameObject currentTile = Instantiate(defaultTile);

                int tileX = currentTileStats.position[0];
                int tileY = currentTileStats.position[1];

                currentTile.transform.parent = this.transform;
                currentTile.transform.position = new Vector3(tileX,0,tileY);
                currentTile.name = "["+tileX+","+tileY+"]";

                currentTile.GetComponent<TileLogic>().stats = currentTileStats;
                map[tileX,tileY] = currentTile;
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
