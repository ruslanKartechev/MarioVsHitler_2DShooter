using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextControl : MonoBehaviour
{
    public  TextMeshProUGUI textOutput;
    public static float counter=0;
    public static string text;
    void Start()
    {
        text = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(textOutput == null)
        {
            return;
        }
        if (counter > 0)
        {
            textOutput.text = text;
            counter -= Time.deltaTime;

        } else
        {
            textOutput.text = null;
        }
    }

    public static void PrintText(string textToPrint, float timeToShow)
    {
        
            //Debug.LogWarning("showing");
            counter = timeToShow;
            text = textToPrint;
        
        
        


    }
}
