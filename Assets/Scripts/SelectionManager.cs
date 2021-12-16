using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] Tower selectedTower;
    [SerializeField] private LayerMask towerLayer;
    [SerializeField] GameObject towerUI;

    private void Update()
    {
        SelectTower();
    }

    void SelectTower()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Utilities.GetMouseWorldPosition(), transform.forward, Mathf.Infinity, towerLayer);
            Debug.DrawRay(Utilities.GetMouseWorldPosition(), transform.forward, Color.red, 10.0f);
            if (hit.collider != null)
            {
                if (hit.transform.gameObject.GetComponent<Tower>() != null)
                {
                    UnselectTower();
                    selectedTower = hit.transform.gameObject.GetComponent<Tower>();
                    if (selectedTower.gameObject.GetComponent<TowerRangeOutline>() != null)
                    {
                        selectedTower.gameObject.GetComponent<TowerRangeOutline>().outline.color = new Color(255, 255, 255, 255);
                    }
                    OpenTowerUI();
                }
            }
            else
            {
                if (selectedTower != null && !EventSystem.current.IsPointerOverGameObject())
                {
                    UnselectTower();
                }
            }
        }
    }

    public void UnselectTower()
    {
        if(selectedTower != null)
        {
            if (selectedTower.gameObject.GetComponent<TowerRangeOutline>() != null)
            {
                selectedTower.gameObject.GetComponent<TowerRangeOutline>().outline.color = new Color(255, 255, 255, 0);
            }
        }
        selectedTower = null;
        CloseTowerUI();
    }

    void OpenTowerUI()
    {
        towerUI.gameObject.SetActive(true);
        towerUI.transform.GetChild(0).GetComponent<Image>().color = selectedTower.CheckTowerInfected() ? Color.gray : Color.white;
      
    }

    void CloseTowerUI()
    {
        towerUI.gameObject.SetActive(false);
    }

    public void SellTower()
    {
        if (selectedTower == null) { return; }
        if (selectedTower.CheckTowerInfected()) { return; }
        /* Decrease Counter in the StatisticsManager */
        GameManager.Instance.StatisticsManager.DecreaseTowersBuiltCount();
        GameManager.Instance.PlayerCurrency.AddPlayerNormalCurrency(selectedTower.sellPrice); 
        GameManager.Instance.InfectionManager.RemoveTowerFromList(selectedTower);
        GameManager.Instance.BuildingManager.RemoveBuilding(selectedTower.gameObject);
        GameManager.Instance.BuildingManager.UpdateGraph();
        CloseTowerUI();
    }
}
