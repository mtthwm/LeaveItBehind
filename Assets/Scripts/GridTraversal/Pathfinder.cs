using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public GridProvider gridProvider;
    public Vector2 Target { 
        get { return m_target; } 
        set {
            m_target = value;
            GetPath();
        } 
    }
    public IEnumerable<DSA.GridLocation> Nodes { get; private set; }

    private void GetPath ()
    {
        Vector2Int o = gridProvider.WorldToGrid(transform.position);
        Vector2Int t = gridProvider.WorldToGrid(Target);
        Nodes = DSA.GridTraversal.AStar(gridProvider.Grid, o.x, o.y, t.x, t.y);
    }

    private void Awake()
    {
        gridProvider.GenerateGrid();
    }

    private Vector2 m_target;
}
