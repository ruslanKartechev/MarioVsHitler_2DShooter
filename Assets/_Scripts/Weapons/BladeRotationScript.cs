using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeRotationScript : MonoBehaviour
{
    public float Maxangle = 10f;
    public float startAngle = 0f;
    private float angularSpeed = 30f;
    private bool moveRight = true;
    private float angle = 0f;
    private Quaternion limit;
    // Start is called before the first frame update
    void Start()
    {
        Quaternion limit = Quaternion.Euler(0f,0f,Maxangle);
        gameObject.transform.rotation = Quaternion.Euler(0f,0f,0f);
        angle = gameObject.transform.rotation.z;
        Debug.LogWarning(limit.z.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if(moveRight == true)
        {
            angle += angularSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
         //   Debug.LogWarning(transform.rotation.z.ToString());
        } 
        if(transform.rotation.eulerAngles.z >= Maxangle && transform.rotation.eulerAngles.z < 90)
        {
          
            moveRight = false;
        }
        if(moveRight == false)
        {
            angle -= angularSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
           // Debug.LogWarning(transform.rotation.eulerAngles.z.ToString());
        }
        if (transform.rotation.eulerAngles.z <= (360f - Maxangle) && transform.rotation.eulerAngles.z > 90) 
        {
         
            moveRight = true;
        }
        //Debug.LogWarning(transform.rotation.z.ToString());
        
    }
}
