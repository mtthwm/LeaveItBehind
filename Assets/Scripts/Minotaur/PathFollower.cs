using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Pathfinder), typeof(Rigidbody2D))]
public class PathFollower : MonoBehaviour
{
    public delegate void ArrivedAction (GameObject originator, Transform transform);
    public static event ArrivedAction OnArrived;

    public Transform Target;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float updateTargetDelay = 1f;

    private Pathfinder m_pathfinder;
    private Rigidbody2D m_rb2d;
    private bool m_following;
    private bool m_hasPath;
    private int m_pathIndex;
    private List<Vector3> m_path;
    private Vector3 m_prevTarget;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_pathfinder = GetComponent<Pathfinder>();

        StartFollowing();
    }

    private void FixedUpdate()
    {
        if (m_following && m_hasPath)
        {
            if (Vector2.Distance(transform.position, m_path[m_pathIndex]) > 0.1f)
            {
                Vector3 dir = (m_path[m_pathIndex] - transform.position).normalized;
                Quaternion rotation = new Quaternion();
                if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y) && dir.y > 0)
                {
                    MovementController.Move_Up(m_rb2d, speed);
                    rotation.eulerAngles = new Vector3(0, 0, 180);
                }
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y) && dir.x > 0)
                {
                    MovementController.Move_Right(m_rb2d, speed);
                    rotation.eulerAngles = new Vector3(0, 0, 90);
                }
                if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y) && dir.y < 0)
                {
                    MovementController.Move_Down(m_rb2d, speed);
                    rotation.eulerAngles = Vector3.zero;
                }
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y) && dir.x < 0)
                {
                    MovementController.Move_Left(m_rb2d, speed);
                    rotation.eulerAngles = new Vector3(0, 0, -90);
                }
                transform.rotation = rotation;
            }
            else
            {
                m_pathIndex++;
                if (m_pathIndex >= m_path.Count)
                {
                    m_hasPath = false;
                    m_rb2d.velocity = Vector2.zero;
                    OnArrived?.Invoke(gameObject, Target);
                    return;
                }
            }
        }
    }

    public void StartFollowing()
    {
        m_following = true;
        UpdateTarget();

        StartCoroutine(UpdateTargetCoroutine());
    }

    public void UpdateTarget()
    {
        m_pathfinder.Target = Target.position;
        m_prevTarget = Target.position;
        m_pathfinder.UpdatePath();
        m_path = m_pathfinder.Path;
        if (m_path.Count < 2)
        {
            Stop();
            return;
        }
        m_pathIndex = 0;
        m_hasPath = true;
    }

    public void Stop()
    {
        m_following = false;
        m_hasPath = false;
        m_rb2d.velocity = Vector2.zero;
    }

    private IEnumerator UpdateTargetCoroutine ()
    {
        while (m_following)
        {
            yield return new WaitForSeconds(updateTargetDelay);
            if (Vector2.Distance(Target.position, m_prevTarget) > 0.1f)
            {
                UpdateTarget();
            }
        }
    }
}
