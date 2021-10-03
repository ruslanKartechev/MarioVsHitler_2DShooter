using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolllow : MonoBehaviour
{
    private Camera cam;
    private Transform player;
    private Vector3 playerPos;
    private Vector3 camPos;
    private float xoffset = 3.5f;
    private float yoffset = 0.5f;
    private float Zpos = -7f;
    private bool shaking = false;
    private float defaultCameraSize = 5.5f;
    public float shakeDuration = 0.1f;
    private float shakecounter = 0f;
    public float shakeAmount = 0.1f;
    public float decreaseFactor = 1.0f;

    void Awake()
    {
        cam = Camera.main;
        cam.orthographicSize = defaultCameraSize;
        player = GameManager.Instance.Player;
        GameManager.Instance.eventsManager.StartLevel.AddListener(() => OnLevelStart());
    }

    public void OnLevelStart()
    {
        StartCoroutine(FollowPlayer());
    }
    private IEnumerator FollowPlayer()
    {
        while (player.gameObject.activeInHierarchy == false)
        {
            yield return null;
        }
        while (true)
        {
            playerPos = player.position;
            camPos = playerPos;
            camPos.z = Zpos;
            camPos.x += +xoffset;
            camPos.y += +yoffset;
            if (shaking == false)
            {
                MoveTo(camPos);
            }

            if (shakecounter > 0)
            {
                Vector3 newpos = camPos + Random.insideUnitSphere * shakeAmount;
                shakecounter -= Time.deltaTime * decreaseFactor;
                MoveTo(newpos);
            }
            else
            {
                shaking = false;
                shakecounter = 0f;
            }
            yield return null;
        }
        
       
    }
 

    void MoveTo(Vector3 dest)
    {
        gameObject.transform.position = dest;

    }

    public void Shake()
    {
        shaking = true;
        shakecounter = shakeDuration;
    }
}
