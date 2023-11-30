using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string GameSceneName;

    public void StartGame()
    {
        SceneManager.LoadScene(GameSceneName);
    }
}
