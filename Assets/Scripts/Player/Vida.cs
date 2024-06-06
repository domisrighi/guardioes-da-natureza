using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public float health;
    public int coracoes;
    public GameEvent morreu;
    [HideInInspector] public bool dead;
    [HideInInspector] public float initHealth;
    [HideInInspector] public int initCoracoes;

    private CheckpointsP CheckpointsP;

    void Awake()
    {
        dead = false;
        initHealth = health;
        initCoracoes = coracoes;

        CheckpointsP = GameObject.FindWithTag("CheckpointP").GetComponent<CheckpointsP>();
    }

    void Update()
    {
        if (this.gameObject.transform.position.y <= -50)
        {
            if (coracoes <= 0)
            {
                dead = true;
                morreu.Raise();
                return;
            }

            health = initHealth;
            coracoes -= 1;

            CheckpointsP.NascerCheckpoint();
            return;
        }
    }

    public void TomarDano()
    {
        //Se vida for menor ou igual a zero sai da função
        if (health <= 0)
        {
            return;
        }

        health -= GameEventHub.DanoPLayer.Dano; //Subtrai a vida pelo dano definido no GameEventHub

        //Se vida esvazia mas ainda tem corações, gasta uma vida
        if (health <= 0 && coracoes != 0)
        {
            health = initHealth;
            coracoes -= 1;
        }

        //Se vida esvazia e não tem corações, morre
        if (health <= 0 && coracoes == 0)
        {
            dead = true;
            morreu.Raise();
        }
    }

    public void Curar()
    {
        if (health != initHealth)
        {
            health = health + GameEventHub.Cura > initHealth ? initHealth : health + GameEventHub.Cura; //Cura definida no GameEventHub
        }
    }

    public void vidaPlus()
    {
        coracoes += 1; //Aumenta os corações
    }
}
