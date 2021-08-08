using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile 
{
    public void StartProjectile();
    public void SetDirection(Vector2 dir);
    public void SetVelocity(float vel);
    public void SetDamage(float damage);
    public void SetOwner(PlayerShoot shootingHandle);

}
