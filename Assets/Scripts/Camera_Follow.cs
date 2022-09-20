using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{

    public Transform followTarget;

    [HideInInspector]
    public float camOffset;

    private void Awake()
    {
        camOffset = transform.position.z - followTarget.transform.position.z;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = new Vector3(followTarget.position.x, transform.position.y, followTarget.transform.position.z + camOffset);


        transform.position = camPos;
    }
}
