using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;

    public Slider slide;
    public Image run_Image;
    public Image otherThing;

    public int max_yarn = 120;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void Change_Yarn_Value(int current_total)
    {
        slide.value = ((float)current_total / max_yarn);
    }

    public void Run_Phase()
    {
        slide.gameObject.SetActive(false);
        otherThing.gameObject.SetActive(false);
        run_Image.gameObject.SetActive(true);
    }
}
