using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Transform muzzle;
    [SerializeField]
    private Projectile_Direction projectile;

    public float time_Shots = 100;
    public float muzzle_Speed = 35;
    float next_ShotTime;

    public Transform sleeve_Prefab;
    public Transform sleeve_Ejector;
    public void Shoot() 
    {
        if (Time.time > next_ShotTime) 
        {
            next_ShotTime = Time.time + time_Shots / 1000;
            Projectile_Direction _projectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile_Direction;
            _projectile.Speed(muzzle_Speed);

            Instantiate(sleeve_Prefab, sleeve_Ejector.position, sleeve_Ejector.rotation);
        }
    }
}
