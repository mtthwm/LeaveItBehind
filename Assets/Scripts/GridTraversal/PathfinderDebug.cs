using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(Pathfinder))]
public class PathfinderDebug : MonoBehaviour
{
    [SerializeField] private Transform target;

    private LineRenderer m_lineRenderer;
    private Pathfinder m_pathfinder;

    private void Start()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_pathfinder = GetComponent<Pathfinder>();
    }

    private void FixedUpdate()
    {
        m_pathfinder.Target = target.position;
        m_pathfinder.UpdatePath();
        UpdateLine();
    }

    private void UpdateLine()
    {
        List<Vector3> positions = m_pathfinder.Path;
        m_lineRenderer.positionCount = positions.Count;
        m_lineRenderer.SetPositions(positions.ToArray());
    }
}
