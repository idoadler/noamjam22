using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public string nextScene;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(nextScene);
    }
}
