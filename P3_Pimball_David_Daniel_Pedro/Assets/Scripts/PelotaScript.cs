using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelotaScript : MonoBehaviour
{
    private Rigidbody rd;
    void Start()
    {
        rd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void empuje(Vector3 direc, float speed)
    {
        rd.velocity = direc * speed;
        
    }
}
