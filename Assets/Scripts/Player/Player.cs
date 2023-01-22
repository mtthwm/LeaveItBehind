using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    static public Player player; // Singleton

    [SerializeField]
    [Range(10f, 50f)]
    private float speed = 10f;

    public int yarn_length = 100;

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
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (yarn_length == 0 )
            return;

        float move_Speed = speed * Time.fixedDeltaTime;
        Quaternion rotation = new Quaternion();
        switch (dir) 
        {
            case 1:
                rotation.eulerAngles = new Vector3(0, 0, 90);

                MovementController.Move_Right(rb, move_Speed);
                break;
            case 2:
                rotation.eulerAngles = new Vector3(0, 0, -90);

                MovementController.Move_Left(rb, move_Speed);
                break;
            case 3:
                rotation.eulerAngles = new Vector3(0, 0, 180);

                MovementController.Move_Up(rb, move_Speed);
                break;
            default:
                rotation.eulerAngles = Vector3.zero;
                MovementController.Move_Down(rb, move_Speed);
                break;
        }

        transform.rotation = rotation;

        // if there is more yarn to place, place it
        if (!GameManager.GM.player_Has_Treasure)
        {
            Vector3Int cell = tileMap.WorldToCell(transform.position);
            RopeSpawner.spawner.SpawnRope(cell, transform.rotation);
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
}
