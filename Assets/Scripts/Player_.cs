using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ : Unit_Health
{

    public float move_Speed = 5;
    Player_Movement plr_Movement;
    Camera view_Camera;
    Weapon_Controller plr_Weapon;
    protected override void Start()
    {
        base.Start();
        view_Camera = Camera.main;
        plr_Weapon = GetComponent<Weapon_Controller>();
        plr_Movement = GetComponent<Player_Movement>();
    }

    void Update()
    {
        Vector3 move_Input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 _velocity = move_Input.normalized * move_Speed;
        plr_Movement.Move(_velocity);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane _ground = new Plane(Vector3.up, Vector3.zero);
        float ray_Dist;
        if (_ground.Raycast(ray, out ray_Dist)) 
        {
            Vector3 view_Point = ray.GetPoint(ray_Dist);
            plr_Movement.Look(view_Point);
        }

        if (Input.GetMouseButton(0)) 
        {
            plr_Weapon.Shoot();
        }
    }
}
