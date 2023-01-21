using UnityEngine;
using UnityEngine.Tilemaps;

public class GridProvider : MonoBehaviour
{
    [SerializeField] private Tilemap[] tilemaps;
    [HideInInspector] public DSA.Grid<DSA.WeightedNode> Grid;
    [SerializeField] private Transform corner;
    [SerializeField] private float cellSize;
    [SerializeField] private int gridSize;

    public void GenerateGrid ()
    {
        Grid = new DSA.Grid<DSA.WeightedNode>(gridSize);
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                float x = col * cellSize + corner.position.x;
                float y = -row * cellSize + corner.position.y;

                foreach (Tilemap t in tilemaps)
                {
                    Vector3Int cell = t.WorldToCell(new Vector3(x, y, 0));
                    if (t.HasTile(cell))
                    {
                        Grid.Set(col, row, new DSA.WeightedNode(Mathf.Infinity));
                        break;
                    }
                    else
                    {
                        Grid.Set(col, row, new DSA.WeightedNode(1));
                    }
                }
            }
        }
    }

    public Vector2Int WorldToGrid (Vector3 position)
    {
        int col = (int) ((position.x - corner.position.x) / cellSize);
        int row = (int) ((corner.position.y - position.y) / cellSize);
        return new Vector2Int(col, row);
    }
}
