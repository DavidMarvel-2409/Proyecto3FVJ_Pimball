using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaloScript : MonoBehaviour
{
    public float fuerza;
    public KeyCode control;
    public GameObject PP;
    public List<Material> Materiales;
    private Renderer rr;
    public GameObject palo;

    void Start()
    {
        rr = palo.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(control))
        {
            if (PP != null)
            {
                PP.GetComponent<PelotaScript>().empuje(transform.forward, fuerza);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        PP = collision.gameObject;
        rr.material = Materiales[1];
        //Debug.Log("lala");
    }
    private void OnCollisionExit(Collision collision)
    {
        PP = null;
        rr.material = Materiales[0];
    }

}
