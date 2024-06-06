using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;

        Vector3 AnguloDeTiroA = fov.DirFromAngle(-fov.AnguloDoTiro / 2, false); //inicio do cone
        Vector3 AnguloDeTiroB = fov.DirFromAngle(fov.AnguloDoTiro / 2, false); //final do cone

        Vector3 AnguloVisao = fov.DirFromAngle(-fov.AnguloDeVisao / 2, false); //inicio do cone

        //Cria o alcance da visao
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.forward, AnguloVisao, fov.AnguloDeVisao, fov.AlcanceDaVisao);

        //Cria cone do alcance do tiro
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.forward, AnguloDeTiroB, fov.AnguloDoTiro, fov.AlcanceDoTiro);
        Handles.DrawLine(fov.transform.position, fov.transform.position + AnguloDeTiroA * fov.AlcanceDoTiro);
        Handles.DrawLine(fov.transform.position, fov.transform.position + AnguloDeTiroB * fov.AlcanceDoTiro);

        //Limite da movimentação
        Handles.color = Color.cyan;
        Handles.DrawWireArc(fov.transform.position, Vector3.forward, AnguloDeTiroB, fov.AnguloDoTiro, fov.distanciaLimite);

        //Cria uma linha até o alvo dentro do alcance da visao
        if (fov.alvoVisivel != null)
        {
            Handles.color = Color.white;
            Handles.DrawLine(fov.transform.position, fov.alvoVisivel.position);
        }
        //Cria uma linha até o alvo dentro do alcance do tiro
        else if (fov.alvoAtiravel != null)
        {
            Handles.color = Color.red;
            Handles.DrawLine(fov.transform.position, fov.alvoAtiravel.position);
        }

    }
}
