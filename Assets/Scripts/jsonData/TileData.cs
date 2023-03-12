[System.Serializable]
public class Tile
{
    public int tileType = 0;
    public int[] position = new int[2];
    public float height = 1;
    public bool walkable = true;
    public bool flyable = true;
    public bool slowing = false;
    public bool damaging = false;
}