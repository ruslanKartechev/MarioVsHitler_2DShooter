using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenu;
    public Transform SpawnPoint;
    public GameObject Player;
    private static DeathMenu S;

    private void Awake()
    {
        if(deathMenu == null)
        {
            Debug.LogError("Death Menu object not found");
        }


        if (S == null)
            S = this;

        if(SpawnPoint == null)
        {
            SpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        }

        if (Player == null)
        {
            Player = FindObjectOfType<PlayerControl>().gameObject;
        }
        PlayerControl.DisableInGameInput();
    }



    public void Update()
    {
        if(gameObject.activeInHierarchy == true)
        {
            PlayerControl.DisableInGameInput(); ;
        }
           
    }

    public void Respawn()
    {
        if (PlayerControl.NumberOfLifes > 0)
        {
            SpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
            if (SpawnPoint != null)
            {
                Player.transform.position = SpawnPoint.position;
            }
            else
            {
                Player.transform.position = new Vector3(0, 0, 0);
            }

            Time.timeScale = 1f;
            deathMenu.SetActive(false);
            PlayerControl.Respawn();
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        deathMenu.SetActive(false);
        PlayerControl.Restart();
        
       
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
        PlayerControl.EnableInGameInput();

    }
}
