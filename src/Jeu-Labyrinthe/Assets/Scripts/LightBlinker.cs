using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Blinks the given light in an irregular way
/// </summary>
public class LightBlinker : MonoBehaviour
{
    public Light lamp;
    public float ONTime = 0.9f;
    public float OFFTime = 0f;
    public bool irregular = true;
    public float irregularityAmount = 0.25f;

    private float timer;
    private System.Random rdm;

    void Start()
    {
        rdm = new System.Random((int)System.DateTime.Now.Ticks);
        System.Threading.Thread.Sleep(5); //Needed to have a different seed for every object...

        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0 && timer <= 0)
        {
            int dice = rdm.Next(100); //Chances of switching off
            if (dice < 30) //switch on
            {
                this.lamp.enabled = true;
                timer = ONTime;
                //Add irregularity if needed
                if(irregular)
                {
                    switch (rdm.Next(3))
                    {
                        case 0: timer -= irregularityAmount; break;
                        case 1: timer += irregularityAmount; break;
                        default: break;
                    }
                    switch (rdm.Next(3))
                    {
                        case 0: timer -= (float)rdm.NextDouble(); break;
                        case 1: timer += (float)rdm.NextDouble(); break;
                        default: break;
                    }
                    
                }
            }
            else //switch off
            {
                this.lamp.enabled = false;
                timer = OFFTime;
                if (irregular)
                {
                    switch (rdm.Next(3))
                    {
                        case 0: timer -= OFFTime / 5; break;
                        case 1: timer += OFFTime / 5; break;
                        default: break;
                    }

                }
            }
        }
        timer -= Time.deltaTime;
    }
}
