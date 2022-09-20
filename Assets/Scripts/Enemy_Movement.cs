using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Movement : Unit_Health
{
    public enum EnemyState 
    {
      Idle,
      Chasing,
      Attacking
    };


    public ParticleSystem death_Effect;

    private Unit_Health plr_Health;
    private bool plr_Alive;

    private EnemyState current_State;

    [SerializeField]
    private float attack_Dist = .7f;
    [SerializeField]
    private float attack_Time = 1;
    private float next_AttackTime;

    private float enemy_CollisRadius;
    private float plr_CollisRaius;

    NavMeshAgent enemy_Nav;
    Transform plr;
    private float plr_Damage = 1;

    protected override void Start()
    {
        base.Start();
        enemy_Nav = GetComponent<NavMeshAgent>();


        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            plr = GameObject.FindGameObjectWithTag("Player").transform;
            plr_Alive = true;

            plr_Health = plr.GetComponent<Unit_Health>();
            plr_Health.On_Death += Plr_Death;

            current_State = EnemyState.Chasing;

            enemy_CollisRadius = GetComponent<CapsuleCollider>().radius;
            plr_CollisRaius = plr.GetComponent<CapsuleCollider>().radius;

            StartCoroutine(Plr_Haunt());
        }
    }

    private void Update()
    {
        if (plr_Alive)
        {
            if (Time.time > next_AttackTime)
            {
                float sqr_DistToPlr = (plr.position - transform.position).sqrMagnitude;

                if (sqr_DistToPlr < Mathf.Pow(attack_Dist + enemy_CollisRadius + plr_CollisRaius, 2))
                {
                    next_AttackTime = Time.time + attack_Time;
                    StartCoroutine(Plr_Attack());
                }
            }
        }
    }

    IEnumerator Plr_Attack() 
    {
        current_State = EnemyState.Attacking;
        
        enemy_Nav.enabled = false;

        Vector3 initial_Pos = transform.position;
        Vector3 dir_ToPlr = (plr.position - transform.position).normalized;
        Vector3 plr_Pos = plr.position - dir_ToPlr * (enemy_CollisRadius + plr_CollisRaius + attack_Dist);
        Vector3 attack_Pos = plr.position;

        float attack_Speed = 2.5f;
        float ratio = 0;


        bool set_Damage = false;

        while (ratio <= 1) 
        {
            if (ratio >= .5f && !set_Damage) 
            {
                set_Damage = true;
                plr_Health.Damage(plr_Damage);
            }

            ratio += Time.deltaTime * attack_Speed;
            float get_Interval = (-Mathf.Pow(ratio, 2) + ratio) * 4;
            transform.position = Vector3.Lerp(initial_Pos, attack_Pos, get_Interval);

            yield return null;
        }

        current_State = EnemyState.Chasing;
        enemy_Nav.enabled = true;
    }

    IEnumerator Plr_Haunt() 
    {
        
        float ref_Scan = .25f;

        while (plr_Alive) 
        {
            
            Vector3 plr_Pos = new Vector3(plr.position.x, 0, plr.position.z);

            if (!dead)
            {
                enemy_Nav.SetDestination(plr_Pos);
            }    
            yield return new WaitForSeconds(ref_Scan);
        }
    }


    public override void Hit(float dmg, Vector3 hitPnt, Vector3 hitDir)
    {
        if (dmg >= health) 
        {
            Destroy(Instantiate(death_Effect.gameObject, hitPnt, Quaternion.FromToRotation(Vector3.forward,
                hitDir)) as GameObject, death_Effect.main.startLifetimeMultiplier);
        }
        base.Hit(dmg, hitPnt, hitDir);
    }

    void Plr_Death() 
    {
        plr_Alive = false;
        current_State = EnemyState.Idle;
    }

}
