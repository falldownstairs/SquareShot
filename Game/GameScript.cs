
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public static GameScript Instance;

    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _mainMenu;
    private void Awake()
    {
        if (Instance == null){
            Instance = this;
        }
        Time.timeScale = 1f;
        FindAnyObjectByType<AudioManager>().Play("song");
    }

    // Update is called once per frame
    public void GameOver()
    {
        _gameOverCanvas.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
