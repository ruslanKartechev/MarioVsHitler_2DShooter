using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSources : MonoBehaviour
{
    public static AudioSource pickups;
    public static AudioSource shoot;
    public static AudioSource explosion;
    public static AudioSource music;
    public static AudioSource enemySounds;

    // Start is called before the first frame update
    void Awake()
    {
        pickups = gameObject.AddComponent<AudioSource>();
        shoot = gameObject.AddComponent<AudioSource>();
        explosion = gameObject.AddComponent<AudioSource>();
        music = gameObject.AddComponent<AudioSource>();
        enemySounds = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
