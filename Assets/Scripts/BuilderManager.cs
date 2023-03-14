using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class BuilderManager : MonoBehaviour
{
    [System.Serializable]
    public class Brush{
        public Tile brushStats = new Tile();
    }

    public TMP_Dropdown BrushType;
    public TMP_InputField BrushHeight;
    public Toggle BrushWalkable;
    public Toggle BrushFlyable;
    public TMP_InputField BrushSlowing;
    public TMP_InputField BrushDamaging;

    public GameObject mapPrefab;
    public MapLogic map;
    public Canvas mapInfo;
    public TMP_InputField xInput;
    public TMP_InputField yInput;
    public TMP_InputField mapName;
    public CameraController cam;
    public Brush currentBrush;
    public bool isDrawing = false;
    public bool isLoaded = false;

    public Image brushPointer;
    public Vector3 brushDisplacement;

    private EscapeManager escapeMenu;

    void Start()
    {
        currentBrush = new Brush();
        escapeMenu = GameObject.Find("EscapeMenu").GetComponent<EscapeManager>();
    }

    void Update()
    {
        if(isDrawing)
            brushPointer.rectTransform.position = Input.mousePosition +  brushDisplacement;

        if(isLoaded && Input.GetKeyUp(KeyCode.Escape))
            escapeMenu.ToggleMenu();
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
            }
        }

        System.IO.File.WriteAllText(Application.dataPath + "/Maps/"+name+".json", JsonUtility.ToJson(tiles) + "|" + xSize + "|" + ySize);

        map.mapFile = File.ReadAllText(Application.dataPath + "/Maps/"+name+".json");
        map.tiles = tiles;
        map.cam = cam;

        map.generateMap(true);
        escapeMenu.mapName = name;
        GameObject.Find("BrushCanvas").GetComponent<Canvas>().enabled = true;
        GameObject.Find("Camera").GetComponent<CameraController>().isLocked = false;

        isLoaded = true;
    }

    public void PaintTile(TileLogic hoveredTile)
    {
        if(isDrawing)
        {
            hoveredTile.stats.tileType = currentBrush.brushStats.tileType;
            hoveredTile.stats.height = currentBrush.brushStats.height;
            hoveredTile.stats.walkable = currentBrush.brushStats.walkable;
            hoveredTile.stats.flyable = currentBrush.brushStats.flyable;
            hoveredTile.stats.slowing = currentBrush.brushStats.slowing;
            hoveredTile.stats.damaging = currentBrush.brushStats.damaging;

            escapeMenu.isSaved = false;
        }
    }

    public void ApplyBrush()
    {
        currentBrush.brushStats.tileType = BrushType.value;
        currentBrush.brushStats.height = int.Parse(BrushHeight.text);
        currentBrush.brushStats.walkable = BrushWalkable.isOn;
        currentBrush.brushStats.flyable = BrushFlyable.isOn;
        currentBrush.brushStats.slowing = int.Parse(BrushSlowing.text);
        currentBrush.brushStats.damaging = int.Parse(BrushDamaging.text);

        isDrawing = true;
        Cursor.visible = false;
    }

    public void ResetBrush()
    {
        currentBrush.brushStats.tileType = 0;
        currentBrush.brushStats.height = 1;
        currentBrush.brushStats.walkable = true;
        currentBrush.brushStats.flyable = true;
        currentBrush.brushStats.slowing = 0;
        currentBrush.brushStats.damaging = 0;

        isDrawing = false;
        brushPointer.rectTransform.position = new Vector3(-650, -300, -1);
        Cursor.visible = true;
    }
}
