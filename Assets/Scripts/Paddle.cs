using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector2 size = new Vector2(3.0f, 0.9f);
    private SpriteRenderer mySpriteRenderer;
    private BoxCollider2D myBoxCollider2D;
    private Rigidbody2D myRigidbody2D;
    private float input;
    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        mySpriteRenderer.size = size;
        myBoxCollider2D.size = size;
        if (Input.GetMouseButton(0)) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            input = mousePosition.x - transform.position.x;
        }
        else {
            input = Input.GetAxisRaw("Horizontal");
        }
    }
    private void FixedUpdate() {
        myRigidbody2D.velocity = Vector2.right * input * speed;
    }
}