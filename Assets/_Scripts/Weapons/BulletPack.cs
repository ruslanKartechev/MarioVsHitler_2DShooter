using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPack : MonoBehaviour
{
    public WeaponInfo weaponInf;
    private int low = 0;
    private int up = 0;
    private int index;

    private float ceeling;
    private float floor;
    public float offset = 0.2f;
    private bool goUp = true;
    public float bouncingSpeed = 0.5f;
    private float phase = 0;
    private Vector3 unitY = new Vector3(0, 1, 0);

    void Start()
    {
        weaponInf = FindObjectOfType<WeaponInfo>();


        if (GetComponent<BoxCollider2D>() != null)
        {
            offset = (float)GetComponent<BoxCollider2D>().size.y;
        }
        ceeling = transform.position.y + offset;
        floor = transform.position.y - offset;
        phase = UnityEngine.Random.Range(-offset, offset);
        transform.position += unitY * phase;


        for (int i=0; i < weaponInf._BulletPackList.Length; i++)
        {
            
            if (weaponInf._BulletPackList[i].BulletPackPreFab.tag == gameObject.tag)
            {
                
                low = weaponInf._BulletPackList[i].lower_amount;
                up = weaponInf._BulletPackList[i].upper_amount;
                index = i;
            }
        }
    }
    public void Update()
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
    }

    // Update is called once per frame
    public void GiveAmmo()
    {
        int ammo = UnityEngine.Random.Range(low, up);
        SoundManager.PlaySound("ammoPickUp", ref AudioSources.pickups);
        for(int i =0; i < weaponInf._WeaponList.Length; i++)
        {
            if (weaponInf._WeaponList[i].name == weaponInf._BulletPackList[index].ForWeapon_Name)
            {
               
                weaponInf._WeaponList[i].currentAmmo += ammo;
            }
        }
     

    }
}
