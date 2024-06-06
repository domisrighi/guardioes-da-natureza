using UnityEngine.SceneManagement;
using UnityEngine;

public class Morrer : MonoBehaviour
{
    private MoverPersonagem moverPersonagem;
    private controleAnimacao controleAnimacao;
    private MoverEAtirar moverEAtirar;
    private Arco arco;

    void Awake()
    {
        moverPersonagem = GetComponent<MoverPersonagem>();
        controleAnimacao = GetComponent<controleAnimacao>();
        arco = GetComponentInChildren<Arco>();
    }

    public void DesabilitarTudo()
    {
        moverPersonagem.enabled = false;
        controleAnimacao.enabled = false;
        arco.gameObject.SetActive(false);

        GameObject Enemy = GameObject.FindWithTag("Enemy");
        if (Enemy != null)
        {
            Enemy.GetComponent<MoverEAtirar>().enabled = false;
        }
    }

    public void ReinciarCena()
    {
        Invoke("Reload", 5f);
    }

    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
