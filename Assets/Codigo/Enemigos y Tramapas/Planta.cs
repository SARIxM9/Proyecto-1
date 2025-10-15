using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planta : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private Animator animator;
    [Header("Rayo")]
    [SerializeField] private Transform Rayo;
    [SerializeField] private RaycastHit2D raycast;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float distancia;
    [Header("CoolDown")]
    [SerializeField] private float tiempoEntreDisparos;
    [SerializeField] private float tiempoUltimoDisparos;
    [Header("Proyectil")]
    [SerializeField] private GameObject Bala;
    [SerializeField] private float DelayBala;

    private void Update()
    {
        RayoDetectar();
    }

    public void RayoDetectar()
    {
        raycast = Physics2D.Raycast(Rayo.position, Vector2.left, distancia, mask);

        if (raycast)
        {
            if (Time.time > tiempoEntreDisparos + tiempoUltimoDisparos)
            {
                animator.SetTrigger("Atak");
                Invoke("TirarBala", DelayBala);
                tiempoUltimoDisparos = Time.time;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(Rayo.position, Vector2.left * distancia, Color.white);
        if (raycast)
        {
            Debug.DrawRay(Rayo.position, Vector2.left * distancia, Color.black);
        }
    }

    public void TirarBala()
    {
        Instantiate(Bala, Rayo.position, Quaternion.identity);
    }
}

