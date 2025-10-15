using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatarJugador : MonoBehaviour
{
    [SerializeField] private int QuitarVida;
    [SerializeField] private bool reiniciar;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(reiniciar)
            {
                CanvasManager.Instance.ReiniciarSecundario();
            }
            else
            {
                Jugador_Control.Instance.RestarVida(QuitarVida);
                CanvasManager.Instance.Vidas(Jugador_Control.Instance.VidasVariable);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (reiniciar)
            {
                CanvasManager.Instance.ReiniciarSecundario();
            }
            else
            {
                Jugador_Control.Instance.RestarVida(QuitarVida);
                CanvasManager.Instance.Vidas(Jugador_Control.Instance.VidasVariable);
            }
        }
    }
}
