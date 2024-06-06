using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixa : MonoBehaviour
{
    public List<Sprite> sprites;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        //sprites = new List<Sprite>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        RandomSprite(sprites.Count);
    }

    void Update()
    {
        if (this.transform.position.y <= -50)
        {
            Destroy(this.gameObject);
        }
    }

    private void RandomSprite(int qtd)
    {
        int random = Random.Range(0, qtd);
        spriteRenderer.sprite = sprites[random];
    }
}
