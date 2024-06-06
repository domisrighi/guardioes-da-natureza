using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegeneracaoVida : MonoBehaviour
{
    public float cura;
    public GameEvent Curar;

    //Quando o gatilho é ativado, verifica se o objeto é o player e chama o evento para curar
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameEventHub.Cura = cura;
            Curar.Raise();
            Destroy(this.gameObject);
        }
    }
}
