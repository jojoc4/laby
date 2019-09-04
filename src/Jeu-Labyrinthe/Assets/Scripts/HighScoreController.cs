using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Manages the hish score tab and global ending scene
/// </summary>
public class HighScoreController : MonoBehaviour
{

    public Text topTab_name;
    public Text topTab_time;
    public Text yourScore;
    public InputField nameField;
    public Button submit;

    private long time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetScores());
        yourScore.text = "Your time: " + Timer.getTime();
        time = Timer.timer.ElapsedMilliseconds;
    }

    public void AddTime()
    {
        StartCoroutine(pushScore(time, nameField.text));
    }

    /// push score on webserver
    private IEnumerator pushScore(long time, string name)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://unilab.host-free.ch/add_score.php?n=" + name + "&t=" + time);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            //UnityEditor.EditorUtility.DisplayDialog("Connection error", "Connot connect to server, please check your internet connection.", "ok");
            topTab_name.text = "Connection error\nCannot connect to the server,\nplease verify your internet connection.";
        }
        else
        {
            submit.gameObject.SetActive(false);
            StartCoroutine(GetScores());
        }
    }

    /// <summary>
    /// gets top 10 scores and associated times from webserver
    /// </summary>
    public IEnumerator GetScores()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://unilab.host-free.ch/get_scores.php?s=n");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            //UnityEditor.EditorUtility.DisplayDialog("Connection error", "Connot connect to server, please check your internet connection.", "ok");
            topTab_name.text = "Connection error\nCannot connect to the server,\nplease verify your internet connection.";
        }
        else
        {
            string tab = "Top 10:\n"+www.downloadHandler.text.Replace("<br>", "\n");
            topTab_name.text = tab;

            www = UnityWebRequest.Get("http://unilab.host-free.ch/get_scores.php?s=t");
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                topTab_name.text = "Connection error\nCannot connect to the server,\nplease verify your internet connection.";
            }
            else
            {
                tab = "\n" + www.downloadHandler.text.Replace("<br>", "\n");
                topTab_time.text = tab;
            }
        }

        
    }

}
