using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : MonoBehaviour
{
    [Header("Detectar Jugador")]
    [SerializeField] private float radio;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform Jugador;
    [Header("Fuerzas y distancia")]
    [SerializeField] private float velocidad;
    [SerializeField] private float distanciaMaxima;
    [SerializeField] private Vector3 PuntoInicial;
    [Header("Girar")]
    [SerializeField] private bool mirandoDerecha;


    public EstadoMovimiento estadoActual;

    public enum EstadoMovimiento
    {
        Esperando,
        Siguiendo,
        Volviendo,
    }

    //Falta girar

    private void Start()
    {
        PuntoInicial = transform.position;
    }

    void Update()
    {
        switch (estadoActual)
        {
            case EstadoMovimiento.Esperando:
                EstadoEsperando();
                break;
            case EstadoMovimiento.Siguiendo:
                EstadoSiguiendo();
                break;
            case EstadoMovimiento.Volviendo:
                EstadoVolviendo();
                break;
        }
    }

    public void EstadoEsperando()
    {
        Collider2D circulo = Physics2D.OverlapCircle(transform.position,radio,layer);

        if (circulo)
        {
            Jugador = circulo.transform;

            estadoActual = EstadoMovimiento.Siguiendo;
        }
    }

    public void EstadoSiguiendo()
    {
        if(Jugador == null)
        {
            estadoActual = EstadoMovimiento.Volviendo;
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position,Jugador.position,velocidad * Time.deltaTime);

        Girarfantasma(Jugador.position);

        if(Vector2.Distance(transform.position,PuntoInicial) > distanciaMaxima || Vector2.Distance(transform.position,Jugador.position) > distanciaMaxima)
        {
            estadoActual = EstadoMovimiento.Volviendo;
            Jugador = null;
        }
    }

    public void EstadoVolviendo()
    {
        transform.position = Vector2.MoveTowards(transform.position, PuntoInicial, velocidad * Time.deltaTime);

        Girarfantasma(PuntoInicial);

        if(Vector2.Distance(transform.position,PuntoInicial) < 0.1f)
        {
            estadoActual = EstadoMovimiento.Esperando;
        }
    }

    public void Girarfantasma(Vector3 objetivo)
    {
        if (objetivo.x > transform.position.x && !mirandoDerecha)
        {
            Girar();
        }
        else if (objetivo.x < transform.position.x && mirandoDerecha)
        {
            Girar();
        }
    }

    public void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y + 180,0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position,radio);
        Gizmos.DrawWireSphere(PuntoInicial,distanciaMaxima);
    }
}
