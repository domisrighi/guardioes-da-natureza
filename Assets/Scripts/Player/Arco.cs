using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arco : MonoBehaviour
{
    [Header("Flecha")]
    public float forcaFlecha;
    public GameObject flecha;
    public Transform shotPoint;
    [Header("Trajetória da flecha")]
    public Transform pai;
    public GameObject ponto;
    public int quantidadePontos;
    public float espacoEntrePontos;
    private GameObject[] pontos;
    private Vector2 direcao;

    void Awake()
    {
        //Cria a trajetória da flecha e define o pai como um GameObject vazio
        pontos = new GameObject[quantidadePontos];

        for (int i = 0; i < quantidadePontos; i++)
        {
            pontos[i] = Instantiate(ponto, shotPoint.position, Quaternion.identity);
            pontos[i].transform.SetParent(pai);
        }
    }

    void Update()
    {
        //Rotaciona o arco em direção ao mouse
        Vector2 arcoPos = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //posição do mouse é definida em Pixels; é necessário converter para Euler
        direcao = mousePos - arcoPos;
        transform.right = direcao;

        if (Input.GetButtonDown("Atirar"))
        {
            Atirar();
        }

        for (int i = 0; i < quantidadePontos; i++)
        {
            pontos[i].transform.position = PointPosition(i * espacoEntrePontos); //Define a posição dos pontos
        }
    }

    //Instancia uma flecha e aplica uma força nela
    private void Atirar()
    {
        GameObject novaFlecha = Instantiate(flecha, shotPoint.position, shotPoint.rotation);
        Rigidbody2D rb = novaFlecha.GetComponent<Rigidbody2D>();

        rb.AddForce(transform.right * forcaFlecha, ForceMode2D.Impulse);
    }

    //Define a trajetória da flecha em certo ponto no tempo
    private Vector2 PointPosition(float t)
    {
        return (Vector2)shotPoint.position + (direcao.normalized * forcaFlecha * t) + 0.5f * Physics2D.gravity * (t * t);
    }
}
