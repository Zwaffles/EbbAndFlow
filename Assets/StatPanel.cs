using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatPanel : MonoBehaviour
{
    [SerializeField] private Tooltip tooltip;
    [SerializeField] private Image statIcon;
    [SerializeField] private TextMeshProUGUI statText;

    public Tooltip Tooltip { get { return tooltip; } set { tooltip = value; } }
    public Image StatIcon { get { return statIcon; } set { statIcon = value; } }
    public TextMeshProUGUI StatText { get { return statText; } set { statText = value; } }

    public void UpdateStatPanel(StatInfo statInfo)
    {
        statIcon.sprite = statInfo.StatIcon;
        statText.text = FormatStatText(statInfo);
        EnableStatPanel();
    }

    private string FormatStatText(StatInfo statInfo)
    {
        string text; 
        /* Show Normal Stat */
        if(statInfo.BaseStat == statInfo.CurrentStat)
        {
            text = statInfo.BaseStat.ToString();
            tooltip.UpdateTooltip(statInfo.Stat.ToString() + ": " + statInfo.CurrentStat.ToString());
        }
        /* Show Modified Stat */
        else
        {
            text = statInfo.CurrentStat.ToString() + " + (" + (statInfo.CurrentStat - statInfo.BaseStat).ToString() + ")";
            tooltip.UpdateTooltip(statInfo.Stat.ToString() + ": " + statInfo.CurrentStat.ToString());
        }
        return text;
    }

    public void EnableStatPanel()
    {
        gameObject.SetActive(true);
    }

    public void DisableStatPanel()
    {
        gameObject.SetActive(false);
    }
}