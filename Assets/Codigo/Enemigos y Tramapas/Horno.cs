using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horno : MonoBehaviour
{
    [SerializeField]private Animator animator;
    [SerializeField]private BoxCollider2D box;
    [SerializeField]private GameObject Fuego;
    [SerializeField]private bool Inicio;
    [SerializeField]private float Duracion;
    private float DuracionVariable;

    private void Start()
    {
        DuracionVariable = Duracion;
    }

    private void Update()
    {
        Apagar();
    }

    public void Apagar()
    {
        if (Inicio)
        {
            if(DuracionVariable <= 0f)
            {
                animator.SetTrigger("Apagar");
                DuracionVariable = Duracion;
                Inicio = false;
                StartCoroutine(FuegoApagar());
            }
            else
            {
                DuracionVariable -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            animator.SetTrigger("Encender");
            Inicio = true;
            StartCoroutine(FuegoEncender());
            box.enabled = false;
        }
    }

    IEnumerator FuegoEncender()
    {
        yield return new WaitForSeconds(0.4f);
        Fuego.SetActive(true);
    }
    IEnumerator FuegoApagar()
    {
        yield return new WaitForSeconds(0.4f);
        Fuego.SetActive(false);
        box.enabled = true;
    }
}
