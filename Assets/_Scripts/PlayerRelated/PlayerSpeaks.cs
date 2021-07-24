using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeaks : MonoBehaviour
{
    public string[] PhrasesToSay;
    public float frequency;
    public float timeToDisplay;
    private float timeTillNext = 0;
    public float defaultTimeToDisplay = 3f;
    public static bool doRandomPhrases = true;
    private float countdown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        frequency = 5f;
        timeToDisplay = defaultTimeToDisplay;
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown > 0)
            countdown -= Time.deltaTime;
        else if (countdown <= 0)
        {
            doRandomPhrases = true;
            timeToDisplay = defaultTimeToDisplay;
        }



        if (doRandomPhrases)
        {
            float timerange = 60 / frequency;

            if (timeTillNext <= 0)
            {
                int r = UnityEngine.Random.Range(0, PhrasesToSay.Length);
                if (PhrasesToSay[r] != null)
                {
                  
                    TextControl.PrintText(PhrasesToSay[r], timeToDisplay);
                    timeTillNext = UnityEngine.Random.Range(0, timerange);
                }


            }
            timeTillNext -= Time.deltaTime;
        }

    }
    public void SpecialPharase(string say, float duration)
    {
        TextControl.PrintText(say, duration);
        countdown = duration;
    }
  

}
