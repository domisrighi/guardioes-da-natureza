using UnityEngine;

public class Tiro : MonoBehaviour
{
    public float speed;
    public GameEvent danoPlayer;
    [HideInInspector] public FieldOfView fov;
    private Rigidbody2D rb;
    //private Vector3 target;
    private Transform spawn;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //target = GameObject.FindGameObjectWithTag("Player").transform.position;

        fov = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<FieldOfView>();
        spawn = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
    }

    void Update()
    {
        //rb.position = Vector2.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);

        //Destroi o projétil se ele sai a distância limite
        Destroy(this.gameObject, Vector3.Distance(spawn.position, spawn.position + fov.DirFromAngle(0f, false) * fov.AlcanceDoTiro) / speed);
    }

    //Destroi o projétil se ele colide com algo que não seja ele mesmo e o inimigo que está atirando
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            danoPlayer.Raise();
        }
        else if (other.gameObject.tag == "Flecha")
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "Projectile")
        {
            Destroy(this.gameObject);
        }

    }
}