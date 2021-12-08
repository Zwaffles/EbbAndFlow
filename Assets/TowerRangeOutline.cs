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
        _towerRange = GetComponentInParent<TowerTargeting>().towerRange;
        outline.gameObject.transform.localScale = new Vector2(_towerRange, _towerRange);
    }
}
