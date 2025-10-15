using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Jugador_Control : MonoBehaviour
{
    public static Jugador_Control Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [Header("Atributos")]
    public Rigidbody2D rb;
    [SerializeField] private Piso piso;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteR;

    [Header("Fuerzas")]
    [SerializeField,Range(5f,10f)] private float velocidad;
    [SerializeField,Range(10f,20f)] private float impulso;
    [SerializeField] private Vector2 Empujar;

    [Header("Vidas del Jugador")]
    public int Vidas;
    public int VidasVariable;

    [Header("Aturdimiento al perder vidas")]
    public bool Stun = false;
    [SerializeField] private float ContadorStun;


    [Header("Invencibilidad")]
    [SerializeField] private float DuracionInvencibilidad;
    [SerializeField] public float Invencible;



    private void Start()
    {
        VidasVariable = Vidas;
    }

    void Update()
    {
        if (!Stun)
        {
            Movimiento();
            Salto();
        }
        Animaciones();

        if(Invencible > 0f)
        {
            Invencible -= Time.deltaTime;
        }

        InvecibilidadAnimacion();
    }

    private void Movimiento()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * velocidad, rb.velocity.y);
    }

    private void Salto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && piso.Suelo)
        {
            AudioManager.Instance.SFXreproducir(5);
            rb.velocity = new Vector2(rb.velocity.x,impulso);
        }
    }

    private void Animaciones()
    {
        //Caminar y rotar
        if(rb.velocity.x < 0f)
        {
            spriteR.flipX = true;
        }
        else if (rb.velocity.x > 0f)
        {
            spriteR.flipX = false;
        }

        animator.SetFloat("Run",Mathf.Abs(rb.velocity.x));

        //Saltar

        animator.SetBool("Piso",piso.Suelo);

        //Caer

        animator.SetFloat("Down",rb.velocity.y);


    }

    public void MorirAnimacion()
    {
        animator.Play("Dead");
    }

    //Metodos sobre la vida

    public void RestarVida(int quitarVida)
    {
        if (Invencible <= 0f)
        {
            AudioManager.Instance.SFXreproducir(4);
            MorirAnimacion();
            Stuneado();
            VidasVariable -= quitarVida;
            if (VidasVariable <= 0)
            {
                AudioManager.Instance.SFXreproducir(4);
                GameManager.Instance.Respawn();
                VidasVariable = Vidas;
            }
            Invencible = DuracionInvencibilidad;
        }
    }
    //Aturdimiento

    public void Stuneado()
    {
        StartCoroutine(Stunear());
    }

    IEnumerator Stunear()
    {
        Stun = true;
        if(rb.velocity.x > 0f)
        {
            rb.velocity = new Vector2(-Empujar.x,Empujar.y);
        }
        else if (rb.velocity.x < 0f)
        {
            rb.velocity = new Vector2(Empujar.x, Empujar.y);
        }
        else if (rb.velocity.x == 0f)
        {
            rb.velocity = new Vector2(0f, Empujar.y);
        }
        yield return new WaitForSeconds(ContadorStun);
        Stun = false;
    }

    //Parpadear

    public void InvecibilidadAnimacion()
    {
        if (Invencible > 0f && Stun == false)
        {
            if(Mathf.Floor(Invencible * 5) % 2 == 0)
            {
                spriteR.enabled = true;
            }
            else if(Mathf.Floor(Invencible * 5 ) % 2 != 0)
            {
                spriteR.enabled = false;
            }
            if(Invencible <= 0f)
            {
                spriteR.enabled = false;
            }
        }
    }
}
