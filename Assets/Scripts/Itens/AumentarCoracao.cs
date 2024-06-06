using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumentarCoracao : MonoBehaviour
{
    public Vida vida;
    public GameEvent vidaPlus;

    //Quando o gatilho é ativado, verifica se o objeto é o player e chama o evento para aumentar a vida
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            vidaPlus.Raise();
        }
    }
}
