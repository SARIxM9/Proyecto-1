using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatarEnemigo : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private Vector2 Impulso;
    [SerializeField] private GameObject Polvo;
    [SerializeField] private Animator animator;
    [Header("Vidas")]
    [SerializeField] private int Vidas;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vidas--;
            AudioManager.Instance.SFXreproducir(0);
            animator.Play("Dead");
            Jugador_Control.Instance.rb.velocity = Impulso;
            if(Vidas <= 0f)
            {
                Destroy(gameObject);
                Instantiate(Polvo,transform.position, Quaternion.identity);
            }
        }
    }
}
