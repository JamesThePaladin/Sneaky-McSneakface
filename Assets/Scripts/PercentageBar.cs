using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Use the UI code

public class PercentageBar : MonoBehaviour
{
    public float max; //Public variable for max
    public float current; //Public variable for current
    public Image guiBar;//Public variable for our image component

    // Use this for initialization
    void Start()
    {
        //Make sure the image component is set to filled
        guiBar.type = Image.Type.Filled;
    }

    // Update is called once per frame
    void Update()
    {
        //Find what percentage of max our current value is
        float percentOfMax = current / max;
        //Set our image percentage to that same percent
        guiBar.fillAmount = percentOfMax;
    }
}
