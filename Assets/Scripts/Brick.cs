using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Brick : MonoBehaviour
{
    public int lives = 1;
    public Color[] colorsPerLive;
    private SpriteRenderer mySpriteRenderer;
    private Animator myAnimator;
    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        UpdateColor();
    }
    private void UpdateColor()
    {
        if (colorsPerLive.Length > lives)
        {
            mySpriteRenderer.color = colorsPerLive[lives];
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            --lives;
            if (lives <= 0)
            {
                myAnimator.SetTrigger("Out");
                Destroy(gameObject, 0.1f);
            }
            else
            {
                myAnimator.SetTrigger("Pop");
                UpdateColor();
            }
        }
    }
}