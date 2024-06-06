using System.Collections.Generic;
using UnityEngine;
//Classe para instanciar as classes dos eventos que precisam de parâmetros; Não deve ser aplicada nenhuma logica, somente armazenar dados.
public static class GameEventHub
{
    public static Hub_SeguirAlvo SeguirAlvo = new Hub_SeguirAlvo();
    public static Hub_DanoPlayer DanoPLayer = new Hub_DanoPlayer();
    public static float Cura;
    public static float danoFlecha;
    public static GameObject inimigo;
}
