using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [Header("Datos de la Fruta")]
    public int Frutas;
    public GameObject[] FrutasMaximas;

    [Header("Datos del Jugador")]
    public Vector2 PosicioInicial;

    private void Start()
    {
        GuardarPosicion();
        CalcularFrutas();
    }

    //Metodos para la fruta
    public void SumarFruta()
    {
        Frutas++;
        CanvasManager.Instance.FrutasContador(Frutas);
    }

    public void CalcularFrutas()
    {
        FrutasMaximas = GameObject.FindGameObjectsWithTag("Fruta");
        int A = FrutasMaximas.Length;
        CanvasManager.Instance.FrutasTotal(A);
    }

    //GuardarPosicion
    public void GuardarPosicion()
    {
        PosicioInicial = Jugador_Control.Instance.transform.position;
    }

    //Respawn
    public void Respawn()
    {
        StartCoroutine(ResetJugador());
    }

    
    //Corrutina
    IEnumerator ResetJugador()
    {
        Jugador_Control.Instance.Stun = true;
        Jugador_Control.Instance.rb.simulated = false;
        Jugador_Control.Instance.MorirAnimacion();
        CanvasManager.Instance.Transicion();
        CanvasManager.Instance.InterfazActivar(false);
        yield return new WaitForSeconds(0.5f);
        CanvasManager.Instance.InterfazActivar(true);
        CanvasManager.Instance.Vidas(Jugador_Control.Instance.VidasVariable);
        Jugador_Control.Instance.Invencible = 0;
        Jugador_Control.Instance.rb.velocity = Vector2.zero;
        Jugador_Control.Instance.transform.position = PosicioInicial;
        Jugador_Control.Instance.Stun = false;
        Jugador_Control.Instance.rb.simulated = true;
    }

}
