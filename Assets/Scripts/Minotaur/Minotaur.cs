using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Minotaur : MonoBehaviour
{
    public static Minotaur instance; // Singleton

    [SerializeField]
    private Transform[] patrol_Points;

    [SerializeField]
    private AudioClip near, far;

    private int current_Patrol_Point = 0;

    private Animator animator;
    private AudioSource _source;
    private PathFollower _follower;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
        _follower = GetComponent<PathFollower>();

        _follower.Target = patrol_Points[0];
        _follower.StartFollowing();
    }

    private void OnEnable()
    {
        PathFollower.OnArrived += Cycle_Patrol_Point;
    }

    private void OnDisable()
    {
        PathFollower.OnArrived -= Cycle_Patrol_Point;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Cycle_Patrol_Point (GameObject originator, Transform transform)
    {
        Debug.Log("CYCLE!");
        if (originator != _follower.gameObject || transform != patrol_Points[current_Patrol_Point])
        {
            return;
        }

        current_Patrol_Point = (current_Patrol_Point + 1) % patrol_Points.Length;
        Player_Far();
    }

    public void Player_Near()
    {
        animator.SetBool("Chase", true);

        _follower.Target = Player.player.transform;

        if (_source.clip != near)
        {
            _source.clip = near;
        }
    }

    public void Player_Far()
    {
        animator.SetBool("Chase", false);

        // Player not in range, go back to last patrol_Point
        _follower.Target = patrol_Points[current_Patrol_Point];

        if (_source.clip != far)
        {
            _source.clip = far;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        // Destroy ONLY rope objects
        if (other.CompareTag("Rope"))
        {
            RopeSpawner.spawner.DestroyRope(other, other.transform.position);
            return;
        }

        if (other.CompareTag("Player"))
        {
            GameManager.GM.Player_Caught();
        }
    }
}
