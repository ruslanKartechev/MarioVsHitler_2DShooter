using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerSpawn : MonoBehaviour
{
    private Transform player;
    public int maxRespawns = 3;
    public int respawnsCount { get; set; }
    public EventsManager eventsHandle;

    private void Awake()
    {
        player = GameManager.Instance.Player;
        eventsHandle = GameManager.Instance.eventsManager;
        eventsHandle.StartLevel.AddListener(() => OnLevelStart());
        eventsHandle.PlayerSpawn.AddListener(() => OnSpawnPlayer());
        eventsHandle.PlayerRespawn.AddListener(() => OnRespawnPlayer());
        respawnsCount = maxRespawns;
    }

    public IEnumerator OnLevelStart()
    {
        while (SceneManager.GetActiveScene().name == "MainMenu")
        {
            yield return null;
        }
        
    }
    private void OnSpawnPlayer()
    {
        player.gameObject.SetActive(true);
        respawnsCount -= 1;
        PlacePlayer();

    }

    private void OnRespawnPlayer()
    {
        if (player.gameObject.activeInHierarchy == false)
            player.gameObject.SetActive(true);
        PlacePlayer();
    }
    private void PlacePlayer()
    {
        GameObject spawnP = GameObject.FindGameObjectWithTag("SpawnPoint");
        if (spawnP == null)
        {
            Debug.LogError("No Spawn Point on level");
        }
        player.transform.position = spawnP.transform.position;
    }

}
