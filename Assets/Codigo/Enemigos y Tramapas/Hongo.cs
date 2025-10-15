using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hongo : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField]private Transform[] Puntos;
    [SerializeField]private SpriteRenderer spriteR;
    [SerializeField]private Animator animator;
    private int indice;
    [Header("Fuerzas")]
    [SerializeField, Range(1f, 10f)] private float velocidad;
    [SerializeField]private float Esperar;
    private float EsperarVariable;

    private void Start()
    {
        EsperarVariable = Esperar;
    }

    private void Update()
    {
        IndiceCambiar();
        Movimiento();
        Rotar();
    }

    public void Movimiento()
    {
        transform.position = Vector2.MoveTowards(transform.position, Puntos[indice].position,velocidad * Time.deltaTime);
    }

    public void IndiceCambiar()
    {
        if (Vector2.Distance(transform.position, Puntos[indice].position) <= 0.1f)
        {
            if(EsperarVariable <= 0f)
            {
                animator.SetBool("Esperar", false);
                indice++;
                EsperarVariable = Esperar;
                if (indice >= Puntos.Length)
                {
                    indice = 0;
                }
            }
            else
            {
                animator.SetBool("Esperar", true);
                EsperarVariable -= Time.deltaTime;
            }
            
        }
    }

    public void Rotar()
    {
        StartCoroutine(FlipX());
    }

    IEnumerator FlipX()
    {
        Vector2 posicionInicial = transform.position;
        yield return new WaitForSeconds(0.2f);
        if(posicionInicial.x > transform.position.x)
        {
            spriteR.flipX = false;
        }
        else if(posicionInicial.x < transform.position.x)
        {
            spriteR.flipX = true;
        }
    }
}
