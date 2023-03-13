using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLogic : MonoBehaviour
{    
    [System.Serializable]
    public class TileList
    {
        public Tile[] tiles;

        public TileList(Vector2 size)
        {
            tiles = new Tile[(int)(size.x * size.y)];
        }
        public TileList()
        {
            tiles = new Tile[25];
        }
    }

    private GameObject[,] mapTiles;

    public int mapSizeX;
    public int mapSizeY;
    public TileList tiles = new TileList();
    public GameObject defaultTile;
    public CameraController cam;
    public string mapFile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject[,] LoadTiles()
    {
        tiles = JsonUtility.FromJson<TileList>(mapFile.Split("|")[0]);

        mapSizeX = int.Parse(mapFile.Split("|")[1]);
        mapSizeY = int.Parse(mapFile.Split("|")[2]);

        GameObject[,] map = new GameObject[mapSizeX,mapSizeY];

        for(int y = 0; y < mapSizeY; y++)
        {
            for(int x = 0; x < mapSizeX; x++)
            {
                Tile currentTileStats = tiles.tiles[x + (mapSizeX * y)];
                GameObject currentTile = Instantiate(defaultTile);

                currentTile.transform.parent = this.transform;
                currentTile.transform.position = new Vector3(x,0,y);
                currentTile.name = "["+x+","+y+"]";

                currentTile.GetComponent<TileLogic>().stats = currentTileStats;
                map[x,y] = currentTile;
            }
        }

        if(GameObject.Find("GameManager") != null)
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

    public void generateMap(bool isBuilder)
    {
        mapTiles = LoadTiles();

        cam.mapSize = new Vector2(mapSizeX, mapSizeY);

        if(isBuilder)
            setBuilderMode();
    }

    private void setBuilderMode()
    {
        for(int y = 0; y < mapSizeY; y++)
        {
            for(int x = 0; x < mapSizeX; x++)
            {
                mapTiles[x,y].GetComponent<TileLogic>().isEditable = true;
            }
        }
    }
}
