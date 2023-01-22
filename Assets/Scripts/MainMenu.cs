using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    public string credits;
    public string mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Starts the game
    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    // Opens the credits
    public void OpenCredits()
    {
        SceneManager.LoadScene(credits);
    }

    // Closes the credits
    public void CloseCredits()
    {
        SceneManager.LoadScene(mainMenu);
    }

    // Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
