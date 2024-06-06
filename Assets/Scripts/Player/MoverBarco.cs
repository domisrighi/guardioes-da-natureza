using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBarco : MonoBehaviour
{
    public float velocidade;
    public float multiplicador;

    private Rigidbody2D rb;
    private float X;
       
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(transform.position.x, 110f);
    }

    void Update()
    {
        X = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Horizontal"))
        {
            rb.velocity = new Vector2(velocidade * multiplicador * X, rb.position.y);

            if (X > 0)
                transform.eulerAngles = Vector3.zero;
            else if (X < 0)
                transform.eulerAngles = Vector3.up * 180f;
        }

        AjustesFisicos();
    }

    private void AjustesFisicos()
    {
        // Remove o momentum do personagem
        if (Input.GetButtonUp("Horizontal"))
            rb.velocity = new Vector2(0, rb.velocity.y);
    }
}
