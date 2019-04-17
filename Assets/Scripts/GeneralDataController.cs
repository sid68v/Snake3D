using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralDataController : MonoBehaviour
{
    public static GeneralDataController Instance;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public enum DIRECTION
    {
        LEFT,RIGHT,FORWARD,BACK
    }
}
