using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{

    public Rigidbody rbPlayer;
    public WeaponInfo weaponInf;
    public GameObject weaponPosition;
    public AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        WeaponInfo.Refresh();
        weaponInf._WeaponList[6].equipped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(WeaponInfo.CurrentWeapon == null)
        {
            WeaponInfo.CurrentWeapon = weaponInf._WeaponList[6];
        }
    }

  public  void  SwitchForward()
    {
        int next;
            next = WeaponInfo.CurrentWeapon.index;
        
        for (int i= next+1; i < weaponInf._WeaponList.Length; i++)
        {
            if (weaponInf._WeaponList[i].equipped)
            {
                next = i;
                break;
            }
        }
        if(next != 6)
        {    
            WeaponInfo.CurrentWeapon = weaponInf._WeaponList[next];
            DestroyCurrentWeapons();
            Instantiate(WeaponInfo.CurrentWeapon.PreFab, weaponPosition.transform.position, weaponPosition.transform.rotation, weaponPosition.transform);
        }
       
    }

    public void SwitchBackwards()
    {
        int next;
        next = WeaponInfo.CurrentWeapon.index;

        for (int i = next-1; i >=0; i--)
        {
            if (weaponInf._WeaponList[i].equipped)
            {
                next = i;
                break;
            }
        }
        if (next != 6)
            {
            WeaponInfo.CurrentWeapon = weaponInf._WeaponList[next];
            DestroyCurrentWeapons();
            Instantiate(WeaponInfo.CurrentWeapon.PreFab, weaponPosition.transform.position, weaponPosition.transform.rotation, weaponPosition.transform);
        }


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Weapon") && other.gameObject.transform.position != weaponPosition.transform.position)
        {
                Destroy(other.gameObject);
                PickUpWeapon(other.gameObject.tag.ToString()); 
        }
        if(other.gameObject.tag.Contains("BulletPack"))
        {
            Destroy(other.gameObject);
            BulletPack ex = other.GetComponent<BulletPack>();
            ex.GiveAmmo();
            SoundManager s_manager = FindObjectOfType<SoundManager>();

            if (aud != null && FindObjectOfType<SoundManager>()!=null)
            {
                SoundManager.PlaySound("AmmoPickUp", ref aud);
            }
          


        }

    }

    public void PickUpWeapon(string name)
    {
        
        bool foundInList = false;
        DestroyCurrentWeapons();
        foreach (WeaponInfo.Weapon s in weaponInf._WeaponList)
        {
            if(name == s.name)
            {
              
                foundInList = true;
                WeaponInfo.CurrentWeapon = weaponInf._WeaponList[s.index];
                WeaponInfo.CurrentWeapon.equipped = true;
                Instantiate(WeaponInfo.CurrentWeapon.PreFab, weaponPosition.transform.position, weaponPosition.transform.rotation, weaponPosition.transform);
            }
        }

        if (WeaponInfo.CurrentWeapon != null)
        {
            WeaponInfo.CurrentWeapon.currentAmmo += WeaponInfo.CurrentWeapon.baseAmmo;
        }
        if(foundInList == false)
        {
            return;
        }
  
    }



    public void InstantiateCurrentWeapon()
    {
        if (WeaponInfo.CurrentWeapon != null)
        {
            Instantiate(WeaponInfo.CurrentWeapon.PreFab, weaponPosition.transform.position, weaponPosition.transform.rotation, weaponPosition.transform);
        }
           
    }



    public void DestroyCurrentWeapons()
    {
        if ( weaponPosition.transform.childCount !=0)
        {
            for (int j = 0; j < weaponPosition.transform.childCount; j++)
            {
                if (weaponPosition.transform.GetChild(j).gameObject.layer == LayerMask.NameToLayer("Weapon"))
                {
                    Destroy(weaponPosition.transform.GetChild(j).gameObject);
                }
            }

        }
    }

}
