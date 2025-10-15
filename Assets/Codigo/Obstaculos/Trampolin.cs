using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Vector2 Impulzo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.Play("Jump");
            AudioManager.Instance.SFXreproducir(2);
            Jugador_Control.Instance.rb.velocity = Impulzo;
        }
    }
}
