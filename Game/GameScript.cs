
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public static GameScript Instance;

    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _mainMenu;

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private TextMeshProUGUI _timer;
    private void Awake()
    {
        if (Instance == null){
            Instance = this;
        }
        Time.timeScale = 1f;
        
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.ToString() == "Menu")
        {
            FindAnyObjectByType<AudioManager>().Play("menuTheme");
        }
        if (SceneManager.GetActiveScene().name.ToString() == "Game")
        {
            FindAnyObjectByType<AudioManager>().Play("battleTheme");
        }
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
        Time.timeScale = 1f;

    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        FindAnyObjectByType<AudioManager>().Play("battleTheme");
        FindAnyObjectByType<AudioManager>().Stop("menuTheme");
        Time.timeScale = 1f;
    }
    public void GoMainMenu(){
        SceneManager.LoadScene("Menu");
        FindAnyObjectByType<AudioManager>().Play("menuTheme");
        FindAnyObjectByType<AudioManager>().Stop("battleTheme");
    }
    public void QuitGame(){
        Debug.Log("hi");
        Application.Quit();
    }
    
    public IEnumerator Win(){
        GameObject.FindGameObjectWithTag("Timer").SetActive(false);
        _timer.text = "you completed the game in " + Timer.Instance.EndTimer();
        yield return new WaitForSeconds(3);
        _winScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
