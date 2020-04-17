using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum MenuState
{
    MainMenu,
    LevelSelection
}

public class MenuManager : MonoBehaviour
{
    public MenuState state;
    [SerializeField]
    private Animator mainPanelAnimator;
    [SerializeField]
    private Transform levelButtonParent;
    [SerializeField]
    private GameObject levelButtonPrefab;
    private void Awake()
    {
        for (int i = 0; i < 7; ++i)
        {
            AddLevelButton(i);
        }
    }
    private void AddLevelButton(int levelIndex)
    {
        GameObject n = Instantiate(levelButtonPrefab);
        Button btn = n.GetComponent<Button>();
        TextMeshProUGUI tm = btn.GetComponentInChildren<TextMeshProUGUI>();
        tm.text = (levelIndex + 1).ToString();
        if (GameManager.Instance.progress >= levelIndex)
        {
            btn.onClick.AddListener(() =>
            {
                UserSelectLevel("Level" + (levelIndex + 1));
            });
        }
        else
        {
            btn.interactable = false;
            tm.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        n.transform.SetParent(levelButtonParent);
        n.transform.localScale = Vector2.one;
    }
    public void ChangeState(MenuState newState)
    {
        state = newState;
        switch (state)
        {
            case MenuState.MainMenu:
                mainPanelAnimator.SetTrigger("In");
                break;
            case MenuState.LevelSelection:
                mainPanelAnimator.SetTrigger("Out");
                break;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UserSelectBack();
        }
    }
    public void UserSelectLevel(string levelName)
    {
        PlayPopSound();
        SceneManager.LoadScene(levelName);
    }
    public void UserSelectPlay()
    {
        PlayPopSound();
        ChangeState(MenuState.LevelSelection);
    }
    public void UserSelectBack()
    {
        if (state == MenuState.MainMenu)
        {
            Application.Quit();
        }
        else
        {
            ChangeState(MenuState.MainMenu);
        }
    }
    public void PlayPopSound()
    {
        GameManager.Instance.PlaySound("Pop");
    }
}