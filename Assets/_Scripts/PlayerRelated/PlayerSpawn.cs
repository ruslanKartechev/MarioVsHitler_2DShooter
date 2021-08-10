using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerSpawn : MonoBehaviour
{
    public GameObject player;
    public int maxRespawns = 3;
    private int respawns;
    public EventsManager eventsHandle;
    public int _respawns
    {
        get { return respawns; }
    }

    private void Awake()
    {
        if (eventsHandle == null) eventsHandle = FindObjectOfType<EventsManager>();
        eventsHandle.StartGame.AddListener(() => OnGameStart());
        respawns = maxRespawns;
    }

    public void OnGameStart()
    {

    }
    private void SpawnPlayer()
    {
        player.SetActive(true);
        player.transform.position = FindSpawnPoint().position;
    }

    private void RespawnPlayer()
    {
        eventsHandle.PlayerRespawn.Invoke();
        player.transform.position = FindSpawnPoint().position;
    }
    private Transform FindSpawnPoint()
    {
       Transform SpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        return SpawnPoint;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
