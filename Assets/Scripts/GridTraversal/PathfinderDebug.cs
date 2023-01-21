using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(Pathfinder))]
public class PathfinderDebug : MonoBehaviour
{
    [SerializeField] private Transform target;

    private LineRenderer m_lineRenderer;
    private Pathfinder m_pathfinder;


    // Start is called before the first frame update
    void Start()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_pathfinder = GetComponent<Pathfinder>();
    }

    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        m_pathfinder.Target = target.position;

        List<Vector3> newPositions = new List<Vector3>();
        foreach (DSA.GridLocation node in m_pathfinder.Nodes)
        {
            newPositions.Add(m_pathfinder.gridProvider.GridToWorld(node));
        }
        m_lineRenderer.positionCount = newPositions.Count;
        m_lineRenderer.SetPositions(newPositions.ToArray());
    }
}
