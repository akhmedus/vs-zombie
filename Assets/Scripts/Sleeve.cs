using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleeve : MonoBehaviour
{
    private Rigidbody rb;
    public float max_Force;
    public float min_Force;

    private float life_Time = 4;
    private float droop_Time = 2;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        float current_Force = Random.Range(min_Force, max_Force);

        rb.AddForce(transform.right * current_Force);
        rb.AddTorque(Random.insideUnitSphere * current_Force);

        
    }

    IEnumerator Droop() 
    {
        yield return new WaitForSeconds(life_Time);

        float percent = 0;
        float droop_Speed = 1 / droop_Time;
        Material _material = GetComponent<Renderer>().material;
        Color _color = _material.color;

        while (percent < 1) 
        {
            percent += Time.deltaTime * droop_Speed;
            _material.color = Color.Lerp(_color, Color.clear, percent);
            yield return null;
        }

        Destroy(gameObject);
    }
}
