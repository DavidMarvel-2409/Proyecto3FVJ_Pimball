using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelotaScript : MonoBehaviour
{
    private Rigidbody rd;
    public int lifes;
    private Vector3 spawn;
    private AudioSource ss;
    [SerializeField] private List<AudioClip> sonidos;
    void Start()
    {
        rd = GetComponent<Rigidbody>();
        spawn = transform.position;
        ss = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void empuje(Vector3 direc, float speed)
    {
        rd.velocity += direc * speed;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "muerte")
        {
            lifes--;
            transform.position = spawn;
            ss.pitch = Random.Range(0.1f, 0.2f);
            ss.PlayOneShot(sonidos[0]);
        }
    }
}
