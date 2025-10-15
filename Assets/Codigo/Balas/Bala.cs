using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float TiempoVida;
    [SerializeField] private float velocidad;
    [SerializeField] private Vector2 direccion;
    void Start()
    {
        Destroy(gameObject,TiempoVida);
    }

    private void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }
}
