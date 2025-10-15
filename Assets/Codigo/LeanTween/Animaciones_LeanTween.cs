using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Animaciones_LeanTween : MonoBehaviour
{
    public static Animaciones_LeanTween Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject Opciones;
    [SerializeField] private Button Boton;
    [SerializeField] private GameObject[] Inicio;
    [SerializeField] private float diley;
    [SerializeField] private float direccion;
    [SerializeField] private float direccion2;
    [SerializeField] private float duracion;
    [SerializeField] private float duracion2;
    [SerializeField] private LeanTweenType Ease;
    [SerializeField] private LeanTweenType Ease2;
    [SerializeField] private LeanTweenType Ease3;
    [SerializeField] private LeanTweenType Ease4;
    //2.5f
    private void Start()
    {
        IniciaAnimacion();
        BotonOpciones();
    }

    //Corrutina para activar botones

    IEnumerator ActivarBoton()
    {
        Boton.enabled = false;
        yield return new WaitForSeconds(2.5f);
        Boton.enabled = true;
    }

    public void BotonOpciones()
    {
        StartCoroutine(ActivarBoton());
    }

    //Botones de Pausa y reanudar
    public void MoverInicio()
    {
        resetear();
        LeanTween.moveY(Menu, direccion, duracion).setEase(Ease).setIgnoreTimeScale(true);
    }

    public void resetear()
    {
        Menu.transform.localPosition = new Vector3(0, 1030, 0);
        LeanTween.reset();
    }

    public void MoverFinal()
    {
        LeanTween.moveY(Menu, direccion2, duracion2).setEase(Ease2).setIgnoreTimeScale(true);
    }

    //Menu Inicio
    public void MenuInicio1()
    {
        resetear2();
        LeanTween.scale(Inicio[3], transform.localScale * 1.3f, 1).setIgnoreTimeScale(true).setEase(Ease4).setDelay(diley);
        LeanTween.moveY(Inicio[0], 600, 0.5f).setIgnoreTimeScale(true).setEase(Ease3).setDelay(diley).setOnComplete(MenuInicio2);
    }

    public void MenuInicio2()
    {
        LeanTween.moveY(Inicio[1], 400, 0.5f).setIgnoreTimeScale(true).setEase(Ease3).setOnComplete(MenuInicio3);
    }

    public void MenuInicio3()
    {
        LeanTween.moveY(Inicio[2], 200, 0.5f).setEase(Ease3).setIgnoreTimeScale(true);
    }

    public void resetear2()
    {
        Inicio[0].transform.localPosition = new Vector3(0, -632, 0);
        Inicio[1].transform.localPosition = new Vector3(0, -831, 0);
        Inicio[2].transform.localPosition = new Vector3(0, -1036, 0);
        Inicio[3].transform.localScale = Vector3.zero;
        LeanTween.reset();
    }

    //Iniciar Animacion del Menu Inicio

    public void IniciaAnimacion()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            MenuInicio1();
        }
    }

    //Opciones

    public void OpcionesAnimacionAbrir()
    {
        Opciones.transform.localScale = Vector3.zero;
        LeanTween.reset();
        LeanTween.scale(Opciones, transform.localScale * 1, 1).setIgnoreTimeScale(true).setEase(Ease4);
    }

    public void OpcionesAnimacionCerrar()
    {
        LeanTween.scale(Opciones, Vector3.zero, 0.2f).setIgnoreTimeScale(true);
    }

}
