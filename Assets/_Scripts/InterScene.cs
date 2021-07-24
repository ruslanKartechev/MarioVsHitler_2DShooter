using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InterScene : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject player;
    public GameObject inGameUi;
    public WeaponInfo weaponInf;
    public bool wasSpawned;
    public Scene loadedScene;
    // Start is called before the first frame update
    void Awake()
    {
        loadedScene = SceneManager.GetActiveScene();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if(inGameUi == null)
        {
            inGameUi = GameObject.FindGameObjectWithTag("InGameUI");
        }

        wasSpawned = false;
        if (SceneManager.GetActiveScene().name == "MainMenu" && player.gameObject.activeInHierarchy == true)
        {
            player.gameObject.SetActive(false);
            inGameUi.SetActive(false);
        }
        else if(SceneManager.GetActiveScene().name != "MainMenu")
        {
            inGameUi.SetActive(true);
            player.gameObject.SetActive(true);
        }
       

       
    }


    void Update()
    {
        if (SceneManager.GetActiveScene() != loadedScene)
        {
            wasSpawned = false;
            loadedScene = SceneManager.GetActiveScene();
        }
        if (SceneManager.GetActiveScene().name == "MainMenu") // if in main menu deactivate player
        {
            
            player.gameObject.SetActive(false);
            inGameUi.SetActive(false);

        }

        if (SceneManager.GetActiveScene().name != "MainMenu" ) // if NOT in main menu
        {
            SpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
            player.gameObject.SetActive(true);
          //  inGameUi.SetActive(true);
            if(wasSpawned == false)
            {
                SpawnPlayer();
            }

        } 
        
      
    }

    public void SpawnPlayer()
    {
        if(SceneManager.GetActiveScene().name == "Level_1")
        {
            WeaponInfo.Refresh();
        }

        player.transform.position = SpawnPoint.position;
        wasSpawned = true;
    }

}
