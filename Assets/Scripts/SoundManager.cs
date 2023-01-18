using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource sfx;

    public AudioClip menuMusic;
    public AudioClip backgroundMusic;
    public AudioClip thumpWrong;
    public AudioClip thumpCorrect;
    public AudioClip winLevel;

    private static SoundManager _instance;
    
    private void Awake()
    {
        //Rigidbody rb;
        //rb.
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        music.clip = backgroundMusic;
        music.Play();
    }

    private void playSfx(AudioClip clip, bool force=false)
    {
        if(sfx.isPlaying && !force)
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

    public static void PlayWin()
    {
        _instance.playSfx(_instance.winLevel, true);
    }

    public static void SetMute(bool mute)
    {
        _instance.music.mute = mute;
        _instance.sfx.mute = mute;
    }
}
