using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;

    public Slider slide;
    public Image run_Image;

    int max_yarn;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        //while (Player.player == null) { }

        max_yarn = Player.player.yarn_length;
    }

    public void Change_Yarn_Value(int current_total)
    {
        slide.value = ((float)current_total / max_yarn);
    }

    public void Run_Phase()
    {
        slide.enabled = false;

        run_Image.enabled = true;
    }
}
