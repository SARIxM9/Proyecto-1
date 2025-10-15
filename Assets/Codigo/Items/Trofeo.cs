using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trofeo : MonoBehaviour
{
    [SerializeField]private Animator animator;
    [SerializeField]private string Nivel;
    [SerializeField]private bool final;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (final)
            {
                AudioManager.Instance.SFXreproducir(6);
                CanvasManager.Instance.TransicionJuegoFinal(Nivel);
                CanvasManager.Instance.ReiniciarCanvas();
                animator.SetTrigger("Listo");
            }
            else
            {
                AudioManager.Instance.SFXreproducir(6);
                CanvasManager.Instance.TransicionJuego(Nivel);
                CanvasManager.Instance.ReiniciarCanvas();
                animator.SetTrigger("Listo");
            }
            
        }
    }
}
