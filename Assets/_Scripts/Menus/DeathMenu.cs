using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathMenu : MonoBehaviour
{
    public TextMeshProUGUI livesLeft;
    public GameObject deathMenuCanvas;
    public LevelLoadManager levelmanagereHandle;
    public MenuManager menuManagerHandle;
    public PlayerSpawn playerSpawnHandle;
    public EventsManager eventsHandle;
    private void Awake()
    {
        deathMenuCanvas.SetActive(false);

    }
    public void OnPlayerDie()
    {
        deathMenuCanvas.SetActive(true);
        livesLeft.text = "Lives left: "  + playerSpawnHandle._respawns;
    }


    public void Respawn()
    {
        if(playerSpawnHandle._respawns > 0)
        {
            eventsHandle.PlayerRespawn.Invoke();
            deathMenuCanvas.SetActive(false);
        }
        else
        {
            return;
        }
        
    }

    public void RestartLevel()
    {
        deathMenuCanvas.SetActive(false);
        levelmanagereHandle.ReloadLevel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        deathMenuCanvas.SetActive(false);
        levelmanagereHandle.LoadMainMenu();

    }
}
