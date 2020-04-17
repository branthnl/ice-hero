using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int progress;
    [SerializeField]
    private AudioClip[] audioClips;
    [SerializeField]
    private AudioSource seAudioSource;
    private Dictionary<string, int> audioClipsKey;
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        audioClipsKey = new Dictionary<string, int>();
        audioClipsKey.Add("BounceWall", 0);
        audioClipsKey.Add("BounceBrick", 1);
        audioClipsKey.Add("Pop", 2);
        audioClipsKey.Add("Yay", 3);
        LoadProgress();
        SceneManager.LoadScene("Menu");
    }
    public void PlaySound(string key)
    {
        if (audioClipsKey.ContainsKey(key))
        {
            seAudioSource.PlayOneShot(audioClips[audioClipsKey[key]]);
        }
    }
    public void SaveProgress(int newProgress)
    {
        PlayerPrefs.SetInt("Progress", Mathf.Max(progress, newProgress));
        LoadProgress();
    }
    public void LoadProgress()
    {
        progress = PlayerPrefs.GetInt("Progress", 0);
    }
}