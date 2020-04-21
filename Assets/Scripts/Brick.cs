using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Brick : MonoBehaviour
{
    public int lives = 1;
    public Color[] colorsPerLive;
    private bool isDie = false;
    private SpriteRenderer mySpriteRenderer;
    private Animator myAnimator;
    [SerializeField]
    private GameObject[] powerUpPrefabs;
    private int startLives;
    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        UpdateColor();
        startLives = lives;
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
        if (isDie) return;
        if (other.gameObject.CompareTag("Ball"))
        {
            --lives;
            if (lives <= 0)
            {
                isDie = true;
                if (Random.Range(0, 10) < startLives * 2)
                {
                    Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], transform.position, Quaternion.identity);
                }
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