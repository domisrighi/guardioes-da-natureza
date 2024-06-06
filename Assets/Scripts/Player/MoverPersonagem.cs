using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPersonagem : MonoBehaviour
{
    //Movimentacao X
    [Header("Movimentação e Pulo")]
    public int velocidade;
    //Pulo
    [Space(10)]
    public float forcaPulo;
    public float tempoNoAr;
    private float jumpTimeCounter;
    private bool isJumping;
    private bool taNoChao;

    //Dash
    [Header("Dash")]
    public float duracaoDash;
    public float forcaDash;
    public float cdDash;
    private float tempoAtualDash;
    public float initialTimer;
    private bool dashing;

    [Header("Camada")]
    public LayerMask chao;

    //Componentes
    private Rigidbody2D rb;
    private BoxCollider2D box;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        initialTimer = cdDash;
    }

    void Update()
    {
        //Verifica se o jogador esta no chão
        taNoChao = Physics2D.OverlapArea(new Vector2(box.transform.position.x - 0.5f * box.size.x * transform.localScale.x, box.transform.position.y - 0.5f * box.size.y * transform.localScale.y),
                    new Vector2(box.transform.position.x + 0.5f * box.size.x * transform.localScale.x, box.transform.position.y - 0.5f * box.size.y * transform.localScale.y), chao);

        //Decrementa o timer do Dash
        cdDash = cdDash > 0 ? cdDash - Time.deltaTime : 0;
    }

    void FixedUpdate()
    {
        //Movimento horizontal do personagem
        float X = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(X * velocidade, rb.velocity.y);

        Dash(X);
        Pulo();
        AjustesFisicos();
    }

    //Dash do Personagem; É Necessário segurar a tecla direcional
    private void Dash(float dirX)
    {
        if (Input.GetButtonDown("Dash") && dirX != 0 && cdDash <= 0)
        {
            dashing = true;
            tempoAtualDash = duracaoDash;
        }

        if (dashing)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(transform.right * dirX * forcaDash, ForceMode2D.Impulse);
            tempoAtualDash -= Time.fixedDeltaTime;

            if (tempoAtualDash <= 0)
            {
                dashing = false;
                cdDash = initialTimer;
            }
        }
    }

    //Pulo do personagem
    private void Pulo()
    {
        //Pula somente quando aperta o botão e está no chão
        if (Input.GetButtonDown("Jump") && taNoChao)
        {
            isJumping = true;
            jumpTimeCounter = tempoNoAr;
            rb.velocity = new Vector2(rb.velocity.x, forcaPulo);
        }
        //Pula mais alto se segurar o botão de pulo
        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, forcaPulo);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        isJumping = Input.GetButtonUp("Jump") ? false : isJumping;
    }

    private void AjustesFisicos()
    {
        // Remove o momentum do personagem
        if (Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = new Vector3(0, rb.velocity.y);
        }
    }
}