using System.Collections;
using System.Collections.Generic;
//using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMethods : MonoBehaviour
{
    public bool isPauseMenu;
    private bool paused;

    void Start()
    {
        paused = false;
    }

    void Update()
    {
        if (isPauseMenu && Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!paused);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("hey we quit the game, itll work in actual compile, trust me");
    }

    public void SetPause(bool np)
    {
        paused = np;
        Time.timeScale = paused ? 0 : 1;
        // showing pause menu here??
    }

    public void ContinueGame()
    {
        SetPause(false);
    }

    public void RestartScene()
    {
        SetPause(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToStart()
    {
        ContinueGame();
        SceneManager.LoadScene("UI workshop uwu");
    }

}
