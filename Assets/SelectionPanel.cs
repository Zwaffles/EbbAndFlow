using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionPanel : MonoBehaviour
{
    [Header("Selection")]
    [SerializeField] private TextMeshProUGUI selectionHeaderText;
    [SerializeField] private Image selectionIcon;

    [SerializeField] private List<StatPanel> statPanels = new List<StatPanel>();

    private void Awake()
    {
        DisableSelectionPanel();
    }

    public void UpdateSelectionPanel(SelectionInfo selectionInfo)
    {
        Debug.Log("SelectionInfo: " + selectionInfo.SelectionHeaderText);

        EnableSelectionPanel();
        selectionHeaderText.text = selectionInfo.SelectionHeaderText;
        selectionIcon.sprite = selectionInfo.SelectionIcon;

        for (int i = 0; i < statPanels.Count; i++)
        {
            if(selectionInfo.StatInfo[i] != null)
            {
                statPanels[i].UpdateStatPanel(selectionInfo.StatInfo[i]);
            }
            else
            {
                statPanels[i].DisableStatPanel();
            }
        }
    }

    public void EnableSelectionPanel()
    {
        gameObject.SetActive(true);
    }

    public void DisableSelectionPanel()
    {
        gameObject.SetActive(false);
    }
}