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
            /* ArmorStat format */
            if (statInfo.Stat == StatInfo.StatType.Armor)
            {
                text = (statInfo.BaseStat * 100.0f).ToString() + "%";
                tooltip.UpdateTooltip(statInfo.Stat.ToString() + ": " + " +" + (statInfo.BaseStat * 100.0f).ToString() + "%");
            }
            /* InfectionScore format */
            else if (statInfo.Stat == StatInfo.StatType.InfectionScore)
            {
                text = statInfo.BaseStat.ToString();
                tooltip.UpdateTooltip(statInfo.Stat.ToString() + ": " + statInfo.CurrentStat.ToString());
            }
            /* Normal Stat format */
            else
            {
                text = statInfo.BaseStat.ToString();
                tooltip.UpdateTooltip(statInfo.Stat.ToString() + ": " + statInfo.CurrentStat.ToString());
            }
        }
        /* Show Modified Stat */
        else
        {
            if (statInfo.Stat == StatInfo.StatType.Armor)
            {
                float difference = Mathf.Abs(statInfo.BaseStat - statInfo.CurrentStat);
                difference = FormatToDecimals(difference);
                

                /* Damage Increase */
                if (statInfo.CurrentStat > statInfo.BaseStat)
                {
                    float statValue = (difference * 100.0f);
                    statValue = FormatToDecimals(statValue);
                    text = "- " + statValue.ToString() + "%";
                    tooltip.UpdateTooltip(statInfo.Stat.ToString() + ": " + " -" + statValue.ToString() + "%");
                }
                /* Damage Decrease */
                else
                {
                    float statValue = ((statInfo.BaseStat + difference) * 100.0f);
                    statValue = FormatToDecimals(statValue);
                    text = statValue.ToString() + "%";
                    tooltip.UpdateTooltip(statInfo.Stat.ToString() + ": " + statValue.ToString() + "%");
                }
            }
            else if (statInfo.Stat == StatInfo.StatType.AttackSpeed)
            {
                float difference = Mathf.Abs(statInfo.BaseStat - statInfo.CurrentStat);
                difference = FormatToDecimals(difference);

                text = (statInfo.BaseStat + difference).ToString() + " +[" + difference.ToString("F3") + "]";
                tooltip.UpdateTooltip(statInfo.Stat.ToString() + ": " + (statInfo.BaseStat + difference).ToString());
            }
            else
            {
                text = statInfo.CurrentStat.ToString() + " +[" + FormatToDecimals((statInfo.CurrentStat - statInfo.BaseStat)).ToString("F2") + "]";
                tooltip.UpdateTooltip(statInfo.Stat.ToString() + ": " + statInfo.CurrentStat.ToString());
            }
        }
        return text;
    }

    private float FormatToDecimals(float value)
    {
        return Mathf.Round(value * 100f) / 100f;
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