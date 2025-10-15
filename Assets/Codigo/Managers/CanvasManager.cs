using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.HasKey("Musica") || PlayerPrefs.HasKey("SFX"))
        {
            Musica.value = PlayerPrefs.GetFloat("Musica");
            SFX.value = PlayerPrefs.GetFloat("SFX");
        }
    }


    [Header("Datos de la Iterfaz")]
    public TextMeshProUGUI FrutaDato;
    public TextMeshProUGUI FrutaDatoTotal;
    public Image[] Corazon;

    [Header("Paneles")]
    public GameObject Interfaz;
    public GameObject MenuPausa;
    public GameObject PanelOpciones;
    public GameObject MenuInicio;
    public GameObject Final;

    [Header("Animacion de transicion del Canvas")]
    public Animation PanelTransicion;
    public Animation TransicionNivel;
    public Animation TransicionReseteo;
    public GameObject SystemCanvas;

    [Header("Sonido")]
    public AudioMixer audioMixer;

    [Header("Otros")]
    public string nombrePanel;
    public Slider Musica;
    public Slider SFX;



    //Metodos para actualizar los datos del Canvas
    public void FrutasTotal(int numero)
    {
        FrutaDatoTotal.text = numero.ToString();
    }

    public void FrutasContador(int numero)
    {
        FrutaDato.text = numero.ToString();
    }

    public void ReiniciarCanvas()
    {
        FrutaDato.text = "0";
        Vidas(3);
    }

    //Animacion del Canvas
    public void Transicion()
    {
        PanelTransicion.Play();
    }

    //Ocultar Interfaz
    public void InterfazActivar(bool Activar)
    {
        Interfaz.SetActive(Activar);
    }

    //Actualizar Vidas
    public void Vidas(int vidas)
    {
        switch (vidas)
        {
            case 3:
                Corazon[2].enabled = true;
                Corazon[1].enabled = true;
                Corazon[0].enabled = true;
                return;
            case 2:
                Corazon[2].enabled = false;
                return;
            case 1:
                Corazon[1].enabled = false;
                return;
            case 0:
                Corazon[0].enabled = false;
                return;
        }
    }

    //Pausa
    public void Pausa()
    {
        Time.timeScale = 0;
        Interfaz.SetActive(false);
        MenuPausa.SetActive(true);
        Animaciones_LeanTween.Instance.MoverInicio();
    }


    //MenuPausa
    public void Reanudar()
    {
        StartCoroutine(AnimacionVolver_mas_otros());
    }

    IEnumerator AnimacionVolver_mas_otros()
    {
        Animaciones_LeanTween.Instance.MoverFinal();
        SystemCanvas.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        SystemCanvas.SetActive(true);
        Time.timeScale = 1;
        Interfaz.SetActive(true);
        MenuPausa.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1;
        MenuPausa.SetActive(false);
        ReiniciarCanvas();
        StartCoroutine(PasarPanel(Interfaz,Interfaz,"JAJAJA", true));
    }

    public void ReiniciarSecundario()
    {
        TransicionReseteo.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Opciones()
    {
        if (MenuPausa.active)
        {
            StartCoroutine(AnimacionAbrir_Opciones(MenuPausa));
            nombrePanel = "MenuPausa";
        }
        else if (MenuInicio.active)
        {
            StartCoroutine(AnimacionAbrir_Opciones(MenuInicio));
            nombrePanel = "MenuInicio";
        }
    }
    IEnumerator AnimacionAbrir_Opciones(GameObject desactivar)
    {
        PanelOpciones.SetActive(true);
        desactivar.SetActive(false);
        SystemCanvas.SetActive(false);
        Animaciones_LeanTween.Instance.OpcionesAnimacionAbrir();
        yield return new WaitForSecondsRealtime(1);
        SystemCanvas.SetActive(true);
    }

    public void Salir()
    {
        Time.timeScale = 1;
        ReiniciarCanvas();
        StartCoroutine(PasarPanel(MenuPausa, MenuInicio, "Menu", false));
        Invoke("III", 1);
    }

    //Imvokar
    public void III()
    {
        Animaciones_LeanTween.Instance.IniciaAnimacion();
        Animaciones_LeanTween.Instance.BotonOpciones();
    }

    //Botones de Opciones

    public void Pantalla(bool completo)
    {
        Screen.fullScreen = completo;
    }

    public void MusicaAjustar()
    {
        audioMixer.SetFloat("Musica", MathF.Log10(Musica.value) * 20);
        PlayerPrefs.SetFloat("Musica", Musica.value);
    }

    public void SFXAjustar()
    {
        audioMixer.SetFloat("SFX", MathF.Log10(SFX.value) * 20);
        PlayerPrefs.SetFloat("SFX", SFX.value);
    }

    public void ResetearAudio()
    {
        PlayerPrefs.DeleteAll();
        Musica.value = 0.8f;
        SFX.value = 0.8f;
    }


    public void SalirOpciones()
    {
        if (nombrePanel == "MenuPausa")
        {
            StartCoroutine(AnimacionCerrar_Opciones(MenuPausa));
        }
        else if (nombrePanel == "MenuInicio")
        {
            StartCoroutine(AnimacionCerrar_Opciones(MenuInicio));
        }
    }

    IEnumerator AnimacionCerrar_Opciones(GameObject activar)
    {
        activar.SetActive(true);
        SystemCanvas.SetActive(false);
        Animaciones_LeanTween.Instance.OpcionesAnimacionCerrar();
        yield return new WaitForSecondsRealtime(0.2f);
        PanelOpciones.SetActive(false);
        SystemCanvas.SetActive(true);
    }

    IEnumerator PasarPanel(GameObject PanelFalse, GameObject PanelTrue, string Escena,bool Reiniciar)
    {
        PanelFalse.SetActive(false);
        SystemCanvas.SetActive(false);
        TransicionNivel.Play();
        yield return new WaitForSeconds(0.7f);
        if(Reiniciar) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManager.LoadScene(Escena);
            
        }
            yield return new WaitForSeconds(0.7f);
            PanelTrue.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            SystemCanvas.SetActive(true);
        
    }

    public void TransicionJuego(string nivel)
    {
        StartCoroutine(PasarPanel(Interfaz, Interfaz, nivel, false));
    }

    public void TransicionJuegoFinal(string nivel)
    {
        StartCoroutine(PasarPanel(Interfaz, Final, nivel, false));
    }

    //Menu Botones
    public void Jugar()
    {
        StartCoroutine(PasarPanel(MenuInicio,Interfaz,"Nivel_1",false));
    }


    public void SalirJuego()
    {
        Application.Quit();
    }

    //Final

    public void FinalBoton()
    {
        Time.timeScale = 1;
        ReiniciarCanvas();
        StartCoroutine(PasarPanel(Final, MenuInicio, "Menu", false));
        Invoke("III", 1);
    }
}
