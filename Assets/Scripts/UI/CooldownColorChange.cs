using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownColorChange : MonoBehaviour
{
    public Image barIMG;
    public Slider bar;

	void Update ()
    {
        Color barColour;
        if (bar.value < 0.3f)
        {
            barColour = Color.red;
            barIMG.color = barColour;
        }
        else if (bar.value < 0.9f)
        {
            barColour = Color.yellow;
            barIMG.color = barColour;
        }
        else
        {
            barColour = Color.green;
            barIMG.color = barColour;
        }
        
    }
}
