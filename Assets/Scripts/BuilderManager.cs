using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class BuilderManager : MonoBehaviour
{
    public GameObject mapPrefab;
    public MapLogic map;
    public Canvas mapInfo;
    public TMP_InputField xInput;
    public TMP_InputField yInput;
    public TMP_InputField mapName;
    public CameraController cam;

    void Start()
    {

    }

    public void GenerateMap()
    {
        map = Instantiate(mapPrefab).GetComponent<MapLogic>();
        
        int xSize = int.Parse(xInput.text) > 30 ? 30 : int.Parse(xInput.text);
        int ySize = int.Parse(yInput.text) > 30 ? 30 : int.Parse(yInput.text);
        string name = mapName.text;

        mapInfo.enabled = false;
        map.mapSizeX = xSize;
        map.mapSizeY = ySize;

        MapLogic.TileList tiles = new MapLogic.TileList(new Vector2(xSize, ySize));

        for(int y=0;y<ySize;y++)
        {
            for(int x=0;x<xSize;x++)
            {
                tiles.tiles[x + (xSize * y)] = new Tile();
                tiles.tiles[x + (xSize * y)].position[0] = x;
                tiles.tiles[x + (xSize * y)].position[1] = y;
            }
        }

        System.IO.File.WriteAllText(Application.dataPath + "/Maps/"+name+".json", JsonUtility.ToJson(tiles) + "|" + xSize + "|" + ySize);

        map.mapFile = File.ReadAllText(Application.dataPath + "/Maps/"+name+".json");
        map.tiles = tiles;
        map.cam = cam;

        map.generateMap(true);
    }
}
