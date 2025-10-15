using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Camara : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private Vector3 AjustarPosicion;
    [SerializeField,Range(0f,1.5f)] private float retraso;
    private Vector3 jugador;
    private Vector3 velocidaDato;
    [Header("Ajustar limites")]
    [SerializeField] private Vector2 X;
    [SerializeField] private Vector2 Y;


    private void Update()
    {
        SeguirJugador();
    }

    private void SeguirJugador()
    {
        jugador = Jugador_Control.Instance.transform.position;
        jugador += AjustarPosicion;
        jugador = new Vector3(Mathf.Clamp(jugador.x, X.x, X.y), Mathf.Clamp(jugador.y, Y.x, Y.y), -10);
        //transform.position = Vector3.SmoothDamp(transform.position, jugador, ref velocidaDato, retraso);
        transform.position = jugador;
    }


}
