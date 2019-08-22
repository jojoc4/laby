using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    
    public void openGame()
    {
        
        SceneManager.LoadScene("MainGameScene");
    }

    public void openEnd()
    {

    }

    public void openGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Exit()
    {
        Application.Quit();
    }


}
