using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private string EndScene = "EndScene";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.GM.player_Has_Treasure && collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(EndScene);
        }
    }
}
