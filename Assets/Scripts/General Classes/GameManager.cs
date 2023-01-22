using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float detection_radius = 20f;

    [SerializeField]
    private AudioClip[] heart_beat;

    [SerializeField]
    private Tilemap tileMap;

    private bool chase_triggered;

    private Vector3Int WinPoint;

    // Start is called before the first frame update
    void Start()
    {
        WinPoint = tileMap.WorldToCell(Treasure.instance.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Distance_Min_Player();
        if (distance < detection_radius)
        {
            // trigger minotaur chase
            chase_triggered = true;

            if (distance < detection_radius/2)
            {
                Minotaur.instance.GetComponent<AudioSource>().clip = heart_beat[1];
            }
            else
            {
                Minotaur.instance.GetComponent<AudioSource>().clip = heart_beat[0];
            }

            Minotaur.instance.Player_Near();
        } 
        else if (chase_triggered)
        {
            // should only play if chase was triggered prior
            chase_triggered = false;

            Minotaur.instance.Player_Far();
        }


    }

    private float Distance_Min_Player()
    {
        return Vector2.Distance(Player.player.transform.position, Minotaur.instance.transform.position);
    }
}
