using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumperpad : MonoBehaviour
{
    [SerializeField] private List<Material> Materiales = new List<Material>();
    private GameObject player;
    private Renderer rr;
    [SerializeField] private float fuerza;
    [SerializeField] private List<AudioClip> sonidos;
    private AudioSource ss;
    public bool pedoMode = false;
    [SerializeField] private int addPuntaje;
    void Start()
    {
        rr = GetComponent<Renderer>();
        ss = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rr.material = Materiales[1];
            collision.gameObject.GetComponent<PelotaScript>().empuje(transform.forward, fuerza);
            collision.gameObject.GetComponent<PelotaScript>().AddPuntaje(addPuntaje);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        rr.material = Materiales[0];
        ss.pitch = Random.Range(0.9f, 1.2f);
        if (pedoMode) ss.PlayOneShot(sonidos[0]);
        else ss.PlayOneShot(sonidos[1]);
    }
}
