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

    private void playSfx(AudioClip clip)
    {
        if(sfx.isPlaying)
            return;
        sfx.clip = clip;
        sfx.Play();
    }
    
    public static void PlayThumpSame()
    {
        _instance.playSfx(_instance.thumpWrong);
    }

    public static void PlayThump()
    {
        _instance.playSfx(_instance.thumpCorrect);
    }
}
