using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleAnimacao : MonoBehaviour
{
    private bool estaEspelhado;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ((Input.GetAxis("Horizontal") > 0 && sprite.flipX) || (Input.GetAxis("Horizontal") < 0 && !sprite.flipX))
        {
            flipX();
        }

        if (rb.velocity != Vector2.zero)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        if (Input.GetButtonDown("Atirar"))
        {
            animator.SetBool("ataque", true);
        }
        else
        {
            animator.SetBool("ataque", false);
        }
    }

    private void flipX()
    {
        sprite.flipX = !sprite.flipX;
        estaEspelhado = !estaEspelhado;
    }

    public void Morreu()
    {
        animator.SetTrigger("dead");
    }
}
