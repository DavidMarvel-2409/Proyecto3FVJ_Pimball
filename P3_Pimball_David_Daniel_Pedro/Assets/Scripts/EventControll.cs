using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventControll : MonoBehaviour
{
    private KeyCode[] code = new KeyCode[]
    {
        KeyCode.UpArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.LeftArrow,
        KeyCode.B,
        KeyCode.A,
        KeyCode.Return   // Enter
    };
    private int index = 0;
    private bool pedoMode = false;

    [SerializeField] private List<GameObject> Objetos_en_escenario = new List<GameObject>();
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(code[index]))
            {
                index++;

                // ¿Completó toda la secuencia?
                if (index == code.Length)
                {
                    Debug.Log("¡Konami Code Activado!");
                    set_PedoMode();
                    index = 0;
                }
            }
            else
            {
                index = 0;
            }
        }
    }

    private void set_PedoMode()
    {
        pedoMode = !pedoMode;
        if (Objetos_en_escenario.Count > 0)
        {
            for (int i = 0; i < Objetos_en_escenario.Count; i++)
            {
                try
                {
                    Objetos_en_escenario[i].GetComponent<PelotaScript>().pedoMode = pedoMode;
                }
                catch
                {
                    try
                    {
                        Objetos_en_escenario[i].GetComponent<PaloScript>().pedoMode = pedoMode;
                    }
                    catch
                    {
                        try
                        {
                            Objetos_en_escenario[i].GetComponent<BumperCircular>().pedoMode = pedoMode;
                        }
                        catch
                        {
                            Debug.Log("Objeto fuera de lista");
                        }
                    }
                }
            }
        }
    }
}
