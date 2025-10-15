using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yunque_Espinoso : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float distancia;
    [SerializeField] private LayerMask layer;
    [SerializeField] private RaycastHit2D hit;
    [Header("Subir")]
    [SerializeField] private bool Subiendo;
    [SerializeField] private float velocidadSubida;
    [Header("Animator")]
    [SerializeField] private Animator animator;
    [SerializeField] private float Esperar;


    void Update()
    {
        Rayo();
    }

    public void Rayo()
    {
        hit = Physics2D.Raycast(transform.position,Vector2.down,distancia,layer);

        if(hit && !Subiendo)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        if(Subiendo)
        {
            transform.Translate(Vector2.up * velocidadSubida * Time.deltaTime);
        }
    }


    IEnumerator EsperarSuelo()
    {
        yield return new WaitForSeconds(Esperar);
        Subiendo = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Pallete")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            if (Subiendo)
            {
                Subiendo = false;
            }
            else
            {
                animator.SetTrigger("Down");
                AudioManager.Instance.SFXreproducir(0);
                StartCoroutine(EsperarSuelo());
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector2.down * distancia, Color.black);

        if (hit)
        {
            Debug.DrawRay(transform.position, Vector2.down * distancia, Color.white);
        }
    }

}
