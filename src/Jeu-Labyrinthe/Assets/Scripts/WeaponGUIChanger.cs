using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the weapon displayed on gui
/// </summary>
public class WeaponGUIChanger : MonoBehaviour
{

    public Image[] weapons;         //weapon images
    private int currentIndex = 0;   //current image

    // Start is called before the first frame update
    void Start()
    {
        change();
    }

    public void selectNew(int index)
    {
        currentIndex = index;
        change();
    }

    private void change()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == currentIndex)
            {
                weapons[i].gameObject.SetActive(true);
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
    }
}
