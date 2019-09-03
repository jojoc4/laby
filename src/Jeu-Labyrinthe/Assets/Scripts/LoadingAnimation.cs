using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animation while loading game scene
/// </summary>
public class LoadingAnimation : MonoBehaviour
{

    private RectTransform rectComponent;
    private float rotateSpeed = -200f;

    // Start is called before the first frame update
    void Start()
    {
        rectComponent = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}
