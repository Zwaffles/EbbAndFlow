using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class Utilities
{
    public static Vector2 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static Vector2Int Vector2ToInt(Vector2 vector)
    {
        return new Vector2Int((int)vector.x, (int)vector.y);
    }

    public static TextMeshPro CreateWorldText(Transform parent, string name, string text, Vector3 localPosition, Vector2 textAreaSize, int fontSize, Color color = default, string sortingLayer = "Default", int sortingOrder = 0, TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignmentOptions textAlignment = TextAlignmentOptions.Center)
    {
        GameObject textObject = new GameObject(name, typeof(TextMeshPro));
        textObject.transform.SetParent(parent, false);
        textObject.transform.localPosition = localPosition;
        TextMeshPro textMesh = textObject.GetComponent<TextMeshPro>();
        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = textAreaSize;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingLayerName = "Foreground";
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
}