using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class ShootingLight : MonoBehaviour
{
    public Light2D eff;
    private static ShootingLight S;
    public Color defaultColor = Color.white;
    public float defaultIntensity = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        eff = gameObject.GetComponent<Light2D>();
        eff.intensity = 0;
    }

    public static void ShowFireLight()
    {
        S.eff.intensity = 2;
        S.eff.color = Color.red;
        S.Invoke("StopLight", 0.2f);
    }


    public void StopLight()
    {
        eff.intensity = defaultIntensity;
        eff.color = defaultColor;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
