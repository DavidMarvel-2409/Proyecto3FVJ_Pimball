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
    [SerializeField] private GameObject entrada;
    private ParticleSystem ps;
    public bool pedoMode = false;
    private int Points = 0;
    void Start()
    {
        rd = GetComponent<Rigidbody>();
        spawn = transform.position;
        ss = GetComponent<AudioSource>();
        ps = transform.Find("effect").GetComponent<ParticleSystem>();
    }

    public void empuje(Vector3 direc, float speed)
    {
        rd.velocity += direc * speed;
        ps.Emit(20);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "muerte")
        {
            lifes--;
            transform.position = spawn;
            entrada.GetComponent<entrada>().ActivarEntrada();
            if (pedoMode)
            {
                ss.pitch = Random.Range(0.1f, 0.2f);
                ss.PlayOneShot(sonidos[0]);
            }
        }
    }

    public void AddPuntaje(int puntaje) => Points += puntaje;
    public int getPoints() => Points;
}
