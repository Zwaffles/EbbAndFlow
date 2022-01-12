using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerRangeOutline : MonoBehaviour
{
    private float _towerRange;
    public SpriteRenderer outline;

    void Start()
    {
        UpdateOutline();
    }

    public void UpdateOutline()
    {
        _towerRange = GetComponentInParent<TowerTargeting>().towerRange * 2;
        outline.gameObject.transform.localScale = new Vector2(_towerRange, _towerRange);
    }
}
