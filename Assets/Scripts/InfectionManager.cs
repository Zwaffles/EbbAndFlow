using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionManager : MonoBehaviour
{
    public static InfectionManager Instance { get { return instance; } }
    private static InfectionManager instance;

    [SerializeField] private List<InfectionCyst> infectionCysts = new List<InfectionCyst>();

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