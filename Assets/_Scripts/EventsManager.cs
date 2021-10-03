using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventsManager : MonoBehaviour
{
    public UnityEvent StartLevel = new UnityEvent();
    public UnityEvent PlayerDie = new UnityEvent();
    public UnityEvent PauseSet = new UnityEvent();
    public UnityEvent GameResumed = new UnityEvent();
    public UnityEvent LevelEnd = new UnityEvent();
    public UnityEvent PlayerRespawn = new UnityEvent();
    public UnityEvent PlayerSpawn = new UnityEvent();
}
