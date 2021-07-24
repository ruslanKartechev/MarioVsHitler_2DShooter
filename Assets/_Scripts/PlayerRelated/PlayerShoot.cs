using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Vector3 shootDir;
    public Vector3 WorldPos;
    public float cameraZpos = 0;
    public Transform weaponPoint;
    public Transform weaponHoldingPosition;
    public Transform grenadePoint;
    public float angle;
    private Transform firePoint; 
    private float timeToShoot = 0;
    public float throwForce = 200f;
    // Start is called before the first frame update
    void Start()
    {
        firePoint = weaponPoint;
        if(grenadePoint == null)
        {
            grenadePoint = GameObject.FindGameObjectWithTag("GrenadePoint").transform;
        }
    }



    public void Aim(Vector2 ScreenPos)
    {
        if (Camera.main == null)
            return;
        WorldPos = Camera.main.ScreenToWorldPoint(ScreenPos);
        WorldPos.z -= Camera.main.transform.position.z;
        shootDir = WorldPos - weaponPoint.position;
        angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;

        if (PlayerMove.FacingRight == false)
        {
            shootDir.x *= -1;
        }
        if (angle >= -90 && angle <= 90)
        {
            weaponPoint.eulerAngles = new Vector3(0f, weaponPoint.eulerAngles.y, angle);
        }
        else
        {
            return;
        }

    }





    public bool Shoot()
    { 
        bool fired = false;
        if (WeaponInfo.CurrentWeapon != null && WeaponInfo.CurrentWeapon.currentAmmo > 0 && WeaponInfo.CurrentWeapon.name.Contains("Grenade") == false)
        {
            firePoint = weaponHoldingPosition.GetChild(0).GetChild(0);
            
            //if not found and not grenade
            if (firePoint == null)
            {
                Debug.LogError("FirePoint not found for the Weapon");
            } else //if found firepoint -- shoot
            {
                GameObject b = Instantiate(WeaponInfo.CurrentWeapon.bullet, firePoint.position, weaponPoint.transform.rotation);
                Bullet bh = b.GetComponent<Bullet>();
                bh.GetDir(shootDir);
                bh.GetDam(WeaponInfo.CurrentWeapon.damage);
                WeaponInfo.CurrentWeapon.currentAmmo -= 1;
            }

            fired = true;
        }
        else
        {
            return false;
        }

        return fired;
    }




    public void ShootingEffectPlay(GameObject effect)
    {
        if(WeaponInfo.CurrentWeapon.PreFab == null || WeaponInfo.CurrentWeapon.PreFab.tag.Contains("Grenade"))
        {
            return;
        }
        ShootingLight.ShowFireLight();
        Destroy(Instantiate(effect, firePoint.position, weaponPoint.transform.rotation, null), 0.2f);
        

    }


   public void throwGrenade()
    {
        if (WeaponInfo.CurrentWeapon.currentAmmo > 0)
        {

            Grenade g = GetComponentInChildren<Grenade>();
            if (g == null)
                return;
            g.gameObject.transform.position = grenadePoint.position;
            g.throwG(shootDir, throwForce);
            WeaponInfo.CurrentWeapon.currentAmmo -= 1;
            if (WeaponInfo.CurrentWeapon.currentAmmo > 0)
            {
                Instantiate(WeaponInfo.CurrentWeapon.PreFab, weaponPoint.transform.position , weaponPoint.transform.rotation, weaponPoint.transform);
            }
            else
            {
                WeaponInfo.CurrentWeapon = null;
            }
        }

    }





}
