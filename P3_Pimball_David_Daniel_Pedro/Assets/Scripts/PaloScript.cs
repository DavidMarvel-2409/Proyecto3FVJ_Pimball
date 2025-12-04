using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaloScript : MonoBehaviour
{
    public float fuerza;
    public float fuerzaCargada = 0;
    public KeyCode control;
    public GameObject PP;
    public List<Material> Materiales;
    private Renderer rr;
    private AudioSource ss;
    [SerializeField] private List<AudioClip> sonidos;
    public bool pedoMode = false;
    //public GameObject palo;

    void Start()
    {
        rr = GetComponent<Renderer>();
        ss = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(control))
        {
            if (fuerzaCargada < fuerza) fuerzaCargada += 50 * Time.deltaTime;
            else fuerzaCargada = fuerza;
        }
        else if (Input.GetKeyUp(control)) {
            if (PP != null)
            {
                Debug.Log("Lanzamiento");
                ss.pitch = Random.Range(0.3f, 0.5f);
                if (pedoMode) ss.PlayOneShot(sonidos[0]);
                else ss.PlayOneShot(sonidos[1]);
                if (fuerzaCargada >= fuerza) fuerzaCargada = fuerza;
                PP.GetComponent<PelotaScript>().empuje(transform.forward, fuerzaCargada);
            }
            fuerzaCargada = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PP = collision.gameObject;
            rr.material = Materiales[1];
        }
        //Debug.Log("lala");
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PP = null;
            rr.material = Materiales[0];
            if (Input.GetKey(control))
            {
                ss.pitch = Random.Range(0.3f, 0.5f);
                if (pedoMode) ss.PlayOneShot(sonidos[0]);
                else ss.PlayOneShot(sonidos[1]);
            }
        }
    }

    public float getFuerzaCargada() => fuerzaCargada;

}
