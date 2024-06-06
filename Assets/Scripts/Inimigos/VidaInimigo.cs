using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaInimigo : MonoBehaviour
{
    public float health;

    private MoverEAtirar moverEAtirar;
    private GameObject[] enemys;

    private Transform vida;
    private Vector2 localScale;

    void Awake()
    {
        moverEAtirar = GetComponent<MoverEAtirar>();

        vida = this.transform.Find("vida");
        localScale = vida.localScale;
    }

    void Update()
    {
        Morrer();
        localScale.x = (health / 100) * 4;
        vida.localScale = localScale;
    }

    public void TomarDano()
    {
        if (GameEventHub.inimigo == this.gameObject)
        {
            health -= GameEventHub.danoFlecha;
        }
    }

    private void Morrer()
    {
        if (health <= 0 || this.transform.position.y <= -50)
        {
            Destroy(this.gameObject);
        }
    }
}
