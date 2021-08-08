using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Vector3 shootDir;
    public Vector3 WorldPos;
    public float cameraZpos = 0;
    public float throwForce = 200f;
    public float bulletSpeed = 50f;
    public Transform weaponPoint;
    public Transform weaponHoldingPosition;
    public Transform grenadePoint;

    public GameObject shootingEffect;
    public CameraFolllow camScript;

    private IAiming aimingHandle;
    private Transform firePoint; 
    private float timeToShoot = -1f;
    private bool doShoot = false;
    // Start is called before the first frame update
    void Awake()
    {
        firePoint = weaponPoint;
        if(grenadePoint == null)
        {
            grenadePoint = GameObject.FindGameObjectWithTag("GrenadePoint").transform;
        }
        if(camScript == null)
        {
            camScript = FindObjectOfType<CameraFolllow>();
        }
        aimingHandle = GetComponent<IAiming>();
    }

    public bool Shoot(float input)
    {
        bool fired = false;
        if (input != 0f)
        {
            if (WeaponInfo.CurrentWeapon.currentAmmo > 0)
            {
                fired = true;
                doShoot = true;
            }
        } else if(input == 0f)
        {
            doShoot = false;
        }
        return fired;
    }

    private void ShootOnce()
    {
        if(CheckFirepoint() == false)
        {
            return;
        }
        else
        {
            InitBullet();
        }
        doShoot = false;
    }
    
    private void ShootingEffects(bool shotwasfired)
    {
        if (shotwasfired == true)
        {
            camScript.Shake();
            ShootingVisualEffect(shootingEffect);
            SoundManager.PlaySound("Shoot", ref AudioSources.shoot);
        }
    }
    public void ShootingVisualEffect(GameObject effect)
    {
        if (WeaponInfo.CurrentWeapon.PreFab == null || WeaponInfo.CurrentWeapon.PreFab.tag.Contains("Grenade"))
        {
            return;
        }
        ShootingLight.ShowFireLight();
        Destroy(Instantiate(effect, firePoint.position, weaponPoint.transform.rotation, null), 0.2f);
    }
    private void InitBullet()
    {
        GameObject b = Instantiate(WeaponInfo.CurrentWeapon.bullet, firePoint.position, weaponPoint.transform.rotation);
        IProjectile temp = b.GetComponent<IProjectile>();
        temp.SetDirection(aimingHandle.AimingPosition());
        temp.SetVelocity(bulletSpeed);
        temp.SetDamage(WeaponInfo.CurrentWeapon.damage);
        temp.SetOwner(this);
        temp.StartProjectile();
        WeaponInfo.CurrentWeapon.currentAmmo -= 1;
    }
    private bool CheckFirepoint()
    {
        if (WeaponInfo.CurrentWeapon != null && WeaponInfo.CurrentWeapon.name.Contains("Grenade") == false)
        {
            firePoint = weaponHoldingPosition.GetChild(0).GetChild(0);
        }
        if (firePoint == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void Update()
    {
        if (WeaponInfo.CurrentWeapon != null)
        {
            if(WeaponInfo.CurrentWeapon.fireRate == 0 && doShoot == true)
            {
                ShootOnce();
                ShootingEffects(true);
            } else if (WeaponInfo.CurrentWeapon.fireRate !=0 && doShoot == true)
            {
                if(Time.time >= timeToShoot)
                {
                    timeToShoot = Time.time + 1 / WeaponInfo.CurrentWeapon.fireRate;
                    CheckFirepoint();
                    InitBullet();
                    ShootingEffects(true);
                }
            }
        }
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
