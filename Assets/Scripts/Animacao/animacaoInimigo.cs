using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animacaoInimigo : MonoBehaviour
{
    private bool moving;
    private bool estaEspelhado;
    private Vector2 direction;
    private Animator animator;
    private MoverEAtirar moverEAtirar;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        moverEAtirar = GetComponent<MoverEAtirar>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        estaEspelhado = false;
    }

    void Update()
    {
        moving = moverEAtirar.moving;
        direction = moverEAtirar.direction;

        if (moving)
        {
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }

        if (direction.x > 0 && spriteRenderer.flipX || direction.x < 0 && !spriteRenderer.flipX)
        {
            flipX();
        }

    }

    void flipX()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        estaEspelhado = !estaEspelhado;
    }
}
