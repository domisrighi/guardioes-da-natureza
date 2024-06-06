using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UserInterface : MonoBehaviour
{
    [Header("Vida")]
    public Image fillBarHealth;
    public Text contadorVida;
    private Vida vida;

    [Header("Corações")]
    public RectTransform HeartsUI;
    public Sprite heartsSprite;
    private Text text;

    [Header("Dash")]
    public Image fillBarDash;
    private MoverPersonagem personagem;

    public Text textoFinal;

    void Awake()
    {
        personagem = GameObject.FindGameObjectWithTag("Player").GetComponent<MoverPersonagem>();
        vida = GameObject.FindGameObjectWithTag("Player").GetComponent<Vida>();

        CriarCoracoes(vida.coracoes);//Cria os primeiros corações
        CriarContadorCoracoes();
    }

    void Update()
    {
        TempoDash();
        AtualizarVida();
        AtualizarCoracoes();
    }

    public void Criar1Coracao() //Cria um coração e atualiza o contador
    {
        CriarCoracoes(1);
        AtualizarContador();
    }

    private void CriarCoracoes(int controle)
    {
        for (int i = 0; i < controle; i++) //Loop para controlar quantos corações cria de uma vez
        {
            //Cria objeto vazio, altera o nome para "Vidas" e define o pai
            GameObject Vidas = new GameObject();
            Vidas.name = $"Coracao {i + 1:0#}";
            Vidas.transform.SetParent(HeartsUI.transform);

            //Adiciona o componente "Imagem" ao objeto criado anteriormente, define o sprite e ativa "preservar aspecto"
            Image imageComponent = Vidas.AddComponent(typeof(Image)) as Image;
            Vidas.GetComponent<Image>().sprite = heartsSprite;
            Vidas.GetComponent<Image>().preserveAspect = true;

            //Define a posição para a mesma do pai e define a escala
            Vidas.transform.position = HeartsUI.transform.position;
            Vidas.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
    }

    private void CriarContadorCoracoes()
    {
        //Cria um objeto vazio, altera o nome para "ContadorVidas" e define o pai
        GameObject contadorVidas = new GameObject();
        contadorVidas.name = "ContadorVidas";
        contadorVidas.transform.SetParent(HeartsUI.transform);

        //Adiciona o componente "contadorVida" ao objeto criado anteriormente, define a fonte, o tamanho e o contadorVida
        Text textoVidas = contadorVidas.AddComponent(typeof(Text)) as Text;
        text = contadorVidas.GetComponent<Text>();

        text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        text.fontSize = 22;
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.text = $"X{vida.coracoes}";

        //Define a posição para a mesma do pai e define a escala
        contadorVidas.transform.position = HeartsUI.transform.position;
        contadorVidas.transform.localScale = Vector3.one;
    }

    public void AtualizarVida() //Atualiza a vida 
    {
        contadorVida.text = $"{(int)vida.health}";
        fillBarHealth.fillAmount = vida.health / vida.initHealth; //Converte a escala de 0 - vidaMáxima para 0 - 1
    }
    public void AtualizarContador() //Atualiza o contador
    {
        text.text = $"X{vida.coracoes}";
        text.transform.SetAsLastSibling();
    }

    public void AtualizarCoracoes()
    {
        //Cria uma lista e adiciona todos os filhos do objecto "HeartsUI" nela caso tenham o componente "Image"
        List<GameObject> coracoes = new List<GameObject>();

        foreach (Transform child in HeartsUI.transform)
        {
            if (child.GetComponent<Image>() != null)
                coracoes.Add(child.gameObject);
        }

        //Se a quantidade de corações na variável for menor do que a quantidade na tela, destroi um coração na tela e altera o contador;
        if (vida.coracoes < coracoes.Count)
        {
            Destroy(coracoes[coracoes.Count - 1].gameObject);
            AtualizarContador();
        }
    }

    public void TempoDash()
    {
        fillBarDash.fillAmount = (personagem.initialTimer - personagem.cdDash) / personagem.initialTimer; //Atualiza o timer do Dash
    }

    public void Morte()
    {
        textoFinal.text = "Você morreu!";
    }
}
