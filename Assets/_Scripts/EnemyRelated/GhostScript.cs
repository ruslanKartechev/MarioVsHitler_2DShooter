using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Material))]
[RequireComponent(typeof(BoxCollider2D))]
public class GhostScript : MonoBehaviour
{
    private float ceeling;
    private float floor;
    public float offset = 0;
    private bool goUp = true;
    public float bouncingSpeed = 0.5f;
    private float phase = 0;
    private Vector3 unitY = new Vector3(0, 1, 0);
    private Color violet;
    private Color red;
    private Material mat;
    private float fadeChangeSpeed = 0.4f;
    private float fadeUpBound = 0.3f;
    private float fadeLowBound = -0.3f;
    private bool increase = true;
    // Start is called before the first frame update
    void Start()
    {
        violet.r = 0; violet.b = 191; violet.g = 0;
        red.r = 245; red.b = 0; red.g = 13;
         mat = GetComponent<Renderer>().material;
        if (GetComponent<BoxCollider2D>() != null)
        {
            offset = (float)GetComponent<BoxCollider2D>().size.y;
        }

        if(gameObject.name.Contains("Red"))
            mat.color = violet;
        else if(gameObject.name.Contains("Blue"))
            mat.color = red;
        mat.SetFloat("fade", 0f);

        ceeling = transform.position.y + offset;
        floor = transform.position.y - offset;
        phase = UnityEngine.Random.Range(-offset, offset);
        transform.position += unitY * phase;
    }

    // Update is called once per frame
    void Update()
    {
        if (goUp)
        {
            transform.position += unitY * bouncingSpeed * Time.deltaTime;

        }
        else if (!goUp)
        {
            transform.position -= unitY * bouncingSpeed * Time.deltaTime;
        }

        if (transform.position.y >= ceeling)
        {
            goUp = false;
        }
        else if (transform.position.y <= floor)
        {
            goUp = true;
        }

  

        if(mat.GetFloat("fade") >= fadeUpBound)
        {
            increase = false;
        } else if(mat.GetFloat("fade") <= fadeLowBound)
        {
            increase = true;
        }

        if (increase==true)
        {
            mat.SetFloat("fade", mat.GetFloat("fade") + Time.deltaTime * fadeChangeSpeed);

        } else if (increase==false)
        {
            mat.SetFloat("fade", mat.GetFloat("fade") - Time.deltaTime * fadeChangeSpeed);
        }
    
    }


}
 
