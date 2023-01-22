using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public GridProvider gridProvider;
    public Vector2 Target { get; set; }
    public List<Vector3> Path { get; private set; } = new List<Vector3>();

    public void UpdatePath ()
    {
        Vector2Int o = gridProvider.WorldToGrid(transform.position);
        Vector2Int t = gridProvider.WorldToGrid(Target);
        IEnumerable<DSA.GridLocation> m_nodes = DSA.GridTraversal.AStar(gridProvider.Grid, o.x, o.y, t.x, t.y, false);

        Path.Clear();
        foreach (DSA.GridLocation l in m_nodes)
        {
            Vector3 v = gridProvider.GridToWorld(l);
            Path.Add(v);
        }
    }

    private void Awake()
    {
        gridProvider.GenerateGrid();
    }
}
