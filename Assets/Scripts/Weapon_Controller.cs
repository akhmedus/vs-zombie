using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Controller : MonoBehaviour
{
    [SerializeField]
    private Transform weapon_Pos;
    [SerializeField]
    private Gun start_Gun;
    Gun gun;
     
    void Start()
    {
        if (start_Gun != null) 
        {
            plr_Gun(start_Gun);
        }
    }

    public void plr_Gun(Gun _gun) 
    {
        if (gun != null) 
        {
            Destroy(gun.gameObject);
        }
        gun = Instantiate(_gun, weapon_Pos.position, weapon_Pos.rotation) as Gun;
        gun.transform.parent = weapon_Pos;
    }

    public void Shoot() 
    {
        if (gun != null) 
        {
            gun.Shoot();
        }
    }
}
