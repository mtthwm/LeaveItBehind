using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    static public Player player; // Singleton

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private int yarn_length = 100;

    [SerializeField]
    private Tilemap tileMap;

    private Vector3Int current_Tile;

    private Rigidbody2D rb;

    private int dir = 0;
    Vector2 move_Dir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        move_Dir = Vector2.zero;
    }

    private void Start()
    {
        player = this;

        //tileMap.WorldToCell
        current_Tile = tileMap.WorldToCell(transform.position);
    }

    private void FixedUpdate()
    {
        if (dir == 0)
            return;

        if (yarn_length == 0)
            return;

        float move_Speed = speed * Time.fixedDeltaTime;
        switch (dir) 
        {
            case 1:
                MovementController.Move_Right(rb, move_Speed);
                break;
            case 2:
                MovementController.Move_Left(rb, move_Speed);
                break;
            case 3:
                MovementController.Move_Up(rb, move_Speed);
                break;
            default:
                MovementController.Move_Down(rb, move_Speed);
                break;
        }

        // if there is more yarn to place, place it
        if (yarn_length > 0 )
        {
            Vector3Int cell = tileMap.WorldToCell(transform.position);
            if (current_Tile != cell)
            {
                current_Tile = cell;

                // attempt rope spawn
                RopeSpawner.spawner.SpawnRope(cell, move_Dir);

                yarn_length--;
            }
        }
    }

    public void Yarn_Destroyed()
    {
        yarn_length++;
    }

    public void Move(InputAction.CallbackContext context) 
    {
        // move button released
        if (context.canceled)
        {
            dir = 0;
            return;
        }

        // move button pressed
        if (context.performed)
        {
            move_Dir = context.ReadValue<Vector2>();
            if ((uint)move_Dir.x > 0)
            {
                dir = (move_Dir.x > 0) ? 1 : 2;
            }
            else
            {
                dir = (move_Dir.y > 0) ? 3 : 4;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
