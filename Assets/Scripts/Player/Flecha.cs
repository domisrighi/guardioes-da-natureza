using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public float tempoAtirando;
    public float tempoOcioso;
    public float dano;
    public GameEvent danoFlecha;

    private Rigidbody2D rb;
    [HideInInspector] public bool hasHit;
    float tempo;
    float tempoRestante;
    private float tempoOciosoRestante;
    private float tempoAtirandoRestante;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        tempoOciosoRestante = tempoOcioso; //Define o tempo inicial do timerOcioso
        tempoAtirandoRestante = tempoAtirando; //Define o tempo inicial do timerAtirando
    }

    void Update()
    {
        if (!hasHit)
        {
            //Rotaciona a flecha na direção da trajetória
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //Timer para destruir a flecha se ficar no ar por tempo determinado
            tempoAtirandoRestante -= tempoAtirandoRestante > 0 ? Time.deltaTime : 0;
            if (tempoAtirandoRestante <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            //Timer para destruir a flecha se ficar no chao por tempo determinado
            tempoOciosoRestante -= tempoOciosoRestante > 0 ? Time.deltaTime : 0;
            if (tempoOciosoRestante <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    //Ao colidir com algo, fica grudada
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Caixa"))
        {
            this.transform.SetParent(collision.transform);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameEventHub.danoFlecha = dano;
            Destroy(this.gameObject);
            GameEventHub.inimigo = collision.gameObject;
            danoFlecha.Raise();
        }
        else if (collision.gameObject.layer != 9)
        {
            hasHit = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            gameObject.layer = 8;

            if (collision.gameObject.CompareTag("Flecha"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
