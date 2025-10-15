using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piso : MonoBehaviour
{
    public bool Suelo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pallete"))
        {
            Suelo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pallete"))
        {
            Suelo = false;
        }
    }
}
