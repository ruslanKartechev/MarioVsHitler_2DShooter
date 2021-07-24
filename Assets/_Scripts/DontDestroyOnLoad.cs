using System.Collections;
using UnityEngine;
public class DontDestroyOnLoad : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

 

}
