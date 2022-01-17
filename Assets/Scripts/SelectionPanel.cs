using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionPanel : MonoBehaviour
{
    [Header("Selection")]
    [SerializeField] private TextMeshProUGUI selectionHeaderText;
    [SerializeField] private Tooltip selectionIconTooltip;
    [SerializeField] private Image selectionIcon;

    [SerializeField] private List<StatPanel> statPanels = new List<StatPanel>();

    private void Awake()
    {
        DisableSelectionPanel();
    }

    public void UpdateSelectionPanel(SelectionInfo selectionInfo)
    {
        EnableSelectionPanel();
        selectionHeaderText.text = selectionInfo.SelectionHeaderText;
        selectionIconTooltip.UpdateTooltip(selectionInfo.SelectionDescriptionText);
        selectionIcon.sprite = selectionInfo.SelectionIcon;

        for (int i = 0; i < statPanels.Count; i++)
        {
            if(i < selectionInfo.StatInfo.Count)
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