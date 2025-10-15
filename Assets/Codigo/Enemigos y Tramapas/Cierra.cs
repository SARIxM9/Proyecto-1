using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cierra : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private Transform[] Puntos;
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
        IndiceCambiar();
        Movimiento();
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
}
