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
        for (int i = 0; i < 10; ++i)
        {
            AddLevelButton(i);
        }
    }
    private void AddLevelButton(int levelIndex)
    {
        GameObject n = Instantiate(levelButtonPrefab);
        Button btn = n.GetComponent<Button>();
        btn.GetComponentInChildren<TextMeshProUGUI>().text = (levelIndex + 1).ToString();
        btn.onClick.AddListener(() =>
        {
            UserSelectLevel("Level" + (levelIndex + 1));
        });
        n.transform.SetParent(levelButtonParent);
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
        SceneManager.LoadScene(levelName);
    }
    public void UserSelectPlay()
    {
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
}