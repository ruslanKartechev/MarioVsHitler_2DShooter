using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MenuElement
{
    public GameObject element;
    public string name;
}
[System.Serializable]
public class Menu
{
     public Canvas m_Canvas;
     public GameObject m_menuParent;
     public List<MenuElement> m_elements;
    public void ShowMenu(bool on)
    {
        if(on == true)
        {
            if (m_Canvas.isActiveAndEnabled == false)
                m_Canvas.gameObject.SetActive(true);
            if(m_menuParent != null)
                m_menuParent.SetActive(true);
        } else if(on == false)
        {
            if (m_Canvas != null && m_Canvas.gameObject.activeInHierarchy == true)
                m_Canvas.gameObject.SetActive(false);
        }
    }
    protected MenuElement GetGutton(string name)
    {
        return m_elements.Find( x => x.name == name);
    }
    protected void ShowButton(string name)
    {
        GetGutton(name).element.SetActive(false);
    }
}

public class MenuManager : MonoBehaviour
{
    public GameObject inGameUI;

    public DeathMenu deathMenuHandle;
    public PauseMenu pauseMenuHandle;
    public LevelEndMenu levelExitHandle;
    public float defaultTimeScale = 1f;
    private void Awake()
    {
        GameManager.Instance.eventsManager.PlayerDie.AddListener(() => OnPlayerDie());
        GameManager.Instance.eventsManager.StartLevel.AddListener(() => OnStartLevel());
        GameManager.Instance.eventsManager.PauseSet.AddListener(() => OnPause());
        GameManager.Instance.eventsManager.GameResumed.AddListener(() => ResumeGame());

        GameManager.Instance.eventsManager.PauseSet.AddListener(() => GameTimeChange(0));
        GameManager.Instance.eventsManager.PlayerDie.AddListener(() => GameTimeChange(0));
        GameManager.Instance.eventsManager.LevelEnd.AddListener(() => GameTimeChange(0));
        GameManager.Instance.eventsManager.PlayerRespawn.AddListener(() => GameTimeChange(defaultTimeScale));
        GameManager.Instance.eventsManager.PlayerSpawn.AddListener(() => GameTimeChange(defaultTimeScale));
        GameManager.Instance.eventsManager.GameResumed.AddListener(() => GameTimeChange(defaultTimeScale)) ;
        
    }
    public void OnStartLevel()
    {
        ResumeGame();
    }
    public void OnPause()
    {
        if (pauseMenuHandle._isPaused == false)
        {
            inGameUI.SetActive(false);
            pauseMenuHandle.SetPause();
        }
        else
        {
            ResumeGame();
        }
    }
    public void ResumeGame()
    {
        if(pauseMenuHandle._isPaused)
            pauseMenuHandle.Resume();
        inGameUI.SetActive(true);
    }
    
    public void OnPlayerDie()
    {
        deathMenuHandle.ShowDeathMenu();
    }

    public void GameTimeChange(float rate)
    {
        Time.timeScale = rate;
    }

}
