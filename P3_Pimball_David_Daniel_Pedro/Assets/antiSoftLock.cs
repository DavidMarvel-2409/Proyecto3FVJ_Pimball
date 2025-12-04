using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antiSoftLock : MonoBehaviour
{
    [SerializeField] private GameObject entrada;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            entrada.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
