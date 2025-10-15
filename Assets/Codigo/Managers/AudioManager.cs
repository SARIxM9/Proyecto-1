using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("Sonido del juego")]
    public AudioSource[] Musica;
    public AudioSource[] SFX;

    private void Start()
    {
        Musicareproducir(0);
        DontDestroyOnLoad(gameObject);
    }

    public void Musicareproducir(int musica)
    {
        Musica[musica].Play();
    }

    public void SFXreproducir(int sfx)
    {
        SFX[sfx].Play();
    }


}
