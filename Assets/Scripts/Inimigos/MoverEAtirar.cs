using UnityEngine;

public class MoverEAtirar : MonoBehaviour
{
    [Header("Movimentação")]
    public float velocidade;

    [Header("Tiro")]
    public float tempoEntreTiros;
    public float tempoRecarregar;
    public int pente;
    public int numeroProjeteis;
    public int danoPorTiro;

    private bool recarregando;
    private float currentTime;
    private int balasNoPente;
    private float proximoTiro;

    [Header("Componentes")]
    public FieldOfView fov;
    //public Eventos Eventos;
    public GameObject Tiro;
    private Rigidbody2D rb;
    [HideInInspector] public bool moving;
    [HideInInspector] public Vector2 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        fov = GetComponentInChildren<FieldOfView>(); //Pega o componente no GameObject filho

        recarregando = false;
        currentTime = tempoRecarregar;
        balasNoPente = pente;

        moving = false;
    }

    void Update()
    {
        AjustesFisicos();
        Timer();

        if (fov.alvoVisivel == null)
        {
            moving = false;
        }
    }

    public void SeguirAlvo()
    {
        if (GameEventHub.inimigo == this.gameObject)
        {
            direction = (GameEventHub.SeguirAlvo.alvo.position - transform.position).normalized;
            if (Vector2.Distance(transform.position, GameEventHub.SeguirAlvo.alvo.position) > GameEventHub.SeguirAlvo.distanciaMax)
            {
                float yVel = rb.velocity.y;
                rb.velocity = direction * velocidade;
                rb.velocity = new Vector2(rb.velocity.x, yVel);
                moving = true;
            }
            else
            {
                moving = false;
            }
        }
    }

    //Metodo a ser melhorado no futuro;
    public void Atirar()
    {
        if (fov.SeguirAlvo != null && !recarregando && GameEventHub.inimigo == this.gameObject)
        {
            if (proximoTiro < Time.time)
            {
                GameEventHub.DanoPLayer.Dano = danoPorTiro;

                for (int i = 0; i < numeroProjeteis; i++)
                {
                    AtirarCartucho();
                    proximoTiro = Time.deltaTime + tempoEntreTiros;
                }

                balasNoPente--;
                proximoTiro = Time.time + tempoEntreTiros;
            }

            if (balasNoPente == 0)
            {
                recarregando = true;
            }
        }
    }

    //Cria os projéteis com um intervalo de tempo entre um tiro e outro, e recarrega se o pente está vazio
    public void AtirarEspingarda()
    {
        if (fov.SeguirAlvo != null && !recarregando && GameEventHub.inimigo == this.gameObject)
        {
            if (proximoTiro < Time.time)
            {
                int sequence = Mathf.CeilToInt(-numeroProjeteis / 2);
                GameEventHub.DanoPLayer.Dano = danoPorTiro;

                for (int i = sequence; i <= sequence * -1; i++)
                {
                    AtirarCartucho((fov.AnguloDoTiro / numeroProjeteis) * i);
                }

                balasNoPente--;
                proximoTiro = Time.time + tempoEntreTiros;
            }

            if (balasNoPente == 0)
            {
                recarregando = true;
            }
        }
    }

    //Cria a rotação para a dispersão do projétil
    private void AtirarCartucho(float angleOffset = 0f)
    {
        GameObject bullet = Instantiate(Tiro);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        Tiro tiro = bullet.GetComponent<Tiro>();

        bullet.transform.position = rb.position;
        rigidbody.AddForce(Quaternion.AngleAxis(angleOffset, Vector3.forward) * fov.transform.up * tiro.speed, ForceMode2D.Impulse);
    }

    private void AjustesFisicos()
    {
        if (rb.velocity.x != 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y); //Remove o momentum
        }
    }

    private void Timer()
    {
        //Inicia o timer do recarregamento
        if (recarregando)
        {
            currentTime -= currentTime <= 0 ? 0 : Time.deltaTime;
        }
        //Finaliza o timer do recarregamento
        if (currentTime <= 0)
        {
            balasNoPente = pente;
            currentTime = tempoRecarregar;
            recarregando = false;
        }
    }
}
