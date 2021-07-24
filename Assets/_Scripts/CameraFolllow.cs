using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolllow : MonoBehaviour
{
    Camera cam;
    [SerializeField]  Transform player;
    Vector3 playerPos;
    Vector3 camPos;
    private float xoffset = 3.5f;
    private float yoffset = 0.5f;
    private float Zpos = -7f;
    private bool shaking = false;
    private float defaultCameraSize = 5.5f;
    public float shakeDuration = 0.1f;
    private float shakecounter = 0f;
    public float shakeAmount = 0.1f;
    public float decreaseFactor = 1.0f;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        cam.orthographicSize = defaultCameraSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        }
        playerPos = player.position;
        camPos = playerPos;
        camPos.z = Zpos;
        camPos.x = camPos.x + xoffset;
        camPos.y = camPos.y + yoffset;
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
