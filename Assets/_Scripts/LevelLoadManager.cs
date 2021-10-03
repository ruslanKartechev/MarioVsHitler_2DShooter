using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoadManager : MonoBehaviour
{
    private EventsManager eventsHandle;
    void Awake()
    {
        eventsHandle = GameManager.Instance.eventsManager;        
    }

    public void LoadMainMenu()
    {
        Destroy(FindObjectOfType<DontDestroyOnLoad>().gameObject);
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadLevel_1()
    {
       StartCoroutine( LoadScene("Level_1") );
    }
    public void LoadNextLevel()
    {
        Scene next =  SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine( LoadScene(next.name) );
    }
    public void ReloadLevel()
    {
        eventsHandle.StartLevel.Invoke();
        StartCoroutine( LoadScene(SceneManager.GetActiveScene().name) );
    }

    private IEnumerator LoadScene(string name)
    {
        if (SceneManager.GetSceneByName(name).isLoaded)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
        }
        else
        {
            SceneManager.LoadScene(name);
        }
        yield return null;
        while (SceneManager.GetActiveScene().name != name)
        {
            yield return null;
        }
        yield return null ;
        eventsHandle.StartLevel.Invoke();
        eventsHandle.PlayerSpawn.Invoke();

    }


}
