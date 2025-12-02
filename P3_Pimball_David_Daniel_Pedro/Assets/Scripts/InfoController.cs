using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InfoController : MonoBehaviour
{
    public GameObject pelota;
    public TextMeshProUGUI contadordevidas, bells;
    public TextMeshProUGUI MensajeEnPantalla, puntajeBox, nameBox;
    public GameObject HUD, _Start, puntajes, Content_, puntajeItemPrefab;
    private string tipodepuntos = "";
    private bool GameOver = false;

    private string FileName = "Puntajes.json", ruta; 
    private void Awake()
    {
        ruta = Path.Combine(Application.persistentDataPath, FileName);
    }

    void Start()
    {
        Time.timeScale = 0;
        mostrar();
    }

    void Update()
    {
        contadordevidas.text = $"vidas: {pelota.GetComponent<PelotaScript>().lifes}";
        if (pelota.GetComponent<PelotaScript>().lifes == 0)
        {
            Time.timeScale = 0;
            MensajeEnPantalla.text = "GAME OVER";
            GameOver = true;
        }
        else MensajeEnPantalla.text = " ";
        if (pelota.GetComponent<PelotaScript>().pedoMode) tipodepuntos = "Pedos";
        else tipodepuntos = "Campanas";
        bells.text = $"{tipodepuntos} x {pelota.GetComponent<PelotaScript>().Points}";

        puntajes.SetActive(GameOver);

        if (!GameOver)
        {
            puntajeBox.text = $"Nuevo Puntaje: {pelota.GetComponent<PelotaScript>().Points}";
        }
    }

    public void Save()
    {
        string namee = nameBox.text; 
        if (!string.IsNullOrWhiteSpace(namee))
        {
            GuardarNuevoPuntaje(namee, pelota.GetComponent<PelotaScript>().Points);

            mostrar();
        }
        else
        {
            Debug.LogWarning("El nombre del jugador no puede estar vacío.");
        }
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
    private void mostrar()
    {
        PuntajeData datosCargados = CargarPuntajes(); 
        foreach (Transform child in Content_.transform)
        {
            Destroy(child.gameObject);
        }
        int maxPuntajesAMostrar = 10;
        for (int i = 0; i < datosCargados.listaPuntajes.Count; i++)
        {
            if (i >= maxPuntajesAMostrar)
            {
                break;
            }
            PuntajeEntry entry = datosCargados.listaPuntajes[i];
            GameObject nuevoItem = Instantiate(puntajeItemPrefab, Content_.transform);
            TextMeshProUGUI textoTMP = nuevoItem.GetComponent<TextMeshProUGUI>();
            if (textoTMP != null)
            {
                // Formato: Posición. Nombre - Puntaje
                textoTMP.text = $"{(i + 1)}. {entry.nombreJugador} - {entry.puntaje}\n";
            }
            else
            {
                Debug.LogError("¡El Prefab de puntaje no tiene un componente TextMeshProUGUI!");
            }
            nuevoItem.SetActive(true);

        }
    }
    public void GuardarNuevoPuntaje(string nombre, int score)
    {
        PuntajeData data = CargarPuntajes();
        PuntajeEntry nuevoEntry = new PuntajeEntry
        {
            nombreJugador = nombre,
            puntaje = score
        };
        data.listaPuntajes.Add(nuevoEntry);
        data.listaPuntajes.Sort((a, b) => b.puntaje.CompareTo(a.puntaje));

        string json = JsonUtility.ToJson(data);

        try
        {
            File.WriteAllText(ruta, json);
            Debug.Log("Nuevo puntaje guardado. Total de registros: " + data.listaPuntajes.Count);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al guardar el archivo JSON: " + e.Message);
        }
    }
    public PuntajeData CargarPuntajes()
    {
        if (!File.Exists(ruta))
        {
            return new PuntajeData { listaPuntajes = new List<PuntajeEntry>() };
        }

        try
        {
            string json = File.ReadAllText(ruta);
            PuntajeData data = JsonUtility.FromJson<PuntajeData>(json);

            if (data.listaPuntajes == null)
            {
                data.listaPuntajes = new List<PuntajeEntry>();
            }

            Debug.Log("Datos JSON cargados con " + data.listaPuntajes.Count + " puntajes.");
            return data;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al cargar o deserializar el archivo JSON: " + e.Message);
            return new PuntajeData { listaPuntajes = new List<PuntajeEntry>() };
        }
    }

    [System.Serializable]
    public class PuntajeEntry
    {
        public string nombreJugador;
        public int puntaje;
    }

    [System.Serializable]
    public class PuntajeData
    {
        public List<PuntajeEntry> listaPuntajes;
    }
}
