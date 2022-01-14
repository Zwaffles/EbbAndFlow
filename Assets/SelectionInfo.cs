using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class SelectionInfo
{
    [SerializeField] private Sprite selectionIcon;
    [SerializeField] private string selectionHeaderText = "";
    [SerializeField] private List<StatInfo> statInfo = new List<StatInfo>();

    public Sprite SelectionIcon { get { return selectionIcon; } }
    public string SelectionHeaderText { get { return selectionHeaderText; } }
    public List<StatInfo> StatInfo { get { return statInfo; } }
}