using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    //Singelton pattern
    private static FrameRateManager instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            Application.targetFrameRate = 120;
        }
        else
            Destroy(this);
    }
}
