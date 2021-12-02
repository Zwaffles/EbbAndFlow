using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActionBuild", menuName = "ActionBar/Build Action", order = 2)]
public class BuildAction : Action
{
    [SerializeField] private GameObject buildingPrefab;

    public override void UseAction()
    {
        base.UseAction();

    }
}