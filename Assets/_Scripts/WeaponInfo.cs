using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class WeaponInfo: MonoBehaviour 
{

    [System.Serializable]
    public class Weapon
    {
        public GameObject PreFab;
        public string name;
        public string NameToDisplay;
        public int index;
        public int damage;
        public bool equipped;
        public float fireRate;
        public int accessKey;
        public int baseAmmo;
        public int currentAmmo;
        public GameObject bullet;
        public Weapon()
        {
            PreFab = null;
            name = null;
            damage = 0;
            equipped = false;
            fireRate = 0;
            accessKey = 0;
            baseAmmo = 0;
            currentAmmo = 0;
            index = 0;
            NameToDisplay = name;
        }


    }
    private static WeaponInfo S;
    public static Weapon CurrentWeapon;
    public  Weapon[] _WeaponList;


    [System.Serializable]
    public class BulletPack
    {
        public GameObject BulletPackPreFab;
        public string name;
        public int lower_amount;
        public int upper_amount;
        public string ForWeapon_Name;
    }
    public BulletPack[] _BulletPackList;




    private void Awake()
    {
        if(S == null)
        {
            S = this;
        }
        for(int i=0; i<S._WeaponList.Length; i++)
        {
            if(S._WeaponList[i].PreFab == null)
            {
                S._WeaponList[i].name = "Nothing";
            }
            else
            {
                S._WeaponList[i].name = S._WeaponList[i].PreFab.tag;
            }
         
            if(S._WeaponList[i].NameToDisplay == null)
            {
                S._WeaponList[i].NameToDisplay = S._WeaponList[i].name;
            }
            S._WeaponList[i].index = i;
            S._WeaponList[i].currentAmmo = 0;        
        }
        for (int i = 0; i < S._BulletPackList.Length; i++)
        {
            S._BulletPackList[i].name = S._BulletPackList[i].BulletPackPreFab.tag;
        }

    }

    public static void Refresh()
    {
        for (int i = 0; i < S._WeaponList.Length; i++)
        {
            if(S._WeaponList[i].PreFab != null)
                S._WeaponList[i].name = S._WeaponList[i].PreFab.tag;

            if (S._WeaponList[i].NameToDisplay == null)
            {
                S._WeaponList[i].NameToDisplay = S._WeaponList[i].name;
            }

            S._WeaponList[i].index = i;
            S._WeaponList[i].currentAmmo = 0;
            S._WeaponList[i].equipped = false;
        }

    }


}
