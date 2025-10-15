using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rinoceronte : MonoBehaviour
{
    [Header("Detectar Jugador")]
    [SerializeField] private float radio;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform Jugador;
    [Header("Fuerzas y distancia")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float velocidad;
    [SerializeField] private float distanciaMaxima;
    [SerializeField] private Vector3 PuntoInicial;
    [Header("Girar")]
    [SerializeField] private bool mirandoDerecha;
    [Header("Animator")]
    [SerializeField] private Animator animator;


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
        Collider2D circulo = Physics2D.OverlapCircle(transform.position, radio, layer);

        if (circulo)
        {
            Jugador = circulo.transform;

            estadoActual = EstadoMovimiento.Siguiendo;

            animator.SetBool("Run", true);
        }
    }

    public void EstadoSiguiendo()
    {
        if (Jugador == null)
        {
            estadoActual = EstadoMovimiento.Volviendo;
            return;
        }

        if(transform.position.x < Jugador.position.x)
        {
            rb.velocity = new Vector2(velocidad,rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-velocidad, rb.velocity.y);
        }

        GirarRinoceronte(Jugador.position);

        if (Vector2.Distance(transform.position, PuntoInicial) > distanciaMaxima || Vector2.Distance(transform.position, Jugador.position) > distanciaMaxima)
        {
            estadoActual = EstadoMovimiento.Volviendo;
            Jugador = null;
        }
    }

    public void EstadoVolviendo()
    {
        if (transform.position.x < PuntoInicial.x)
        {
            rb.velocity = new Vector2(velocidad, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-velocidad, rb.velocity.y);
        }

        GirarRinoceronte(PuntoInicial);

        if (Vector2.Distance(transform.position, PuntoInicial) < 0.1f)
        {
            rb.velocity = Vector2.zero;

            estadoActual = EstadoMovimiento.Esperando;

            animator.SetBool("Run", false);
        }
    }

    public void GirarRinoceronte(Vector3 objetivo)
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
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, radio);
        Gizmos.DrawWireSphere(PuntoInicial, distanciaMaxima);
    }
}
