using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used to switch scene, game is very slow to load so it is load async
/// </summary>
public class SceneChanger : MonoBehaviour
{
    
    public void openGame()
    {
        
        StartCoroutine(coGameLoader());
    }

    private IEnumerator coGameLoader()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("MainGameScene");
        while (!async.isDone)
        {
            yield return null;
        }
    }

    public void openEnd()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void openGameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("GameOver");
    }

    public void Exit()
    {
        Application.Quit();
    }


}
