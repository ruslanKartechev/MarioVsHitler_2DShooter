using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatDisplay : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    public void SetParameter(float value)
    {
        if(slider != null)
        {
            if(value > 1)
            {
                value = 1;
            } 
            slider.value = value;
        }
    }
}
