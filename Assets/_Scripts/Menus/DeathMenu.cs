using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class DeathMenu: MonoBehaviour
{

    public LevelLoadManager levelmanagereHandle;
    public MenuManager menuManagerHandle;
    public PlayerSpawn playerSpawnHandle;
    [SerializeField] private Menu deathMenu = new Menu();
    private TextMeshProUGUI livesLeft;

    private void Awake()
    {
        if (menuManagerHandle == null)
            menuManagerHandle = GetComponent<MenuManager>();
        SetMenuElements();
        deathMenu.m_Canvas.gameObject.SetActive(false);
       
    }
    private void SetMenuElements()
    {
        foreach(MenuElement el in deathMenu.m_elements)
        {
            switch (el.name)
            {
                case "Respawn":
                    el.element.GetComponent<Button>().onClick.AddListener(Respawn);
                    break;
                case "Restart":
                    el.element.GetComponent<Button>().onClick.AddListener(RestartLevel);
                    break;
                case "Exit":
                    el.element.GetComponent<Button>().onClick.AddListener( QuitGame );
                    break;
                case "MainMenu":
                    el.element.GetComponent<Button>().onClick.AddListener(MainMenu);
                    break;
                case "RespawnCountText":
                     livesLeft = el.element.GetComponent<TextMeshProUGUI>();
                    break;
            }
        }
    }
    public void ShowDeathMenu()
    {
        deathMenu.ShowMenu(true);
        if(livesLeft != null)
            livesLeft.text = "Respawns left: "  + playerSpawnHandle.respawnsCount;
    }

    public void Respawn()
    {
        if(playerSpawnHandle.respawnsCount > 0)
        {
            GameManager.Instance.eventsManager.PlayerRespawn.Invoke();
            deathMenu.ShowMenu(false);  
        }
        else
            return;
    }
    public void RestartLevel()
    {
        deathMenu.m_Canvas.gameObject.SetActive(false);
        levelmanagereHandle.ReloadLevel();
    }
    public void MainMenu()
    {
        deathMenu.m_Canvas.gameObject.SetActive(false);
        levelmanagereHandle.LoadMainMenu();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
