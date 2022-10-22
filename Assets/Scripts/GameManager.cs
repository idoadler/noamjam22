using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    public string nextScene;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        _instance = this;
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}