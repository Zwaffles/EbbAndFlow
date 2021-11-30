using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBarManager : MonoBehaviour
{
    public static ActionBarManager Instance { get { return instance; } }
    private static ActionBarManager instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }
}