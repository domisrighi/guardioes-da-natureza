using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("Campo de Visao")]
    public float AlcanceDaVisao;
    public float AlcanceDoTiro;
    public float distanciaLimite;
    [HideInInspector] public float AnguloDeVisao = 360f;
    [Range(0, 360)] public float AnguloDoTiro;

    [Space(15)]

    //public Eventos Eventos;
    public GameEvent SeguirAlvo;
    public GameEvent atirarEspingarda;

    [Space(15)]

    public LayerMask camadaJogador;
    public LayerMask camadaObstaculos;
    public LayerMask camadaFlecha;

    [HideInInspector] public Transform alvoVisivel;
    [HideInInspector] public Transform alvoAtiravel;

    void FixedUpdate()
    {
        StartCoroutine("FindTargetsWithDelay", 0.1f); //Executa o metodo mascara a cada 0.1 segundos
    }

    IEnumerator FindTargetsWithDelay(float delay) //Chama o metodo que acha o alvo
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            AcharAlvo();
            AcharFlecha();
        }
    }

    void AcharFlecha()
    {
        Collider2D flechaNoAlcance = Physics2D.OverlapCircle(transform.position, AlcanceDoTiro, camadaFlecha);

        if (flechaNoAlcance != null)
        {
            bool taNoAr = flechaNoAlcance.gameObject.GetComponent<Flecha>().hasHit;

            if (!taNoAr)
            {
                Transform target = flechaNoAlcance.transform;
                Vector2 dirToTarget = (target.position - transform.position).normalized;
                float dstToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, camadaObstaculos) && Vector2.Angle(transform.up, dirToTarget) < AnguloDoTiro / 2)
                {
                    GameEventHub.inimigo = this.transform.parent.gameObject;
                    atirarEspingarda.Raise();
                }
            }
        }
    }

    void AcharAlvo()
    {
        //Limpa a lista para evitar alvos duplicados
        alvoVisivel = null;
        alvoAtiravel = null;
        //Detecta se há um alvo dentro do alcance do tiro
        Collider2D alvoNoAlcanceDoTiro = Physics2D.OverlapCircle(transform.position, AlcanceDoTiro, camadaJogador);
        // Se tiver alvo no alcance do tiro, alvos no alcance da visão é nulo; Senão, Detecta se há um alvo dentro do alcance da visão
        Collider2D alvoNoAlcanceDaVisao = alvoNoAlcanceDoTiro != null ? null : Physics2D.OverlapCircle(transform.position, AlcanceDaVisao, camadaJogador);

        Collider2D Alvo = alvoNoAlcanceDaVisao == null ? alvoNoAlcanceDoTiro : alvoNoAlcanceDaVisao;

        //Código refatorado
        if (Alvo != null)
        {
            //define a direção e a distância do alvo
            Transform target = Alvo.transform;
            Vector2 dirToTarget = (target.position - transform.position).normalized;
            float dstToTarget = Vector2.Distance(transform.position, target.position);

            //Se não tiver um obstáculo entre o inimigo e o alvo E alvo dentro do cone de visão
            if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, camadaObstaculos) && Vector2.Angle(transform.up, dirToTarget) < AnguloDeVisao / 2)
            {
                transform.up = target.position - transform.position; //Rotaciona o campo de visão para acompanhar o alvo

                if (alvoNoAlcanceDoTiro != null)
                {
                    alvoAtiravel = target; //Adiciona alvo a lista
                    GameEventHub.SeguirAlvo.alvo = alvoAtiravel;
                    GameEventHub.SeguirAlvo.distanciaMax = distanciaLimite;
                    GameEventHub.inimigo = this.transform.parent.gameObject;
                    SeguirAlvo.Raise();
                    atirarEspingarda.Raise();
                }
                else if (alvoNoAlcanceDaVisao != null)
                {
                    alvoVisivel = target; //Adiciona alvo a lista
                    GameEventHub.SeguirAlvo.alvo = alvoVisivel;
                    GameEventHub.inimigo = this.transform.parent.gameObject;
                    SeguirAlvo.Raise();
                }
            }
        }
    }

    //Diferencia a rotação local da rotação global
    public Vector3 DirFromAngle(float AngleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            AngleInDegrees -= transform.eulerAngles.z;
        }

        return new Vector3(Mathf.Sin(AngleInDegrees * Mathf.Deg2Rad), Mathf.Cos(AngleInDegrees * Mathf.Deg2Rad), 0);
    }
}