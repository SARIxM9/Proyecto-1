using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruta : MonoBehaviour
{
    [SerializeField] private GameObject Polvo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioManager.Instance.SFXreproducir(3);
            Destroy(gameObject);
            Instantiate(Polvo,transform.position, transform.rotation);
            GameManager.Instance.SumarFruta();
        }
    }
}
