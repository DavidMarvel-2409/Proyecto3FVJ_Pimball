using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class InfoController : MonoBehaviour
{
    public GameObject pelota;
    public TextMeshProUGUI contadordevidas;
    public TextMeshProUGUI MensajeEnPantalla;
    public GameObject HUD, _Start;

    void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        contadordevidas.text = $"vidas: {pelota.GetComponent<PelotaScript>().lifes}";
        if (pelota.GetComponent<PelotaScript>().lifes == 0)
        {
            Time.timeScale = 0;
            MensajeEnPantalla.text = "GAME OVER";
        }
        else MensajeEnPantalla.text = " ";
    }

    public void EndGame()
    {
        Application.Quit();
    }
    public void started()
    {
        _Start.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1;
    }
}
