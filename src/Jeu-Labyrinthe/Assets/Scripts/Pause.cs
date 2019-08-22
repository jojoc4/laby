using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject[] pauseObjects;

    // Start is called before the first frame update
    void Start()
    {
        hidePaused();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                hidePaused();
            }
        }
    }

    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            Time.timeScale = 0;
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            Time.timeScale = 1;
            g.SetActive(false);
        }
    }

}
