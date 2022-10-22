using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource sfx;

    public AudioClip backgroundMusic;
    public AudioClip thumpWrong;
    public AudioClip thumpCorrect;
    public AudioClip winLevel;

    private static SoundManager _instance;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        music.clip = backgroundMusic;
        music.Play();
    }
}
