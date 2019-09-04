using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates an object
/// </summary>
public class Rotater : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0)
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
