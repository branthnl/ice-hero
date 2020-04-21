using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int penguin = 0;
    public int penguinToRescue = 1;
    public bool isPause, isGameOver;
    public int levelIndex;
    private Ball mainBall;
    [HideInInspector]
    public Paddle mainPaddle;
    [SerializeField]
    private GameObject fireworks, pauseButton, ballPrefab;
    [SerializeField]
    private AudioClip resultMusic;
    [SerializeField]
    TextMeshProUGUI levelText, penguinText;
    [SerializeField]
    private Animator pausePanelAnimator, resultPanelAnimator;
    public static LevelManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        mainBall = FindObjectOfType<Ball>();
        mainPaddle = FindObjectOfType<Paddle>();
        levelText.text = "Level " + (levelIndex + 1);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UserSelectPause();
        }
    }
    public void AddPenguin()
    {
        ++penguin;
        if (penguin >= penguinToRescue)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        if (!isGameOver)
        {
            var rbObjects = FindObjectsOfType<Rigidbody2D>();
            foreach (Rigidbody2D rb in rbObjects)
            {
                rb.simulated = !isPause;
            }
            penguinText.text = penguin + "/" + penguinToRescue;
            resultPanelAnimator.SetTrigger("In");
            GameManager.Instance.PlaySound("Yay");
            pauseButton.SetActive(false);
            fireworks.SetActive(true);
            var ad = GetComponent<AudioSource>();
            ad.Stop();
            ad.PlayOneShot(resultMusic);
            // Save progress
            GameManager.Instance.SaveProgress(levelIndex + 1);
            isGameOver = true;
        }
    }
    public void AddBall()
    {
        Instantiate(ballPrefab, new Vector3(0, -7.25f, 0), Quaternion.identity);
    }
    public void UserSelectNext()
    {
        SceneManager.LoadScene("Level" + (levelIndex + 2));
    }
    public void UserSelectMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void UserSelectRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void UserSelectPause()
    {
        isPause = !isPause;
        // mainBall.myRigidbody2D.simulated = !isPause;
        // mainPaddle.myRigidbody2D.simulated = !isPause;
        var rbObjects = FindObjectsOfType<Rigidbody2D>();
        foreach (Rigidbody2D rb in rbObjects)
        {
            rb.simulated = !isPause;
        }
        if (isPause)
        {
            pausePanelAnimator.SetTrigger("In");
            pauseButton.SetActive(false);
        }
        else
        {
            pausePanelAnimator.SetTrigger("Out");
            pauseButton.SetActive(true);
        }
    }
}