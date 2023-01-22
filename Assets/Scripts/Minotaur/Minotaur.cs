using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Minotaur : MonoBehaviour
{
    public static Minotaur instance; // Singleton

    [SerializeField]
    private Vector3[] patrol_Points;

    [SerializeField]
    private AudioClip near, far;

    private Vector3 target; // target position minotaur should path find to

    private int current_Patrol_Point = 0;

    private Animator animator;
    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();

        target = (patrol_Points.Length != 0) ? patrol_Points[0] : new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Player_Near()
    {
        animator.SetBool("Chase", true);

        target = Player.player.transform.position;

        if (_source.clip != near)
        {
            _source.clip = near;
        }
        // Path find to player
        // Go_To_Target(target);
    }

    public void Player_Far()
    {
        animator.SetBool("Chase", false);

        // Player not in range, go back to last patrol_Point
        target = patrol_Points[current_Patrol_Point];

        if (_source.clip != far)
        {
            _source.clip = far;
        }
        // Go_To_Target(target); // responsible for cycling through patrol points
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
