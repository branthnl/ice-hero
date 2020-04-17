using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int penguin = 0;
    public int penguinToRescue = 1;
    public bool isPause;
    public int levelIndex;
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
        penguinText.text = penguin + "/" + penguinToRescue;
        resultPanelAnimator.SetTrigger("In");
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
        if (isPause)
        {
            pausePanelAnimator.SetTrigger("In");
        }
        else
        {
            pausePanelAnimator.SetTrigger("Out");
        }
    }
}