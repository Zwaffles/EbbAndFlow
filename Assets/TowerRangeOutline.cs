using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerRangeOutline : MonoBehaviour
{
    private float _towerRange;
    private RectTransform outlineSize;

    void Start()
    {
        _towerRange = GetComponentInParent<TowerTargeting>().towerRange;
        outlineSize = GetComponent<RectTransform>();
        //RectTransform. = new Vector3(_towerRange, _towerRange, 1);
    }
}
