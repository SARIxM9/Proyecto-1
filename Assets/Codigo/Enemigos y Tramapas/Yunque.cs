using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Yunque : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField]private Animator animator;
    [SerializeField]private Vector2 PuntoOrigen;
    [SerializeField]private Transform Punto;
    [SerializeField]private float velocidadIda;
    [SerializeField]private float velocidadVuelta;
    [SerializeField]private bool Detectar;
    [SerializeField]private bool Ajustes;
    [SerializeField] private string Animacion;
    [Header("RayCast")]
    [SerializeField]private RaycastHit2D hit;
    [SerializeField]private LayerMask layer;
    [SerializeField]private Vector2 direccion;
    [SerializeField]private float distancia;
    [Header("Reposo")]
    [SerializeField] private float Reposo;
    private float ReposoVariable;


    private void Start()
    {
        PuntoOrigen = transform.position;
        ReposoVariable = Reposo;
    }

    private void Update()
    {
        Rayo();
        Mover();
        Regresar();
    }

    public void Mover()
    {
        if (Detectar)
        {
            transform.position = Vector2.MoveTowards(transform.position, Punto.position, velocidadIda * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, PuntoOrigen, velocidadVuelta * Time.deltaTime);
        }
    }

    public void Regresar()
    {
        if (Vector2.Distance(transform.position, Punto.position) <= 0.1f)
        {
            if(Ajustes)
            {
                switch (Animacion)
                {
                    case "Up":
                        animator.Play("Up");
                        break;
                    case "Right":
                        animator.Play("Right");
                        break;
                    case "Down":
                        animator.Play("Down");
                        break;
                    case "Left":
                        animator.Play("Left");
                        break;
                }
                AudioManager.Instance.SFXreproducir(0);
                Ajustes = false;
            }
            
            if (ReposoVariable <= 0f)
            {
                animator.SetTrigger("Regresar");
                Detectar = false;
                ReposoVariable = Reposo;
            }
            else
            {
                ReposoVariable -= Time.deltaTime;
            }
        }
    }
    public void Rayo()
    {
        hit = Physics2D.Raycast(transform.position, direccion, distancia, layer);

        if (hit)
        {
            if(Vector2.Distance(transform.position, PuntoOrigen) <= 0.1f)
            {
                Detectar = true;
                Ajustes = true;
            }
        }
    }



    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, direccion * distancia, Color.black);

        if(hit)
        {
            Debug.DrawRay(transform.position, direccion * distancia, Color.white);
        }
    }
}
