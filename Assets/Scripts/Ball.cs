using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public float speed = 3.0f;
    private float defaultSpeed;
    [HideInInspector]
    public Rigidbody2D myRigidbody2D;
    private Vector3 startPosition;
    private bool isMoving;
    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        startPosition.x += Random.Range(-0.1f, 0.1f);
        defaultSpeed = speed;
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            // Give some gravity when needed
            float vsp = myRigidbody2D.velocity.y;
            if (Mathf.Abs(vsp) < speed)
            {
                myRigidbody2D.velocity += new Vector2(0, ((speed * Mathf.Sign(vsp == 0 ? 1 : vsp)) - vsp) * 0.1f);
            }
        }
        else
        {
            transform.position = startPosition + new Vector3(LevelManager.instance.mainPaddle.transform.position.x, 0, 0);
        }
    }
    public void Start()
    {
        Invoke("Respawn", 1.0f);
    }
    public void Respawn()
    {
        isMoving = false;
        myRigidbody2D.velocity = Vector2.zero;
        if (!LevelManager.instance.isGameOver)
        {
            GameManager.Instance.PlaySound("Check");
            transform.position = startPosition + new Vector3(LevelManager.instance.mainPaddle.transform.position.x, 0, 0);
            Invoke("FirstMove", 1.0f);
        }
    }
    public void FirstMove()
    {
        isMoving = true;
        speed = defaultSpeed;
        myRigidbody2D.velocity = new Vector2(Random.Range(-3.0f, 3.0f), 3.0f) * speed;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            float xDiff = transform.position.x - other.transform.position.x;
            myRigidbody2D.velocity += new Vector2(xDiff * 3.0f, 0);
            speed = Mathf.Min(speed + 0.1f, defaultSpeed * 2);
            GameManager.Instance.PlaySound("BounceWall");
        }
        else if (other.gameObject.CompareTag("Brick"))
        {
            GameManager.Instance.PlaySound("BounceBrick");
        }
        else if (other.gameObject.CompareTag("Penguin"))
        {
            GameManager.Instance.PlaySound("Queck");
        }
        else
        {
            GameManager.Instance.PlaySound("BounceWall");
        }
    }
}