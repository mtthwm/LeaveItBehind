using UnityEngine;

/// <summary>
/// Class containing Methods to move other rigidbodies
/// 
/// Assumes all speeds are adjusted for fixedDeltaTime -- USE IN FIXED UPDATE
/// **IMPORTANT** MovePosition = KINEMATIC RIGIDBODY!!!
/// If Rigidbody not kinemetic change to rb.velocity = Vector2.___ * speed;
/// 
/// 1/21 - Switched to velocity based to move with collisions
/// </summary>
public class MovementController : MonoBehaviour
{
    public static void Move_Left(Rigidbody2D rb, float speed)
    {
        rb.velocity = (Vector2.left * speed);
    }

    public static void Move_Right(Rigidbody2D rb, float speed)
    {
        rb.velocity= (Vector2.right * speed);
    }

    public static void Move_Up(Rigidbody2D rb, float speed)
    {
        rb.velocity = (Vector2.up * speed);
    }

    public static void Move_Down(Rigidbody2D rb, float speed)
    {
        rb.velocity = (Vector2.down * speed);
    }
}
