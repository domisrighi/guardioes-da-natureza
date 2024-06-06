using UnityEngine;

public class MoverCamera : MonoBehaviour
{
    [Header("Componentes")]
    public Transform Alvo;
    public SpriteRenderer background;
    public Camera cam;

    [Header("Configurações da Câmera")]
    public float delay;
    public float deadZone;
    private bool moving;

    private float backMinX, backMaxX, backMinY, backMaxY;
    private Vector3 velocity;


    void Awake()
    {
        velocity = Vector3.zero;

        //Define as bordas do background
        backMinX = background.transform.position.x - background.bounds.size.x / 2f;
        backMaxX = background.transform.position.x + background.bounds.size.x / 2f;
        backMinY = background.transform.position.y - background.bounds.size.y / 2f;
        backMaxY = background.transform.position.y + background.bounds.size.y / 2f;
    }
    void Update()
    {
        Mover();
    }

    private void Mover()
    {
        //Camera segue Alvo com suavização e zona morta
        if (Input.GetButtonUp("Horizontal"))
        {
            moving = false;
        }

        if (moving || Vector2.Distance(transform.position, Alvo.transform.position) > deadZone)
        {
            moving = true;

            Vector3 point = Camera.main.WorldToViewportPoint(Alvo.position);
            Vector3 delta = Alvo.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;

            transform.position = LimitarMovimento(Vector3.SmoothDamp(transform.position, destination, ref velocity, delay));
        }
    }

    private Vector3 LimitarMovimento(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = backMinX + camWidth;
        float maxX = backMaxX - camWidth;
        float minY = backMinY + camHeight;
        float maxY = backMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
