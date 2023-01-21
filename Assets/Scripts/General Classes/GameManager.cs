using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float detection_radius = 10f;

    private bool chase_triggered;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Distance_Min_Player() < detection_radius)
        {
            // trigger minotaur chase
            chase_triggered = true;

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
        return Vector3.Distance(Player.player.transform.position, Minotaur.instance.transform.position);
    }
}
