using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private static int currentLevel = 0;

    public ParticleSystem confetti;
    public LevelsOrder levelsObj;
    public Transform levelRoot;
    public GameObject completeMessage;

    private int needed = -1;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(_instance);
        _instance = this;
        needed = -1;
    }

    private void Start()
    {
        confetti.Pause();
        completeMessage.SetActive(false);
        Instantiate(levelsObj.levels[currentLevel], levelRoot);
    }

    public void GoToNextScene()
    {
        currentLevel++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void WinGame()
    {
        _instance.completeMessage.SetActive(true);
        SoundManager.PlayWin();
        foreach (var character in FindObjectsOfType<Character>())
        {
            character.Dance();
        }
        _instance.Invoke(nameof(GoToNextScene), 5);
        _instance.confetti.Play();
    }

    public static void SignPlatform(int maximumAllowed)
    {
        if (_instance.needed < 0)
            _instance.needed = maximumAllowed;
        else if (_instance.needed == 0)
            Debug.LogWarning("Should never happen");
        else
            _instance.needed += maximumAllowed;
    }

    public static void AddCorrect()
    {
        _instance.needed--;
        if(_instance.needed == 0)
            WinGame();
    }
    
    public static void RemoveCorrect()
    {
        _instance.needed++;
    }
}