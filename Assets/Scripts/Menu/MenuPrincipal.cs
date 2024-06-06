using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("fase01");
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Jogo Fechado!");
    }
}
