using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject levelExitMenu;
    public GameObject DeathMenu;

    public PauseMenu pauseMenuHandle;
    public float defaultTimeScale = 1f;

    void Start()
    {
        
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = defaultTimeScale;
    }
    
    void Update()
    {
        
    }
}
