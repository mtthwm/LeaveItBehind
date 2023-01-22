using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public bool player_Has_Treasure;

    [SerializeField]
    private float detection_radius = 10f;

    [SerializeField]
    private Tilemap tileMap;

    private bool chase_triggered;

    // Start is called before the first frame update
    void Start()
    {
        GM = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (player_Has_Treasure)
            return;

        float distance = Distance_Min_Player();
        if (distance < detection_radius)
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
        return Vector2.Distance(Player.player.transform.position, Minotaur.instance.transform.position);
    }

    public void Player_Has_Treasure()
    {
        player_Has_Treasure = true;

        Player.player.yarn_length += 1; // ensures that yarn length is never 0

        Minotaur.instance.Player_Near();
    }

    public void Player_Caught()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void Player_Won()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
