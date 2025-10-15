using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaGrande : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private Transform[] Puntos;
    [SerializeField] private Animator animator;
    [SerializeField] private bool Activar;
    private int indice;
    [Header("Fuerzas")]
    [SerializeField, Range(1f, 10f)] private float velocidad;
    [SerializeField] private float Esperar;
    private float EsperarVariable;

    private void Start()
    {
        EsperarVariable = Esperar;
    }

    private void Update()
    {
        if (Activar)
        {
            IndiceCambiar();
            Movimiento();
        }
    }

    public void Movimiento()
    {
        transform.position = Vector2.MoveTowards(transform.position, Puntos[indice].position, velocidad * Time.deltaTime);
    }

    public void IndiceCambiar()
    {
        if (Vector2.Distance(transform.position, Puntos[indice].position) < 0.05f)
        {
            if (EsperarVariable <= 0f)
            {
                animator.SetBool("Run", true);

                indice++;
                EsperarVariable = Esperar;
                if (indice >= Puntos.Length)
                {
                    indice = 0;
                }
            }
            else
            {
                animator.SetBool("Run", false);
                EsperarVariable -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Activar = true;
            animator.SetBool("Run", true);
            Jugador_Control.Instance.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Jugador_Control.Instance.transform.parent = null;
        }
    }

}
