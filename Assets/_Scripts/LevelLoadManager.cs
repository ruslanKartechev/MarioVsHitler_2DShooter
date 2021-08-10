using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoadManager : MonoBehaviour
{
    public EventsManager eventsHandle;

    void Awake()
    {
        if (eventsHandle == null) eventsHandle = FindObjectOfType<EventsManager>();
        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadLevel_1()
    {
        eventsHandle.StartGame.Invoke();
        LoadScene("Level_1");
        CursorScript.SetGameCursor();
    }
    public void LoadNextLevel()
    {
        eventsHandle.StartGame.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReloadLevel()
    {
        eventsHandle.StartGame.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void LoadScene(string name)
    {
        eventsHandle.StartGame.Invoke();
        if (SceneManager.GetSceneByName(name).isLoaded)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
        }
        else
        {
            SceneManager.LoadScene(name);
        }
    }

}
