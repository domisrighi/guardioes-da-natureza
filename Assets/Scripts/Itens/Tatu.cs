using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tatu : MonoBehaviour
{
    public Sprite spriteFree;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Flecha"))
        {
            spriteRenderer.sprite = spriteFree;
            boxCollider2D.enabled = false;
            Destroy(col.gameObject);
        }
    }
}
