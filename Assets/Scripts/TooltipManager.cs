using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    [Header("Tooltip Panel")]
    [SerializeField] private RectTransform tooltipPanel;

    private HorizontalLayoutGroup horizontalLayoutGroup;
    private ContentSizeFitter contentSizeFitter;
    private TextMeshProUGUI tooltipText;

    private void Awake()
    {
        horizontalLayoutGroup = tooltipPanel.GetComponent<HorizontalLayoutGroup>();
        tooltipText = tooltipPanel.GetComponentInChildren<TextMeshProUGUI>(); 
        contentSizeFitter = tooltipPanel.GetComponent<ContentSizeFitter>();
    }

    public void DisplayTooltipPanel(string text, Vector3 position, Tooltip.FloatDirection floatDirection)
    {
        tooltipPanel.gameObject.SetActive(true);
        tooltipText.text = text;
        Canvas.ForceUpdateCanvases();
        contentSizeFitter.SetLayoutHorizontal();
        LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipPanel);
        tooltipPanel.gameObject.SetActive(false);
        StartCoroutine(RefreshLayout(position, floatDirection));
        
    }

    private IEnumerator RefreshLayout(Vector3 position, Tooltip.FloatDirection floatDirection)
    {
        yield return new WaitForFixedUpdate();
        PositionTooltipPanel(position, floatDirection);
        tooltipPanel.gameObject.SetActive(true);
    }

    private void PositionTooltipPanel(Vector3 position, Tooltip.FloatDirection floatDirection)
    {
        tooltipPanel.position = position + GetDirectionOffset(floatDirection);
    }

    private Vector3 GetDirectionOffset(Tooltip.FloatDirection floatDirection)
    {
        Vector3 offset = Vector2.zero;

        switch (floatDirection)
        {
            case Tooltip.FloatDirection.Top:
                offset.y = tooltipPanel.sizeDelta.y * 0.5f;
                break;
            case Tooltip.FloatDirection.TopRight:
                offset.x = tooltipPanel.sizeDelta.x * 0.5f;
                offset.y = tooltipPanel.sizeDelta.y * 0.5f;
                break;
            case Tooltip.FloatDirection.Right:
                offset.x = tooltipPanel.sizeDelta.x * 0.5f;
                break;
            case Tooltip.FloatDirection.BottomRight:
                offset.x = tooltipPanel.sizeDelta.x * 0.5f;
                offset.y = -tooltipPanel.sizeDelta.y * 0.5f;
                break;
            case Tooltip.FloatDirection.Bottom:
                offset.y = -tooltipPanel.sizeDelta.y * 0.5f;
                break;
            case Tooltip.FloatDirection.BottomLeft:
                offset.x = -tooltipPanel.sizeDelta.x * 0.5f;
                offset.y = -tooltipPanel.sizeDelta.y * 0.5f;
                break;
            case Tooltip.FloatDirection.Left:
                offset.x = -tooltipPanel.sizeDelta.x * 0.5f;
                break;
            case Tooltip.FloatDirection.TopLeft:
                offset.x = -tooltipPanel.sizeDelta.x * 0.5f;
                offset.y = tooltipPanel.sizeDelta.y * 0.5f;
                break;
            default:
                offset.y = tooltipPanel.sizeDelta.y * 0.5f;
                break;
        }
        return offset;
    }

    public void HideTooltipPanel()
    {
        tooltipPanel.gameObject.SetActive(false);
    }

}