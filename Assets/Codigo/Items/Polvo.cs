using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polvo : MonoBehaviour
{
    [SerializeField,Range(0.2f,1)] private float TimeVida;
    void Start()
    {
        Destroy(gameObject,TimeVida);
    }
}
