using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour, IAiming
{
    private Vector3 shootingDirection = new Vector3();
    public Transform weaponPoint;

    private void Awake()
    {
        if(weaponPoint == null)
        {
            Debug.LogError("No weapon point assigned!");
        }
    }
    public void SetMousePosition(Vector2 screenPos)
    {
        Vector3  WorldPos = Camera.main.ScreenToWorldPoint(screenPos);
        WorldPos.z -= Camera.main.transform.position.z;
        shootingDirection = WorldPos - weaponPoint.position;
    }
    private void RotateWeapon()
    {
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;

        if (PlayerMove.FacingRight == false)
        {
            shootingDirection.x *= -1;
        }
        if (angle >= -90 && angle <= 90)
        {
            weaponPoint.eulerAngles = new Vector3(0f, weaponPoint.eulerAngles.y, angle);
        }
    }
    public Vector2 AimingPosition()
    {
        return weaponPoint.right;
    }

    void Update()
    {
        RotateWeapon();
    }
}
