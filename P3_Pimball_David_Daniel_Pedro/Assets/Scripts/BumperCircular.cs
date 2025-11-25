using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperCircular : MonoBehaviour
{
    [SerializeField] private List<Material> Materiales = new List<Material>();
    private GameObject player;
    private Renderer rr;
    [SerializeField] private float fuerza;

    void Start()
    {
        rr = GetComponent<Renderer>();
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
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        rr.material = Materiales[0];
    }
}
