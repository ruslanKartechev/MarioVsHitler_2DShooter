using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject inGameUI;

    public DeathMenu deathMenuHandle;
    public PauseMenu pauseMenuHandle;
    public float defaultTimeScale = 1f;
    public EventsManager eventsHandle;


    private void Awake()
    {
        if (eventsHandle == null) eventsHandle = FindObjectOfType<EventsManager>();
        eventsHandle.PlayerDie.AddListener(() => OnPlayerDie());
        eventsHandle.PlayerSpawn.AddListener(() => OnRespawn());
        eventsHandle.StartGame.AddListener(() => OnStartGame());
    }



    public void OnStartGame()
    {
        inGameUI.SetActive(true);
    }
    public void PauseGame()
    {
        if (pauseMenuHandle._isPaused == false)
        {
            inGameUI.SetActive(false);
            pauseMenuHandle.SetPause();
            Time.timeScale = 0f;
        }
        else
        {
            ResumeGame();
        }
           
    }
    public void ResumeGame()
    {
        inGameUI.SetActive(true);
        pauseMenuHandle.Resume();
        Time.timeScale = defaultTimeScale;
    }
    
    public void OnPlayerDie()
    {
        deathMenuHandle.OnPlayerDie();
        Time.timeScale = 0f;
    }

    public void OnRespawn()
    {
        inGameUI.SetActive(true);

    }

}
