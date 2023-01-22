using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public static Treasure instance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("Player"))
        {
            GameManager.GM.Player_Has_Treasure();

            Destroy(gameObject);
        }
    }
}
