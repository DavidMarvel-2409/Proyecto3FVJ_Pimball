using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entrada : MonoBehaviour
{
    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boxCollider.isTrigger = false;
        }
    }

    public void ActivarEntrada() => boxCollider.isTrigger = true;
}
