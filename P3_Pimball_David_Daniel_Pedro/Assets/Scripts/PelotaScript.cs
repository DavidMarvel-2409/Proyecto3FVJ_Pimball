using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelotaScript : MonoBehaviour
{
    private Rigidbody rd;
    public int lifes;
    private Vector3 spawn;
    void Start()
    {
        rd = GetComponent<Rigidbody>();
        spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void empuje(Vector3 direc, float speed)
    {
        rd.velocity = direc * speed;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "muerte")
        {
            lifes--;
            transform.position = spawn;
        }
    }
}
