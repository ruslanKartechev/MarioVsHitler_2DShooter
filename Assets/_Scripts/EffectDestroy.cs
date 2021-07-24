using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    public float destructionDelay = 0.5f;
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, 0.5f);
    }
}

