using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    static public Player player; // Singleton

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private Tilemap tileMap;

    private Rigidbody2D rb;

    private int dir = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = this;

        //tileMap.WorldToCell
    }

    private void FixedUpdate()
    {
        if (dir == 0)
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
            Vector2 move_Dir = context.ReadValue<Vector2>();
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
