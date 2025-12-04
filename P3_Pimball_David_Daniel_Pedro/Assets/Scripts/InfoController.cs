using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InfoController : MonoBehaviour
{
    public GameObject pelota;
    public TextMeshProUGUI contadordevidas, bells, fuerza;
    public TextMeshProUGUI MensajeEnPantalla, puntajeBox, nameBox;
    public GameObject HUD, _Start, puntajes, Content_, puntajeItemPrefab, pad;
    private string tipodepuntos = "";
    private bool GameOver = false;
    private bool fadeInMusic = false;
    private float t = 0;
    private string FileName = "Puntajes.json", ruta; 
    [SerializeField] private AudioSource menu, ingame;

    private void Awake()
    {
        ruta = Path.Combine(Application.persistentDataPath, FileName);
    }

    void Start()
    {
        Time.timeScale = 0;
        mostrar();
        //Debug.Log($"Rura: {ruta}");
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
        bells.text = $"{tipodepuntos} x {pelota.GetComponent<PelotaScript>().getPoints()}";

        float f = pad.GetComponent<PaloScript>().getFuerzaCargada();
        switch (f)
        {
            case 00f: fuerza.text = "Fuerza: ------------"; break;
            case 05f: fuerza.text = "Fuerza: []----------"; break;
            case 10f: fuerza.text = "Fuerza: [--]--------"; break;
            case 15f: fuerza.text = "Fuerza: [----]------"; break;
            case 20f: fuerza.text = "Fuerza: [------]----"; break;
            case 25f: fuerza.text = "Fuerza: [--------]--"; break;
            case 30f: fuerza.text = "Fuerza: [----------]"; break;
        }

        if (fadeInMusic)
        {
            t += 0.5f * Time.deltaTime;
            float ingaVol = Mathf.Lerp(0f, 1f, t);
            float menuVol = Mathf.Lerp(1f, 0f, t);
            Debug.Log($"Fade In: {t}");
            menu.volume = menuVol;
            ingame.volume = ingaVol;
            if (ingaVol == 1 && menuVol == 0) 
            {
                t = 0;
                fadeInMusic = false;
            }
        }

        puntajes.SetActive(GameOver);

        if (!GameOver)
        {
            puntajeBox.text = $"Nuevo Puntaje: {pelota.GetComponent<PelotaScript>().getPoints()}";
        }
    }

    public void Save()
    {
        string namee = nameBox.text; 
        if (!string.IsNullOrWhiteSpace(namee))
        {
            GuardarNuevoPuntaje(namee, pelota.GetComponent<PelotaScript>().getPoints());

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
        fadeInMusic = true;
    }
    private void mostrar()
    {
        PuntajeData datosCargados = CargarPuntajes();

        foreach (Transform child in Content_.transform)
            Destroy(child.gameObject);

        for (int i = 0; i < datosCargados.listaPuntajes.Count; i++)
        {
            PuntajeEntry entry = datosCargados.listaPuntajes[i];

            GameObject nuevoItem = Instantiate(puntajeItemPrefab, Content_.transform);

            TextMeshProUGUI textoTMP = nuevoItem.GetComponent<TextMeshProUGUI>();

            if (textoTMP != null)
            {
                textoTMP.text = $"{i + 1}) {entry.nombreJugador} - {entry.puntaje}";
            }
            else
            {
                Debug.LogError("El prefab no tiene TextMeshProUGUI!");
            }
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

        if (data.listaPuntajes.Count > 3)
            data.listaPuntajes = data.listaPuntajes.GetRange(0, 3);

        string json = JsonUtility.ToJson(data, true);

        try
        {
            File.WriteAllText(ruta, json);
            Debug.Log("Puntajes guardados. Total: " + data.listaPuntajes.Count);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al guardar JSON: " + e.Message);
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
