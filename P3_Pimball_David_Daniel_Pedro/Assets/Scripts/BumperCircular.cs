using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperCircular : MonoBehaviour
{
    [SerializeField] private List<Material> Materiales = new List<Material>();
    private GameObject player;
    private Renderer rr;
    [SerializeField] private float fuerza;

    [SerializeField] private List<AudioClip> sonidos;
    private AudioSource ss;
    public bool pedoMode = false;

    void Start()
    {
        rr = GetComponent<Renderer>();
        ss = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Debug.Log($"{transform.rotation}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rr.material = Materiales[1];
            Vector3 direc = collision.transform.position - transform.position;
            collision.gameObject.GetComponent<PelotaScript>().empuje(direc,fuerza);
            collision.gameObject.GetComponent<PelotaScript>().Points++;
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
