using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Direction : MonoBehaviour
{
    float prjl_Speed = 10;

    [SerializeField]
    private LayerMask col_Mask;

    float dmg = 1;


    private void Start()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, .1f, col_Mask);

        if (collisions.Length > 0) 
        {
            Hit_Object(collisions[0], transform.position);
        }

        StartCoroutine(PrjlDestroy());
    }
    public void Speed(float new_speed) 
    {
        prjl_Speed = new_speed;
    }

    private void Update()
    {
        float move_Dist = prjl_Speed * Time.deltaTime;
        Checked_Col(move_Dist);
        transform.Translate(Vector3.forward * move_Dist);

    }

    private void Checked_Col(float move_Dist)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, move_Dist + .1f, col_Mask, QueryTriggerInteraction.Collide)) 
        {
            Hit_Object(hit.collider, hit.point);
        }
    }



    void Hit_Object(Collider cldr, Vector3 hitPnt) 
    {
        IDamageable _damageable = cldr.GetComponent<IDamageable>();

        if (_damageable != null)
        {
            _damageable.Hit(dmg, hitPnt, transform.forward);
        }

        Destroy(gameObject);
    }


    IEnumerator PrjlDestroy() 
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
