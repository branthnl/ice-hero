using UnityEngine;

public enum PowerupType
{
    LargerPaddle,
    MoreBall
}

public class Powerup : MonoBehaviour
{
    public PowerupType type;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.velocity = new Vector2(0, -100.0f * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paddle"))
        {
            switch (type)
            {
                case PowerupType.LargerPaddle:
                    other.GetComponent<Paddle>().size.x += 0.5f;
                    break;
                case PowerupType.MoreBall:
                    LevelManager.instance.AddBall();
                    break;
            }
            Destroy(gameObject);
        }
    }
}