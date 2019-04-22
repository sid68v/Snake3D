using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralDataController : MonoBehaviour
{
    public static GeneralDataController Instance;

    public string TOPSCORE;

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        TOPSCORE = "topscore";
    }

    public enum DIRECTION
    {
        LEFT,RIGHT,FORWARD,BACK
    }
}
