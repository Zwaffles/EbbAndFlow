using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum FloatDirection
    {
        Top, TopRight, Right, BottomRight, Bottom, BottomLeft, Left, TopLeft
    }

    [Header("Positioning")]
    [SerializeField] private Vector2 offset;
    [SerializeField] private FloatDirection floatDirection = FloatDirection.Top;

    [Header("Text")]
    [TextArea(5, 10)]
    [SerializeField] private string tooltip;

    public void UpdateTooltip(string text)
    {
        tooltip = text;
    }

    private void Enable()
    {
        GameManager.Instance.TooltipManager.DisplayTooltipPanel(tooltip, GetTooltipPosition(), floatDirection);
    }

    private void Disable()
    {
        GameManager.Instance.TooltipManager.HideTooltipPanel();
    }

    private Vector3 GetTooltipPosition()
    {
        return transform.position + (Vector3)offset;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Enable();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Disable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + (Vector3)offset, GetComponent<RectTransform>().sizeDelta.x * 0.25f);
    }
}