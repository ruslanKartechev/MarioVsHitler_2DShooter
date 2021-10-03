using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
            }

            return instance;
        }
    }
}
public class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        if (instance == null) instance = this;
    }
     public EventsManager eventsManager;
     public SoundManager soundManager;
     public MenuManager menuManager;
     public CursorScript cursorScript;
     public Transform Player;

}
