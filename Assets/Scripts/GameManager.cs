using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    public GameObject blinkMessage;
    public TextMeshProUGUI levelName;

    private List<Platform> _platforms = new List<Platform>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(_instance);
        _instance = this;
    }

    private void Start()
    {
        levelName.text = "Level " + (currentLevel + 1);
        confetti.Pause();
        completeMessage.SetActive(false);
        blinkMessage.SetActive(false);
        Instantiate(levelsObj.levels[currentLevel], levelRoot);
    }

    public void GoToNextScene()
    {
        currentLevel++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void BlinkMessage()
    {
        blinkMessage.SetActive(true);
    }

    public static void WinGame()
    {
        _instance.completeMessage.SetActive(true);
        SoundManager.PlayWin();
        foreach (var character in FindObjectsOfType<Character>())
        {
            character.Dance();
        }
        _instance.Invoke(nameof(BlinkMessage), 3);
        _instance.confetti.Play();
    }

    public static void SignPlatform(Platform p)
    {
        _instance._platforms.Add(p);
    }

    public static void TestCorrect()
    {
        if(_instance._platforms.All(p => p.IsCorrect()))
            WinGame();
    }
}