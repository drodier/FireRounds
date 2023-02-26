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