using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShader : MonoBehaviour
{
    public Material material;
    public float sizeMin = 2.5f;
    public float sizeMax = 6f;
    public float posMin = -40f;
    public float posMax = 40f;
    public float speedOfChange = 10f;
    private float Size;
    private float Pos;
    private bool sizeUP = true;
    private bool posUP = true;
    void Start()
    {
        if(material == null)
        {
            Debug.LogError("Material not assigned");
            return;
        }
       // material = gameObject.GetComponent<Renderer>().material;
        Size = material.GetFloat("Voronoi_Size");
        Pos = material.GetFloat("Voronoi_Pos");
    }

    // Update is called once per frame
    void Update()
    {
        if (posUP)
        {
            Size += Time.deltaTime * speedOfChange;

        }
        else if (!posUP)
        {
            Size -= Time.deltaTime * speedOfChange;
        }

        if (Size >= posMax)
        {
            posUP = false;
        }
        else if (Size <= posMin)
        {
            posUP = true;
        }

        material.SetFloat("Voronoi_Pos", Size);


    }
}
