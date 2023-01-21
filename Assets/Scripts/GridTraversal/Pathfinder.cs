using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] GridProvider gridProvider;
    public Vector2 Target { 
        get { return m_target; } 
        set {
            Vector2Int o = gridProvider.WorldToGrid(transform.position);
            Vector2Int t = gridProvider.WorldToGrid(transform.position);
            m_nodes = DSA.GridTraversal.AStar(gridProvider.Grid, o.x, o.y, t.x, t.y);
            m_target = value;
        } 
    }

    private Vector2 m_target;
    private IEnumerable<DSA.GridLocation> m_nodes;
}
