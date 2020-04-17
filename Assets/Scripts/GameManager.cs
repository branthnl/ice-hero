using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
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
    }
    public void PlaySound(string key)
    {
        if (audioClipsKey.ContainsKey(key))
        {
            seAudioSource.PlayOneShot(audioClips[audioClipsKey[key]]);
        }
    }
}