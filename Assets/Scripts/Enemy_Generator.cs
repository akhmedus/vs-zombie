using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Generator : MonoBehaviour
{

    public NewEnemies[] new_Enemies;

    NewEnemies current_Enemies;
    int enemies_WaveNum;
    float time_ToGenerate;
    int enemies_ToGenerate;
    int still_Alives;
    [System.Serializable]
    public class NewEnemies 
    {
        public int enemy_Count;
        public float spawn_Time;
    }

    [SerializeField]
    private Enemy_Movement enemy;

    void Start()
    {
        NextEnemies();
    }


    void NextEnemies() 
    {
        enemies_WaveNum++;

        if (enemies_WaveNum - 1 < new_Enemies.Length)
        {
            current_Enemies = new_Enemies[enemies_WaveNum - 1];

            enemies_ToGenerate = current_Enemies.enemy_Count;
            still_Alives = enemies_ToGenerate;
        }
    }
    void Update()
    {
        if (enemies_ToGenerate > 0 && Time.time > time_ToGenerate) 
        {
            enemies_ToGenerate--;
            time_ToGenerate = Time.time + current_Enemies.spawn_Time;

            Enemy_Movement _enemy = Instantiate(enemy, Vector3.zero, Quaternion.identity);
            _enemy.On_Death += EnemyDeath;
        }
    }

    void EnemyDeath() 
    {
        still_Alives--;

        if (still_Alives == 0)
        {
            NextEnemies();
        }
    }
}
