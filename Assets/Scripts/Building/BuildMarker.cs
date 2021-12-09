using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMarker : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private SpriteRenderer buildMarker;
    [SerializeField] private SpriteRenderer towerPreview;
    [SerializeField] private Animator towerPreviewAnimator;

    [Header("Sprite Colors")]
    [SerializeField] private Color towerPreviewColor = new Color(1, 1, 1, 0.5f);
    [SerializeField] private Color canBuildColor = new Color(0, 1, 0, 0.5f);
    [SerializeField] private Color cantBuildColor = new Color(1, 0, 0, 0.5f);

    private void Awake()
    {
        buildMarker.enabled = false;
        towerPreview.enabled = false;
    }

    public void UpdateBuildMarker(RuntimeAnimatorController towerPreviewAC)
    {
        towerPreview.color = towerPreviewColor;
        towerPreviewAnimator.runtimeAnimatorController = towerPreviewAC;
    }

    public void CanBuild()
    {
        buildMarker.color = canBuildColor;
    }

    public void CantBuild()
    {
        buildMarker.color = cantBuildColor;
    }

    public void SetActive(bool state)
    {
        /* Activated */
        if (state)
        {
            buildMarker.enabled = true;
            towerPreview.enabled = true;
        }
        /* Deactivated */
        else
        {
            buildMarker.enabled = false;
            towerPreview.enabled = false;
            transform.position = Vector3.left * 100000;
        }
    }
}