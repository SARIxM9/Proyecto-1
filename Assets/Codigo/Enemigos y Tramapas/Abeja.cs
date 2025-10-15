using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class Abeja : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private Transform[] Puntos;
    [SerializeField] private Animator animator;
    private int indice;
    [Header("Fuerzas")]
    [SerializeField, Range(1f, 10f)] private float velocidad;
    [SerializeField] private float Esperar;
    private float EsperarVariable;
    [Header("Rayo")]
    [SerializeField] private Transform Rayo;
    [SerializeField] private RaycastHit2D raycast;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float distancia;
    [SerializeField] private GameObject Bala;
    [SerializeField] private float tiempoEntreDisparos;
    [SerializeField] private float tiempoUltimoDisparos;
    [SerializeField] private bool detener;

    private void Start()
    {
        EsperarVariable = Esperar;
    }

    private void Update()
    {
        IndiceCambiar();
        if (!detener)
        {
            Movimiento();
        }
        else
        {
            return;
        }
        RayoDetectar();
    }

    public void Movimiento()
    {
        transform.position = Vector2.MoveTowards(transform.position, Puntos[indice].position, velocidad * Time.deltaTime);
    }

    public void IndiceCambiar()
    {
        if (Vector2.Distance(transform.position, Puntos[indice].position) <= 0.1f)
        {
            if (EsperarVariable <= 0f)
            {
                indice++;
                EsperarVariable = Esperar;
                if (indice >= Puntos.Length)
                {
                    indice = 0;
                }
            }
            else
            {
                EsperarVariable -= Time.deltaTime;
            }

        }
    }

    public void RayoDetectar()
    {
        raycast = Physics2D.Raycast(Rayo.position, Vector2.down, distancia,mask);

        if(raycast)
        {
            if(Time.time > tiempoEntreDisparos + tiempoUltimoDisparos)
            {
                animator.SetTrigger("Atak");
                detener = true;
                Invoke("TirarBala", 0.5f);
                tiempoUltimoDisparos = Time.time;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(Rayo.position,Vector2.down * distancia,Color.white);
        if(raycast)
        {
            Debug.DrawRay(Rayo.position, Vector2.down * distancia, Color.black);
        }
    }

    public void TirarBala()
    {
        Instantiate(Bala, Rayo.position, Quaternion.identity);
        detener = false;
    }
}
