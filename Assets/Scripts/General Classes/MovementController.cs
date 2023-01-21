using UnityEngine;

/// <summary>
/// Class containing Methods to move other rigidbodies
/// 
/// Assumes all speeds are adjusted for deltaTime -- USE IN FIXED UPDATE
/// </summary>
public class MovementController : MonoBehaviour
{
    public static void Move_Left(Rigidbody2D rb, float speed)
    {
        rb.MovePosition(Vector2.left * speed);
    }

    public static void Move_Right(Rigidbody2D rb, float speed)
    {
        rb.MovePosition(Vector2.right * speed);
    }

    public static void Move_Up(Rigidbody2D rb, float speed)
    {
        rb.MovePosition(Vector2.up * speed);
    }

    public static void Move_Down(Rigidbody2D rb, float speed)
    {
        rb.MovePosition(Vector2.down * speed);
    }
}
