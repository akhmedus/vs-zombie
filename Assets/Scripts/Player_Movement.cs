using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Vector3 plr_velocity;
    Rigidbody plr_rb;

    private void Start()
    {
        plr_rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity) 
    {
        plr_velocity = _velocity;
    }

    public void Look(Vector3 lookDir) 
    {
        Vector3 body_CorrectPos = new Vector3(lookDir.x, transform.position.y, lookDir.z);
        transform.LookAt(body_CorrectPos);
    }

    private void FixedUpdate()
    {
        plr_rb.MovePosition(plr_rb.position + plr_velocity * Time.fixedDeltaTime);
    }
}
