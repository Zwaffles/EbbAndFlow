using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SelectionManager : MonoBehaviour
{

    [Header("Setup")]
    [SerializeField] private SelectionPanel selectionPanel;
    [SerializeField] private LayerMask selectionLayer;
    
    [Header("Debug")]
    [SerializeField] private Tower selectedTower;
    [SerializeField] private Enemy selectedEnemy;
    [SerializeField] private TowerUpgrades towerUpgrades;

    public SelectionPanel SelectionPanel { get { return selectionPanel; } set { selectionPanel = value; } }
    public Tower SelectedTower { get { return selectedTower; } }
    public Enemy SelectedEnemy { get { return selectedEnemy; } }
    public TowerUpgrades TowerUpgrades { get { return towerUpgrades; } }

    private void Update()
    {
        Select();
    }

    private void FixedUpdate()
    {
        if(selectedEnemy != null)
        {
            selectionPanel.UpdateSelectionPanel(selectedEnemy.GetSelectionInfo());
        }
        if(selectedTower != null)
        {
            selectionPanel.UpdateSelectionPanel(selectedTower.GetSelectionInfo());
        }
    }

    void Select()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Utilities.GetMouseWorldPosition(), transform.forward, Mathf.Infinity, selectionLayer);

            if (hit.collider != null)
            {
                /* Tower Selected */
                if (hit.transform.gameObject.GetComponent<Tower>() != null)
                {
                    DeselectEnemy(); 
                    DeselectTower();
                    selectedTower = hit.transform.gameObject.GetComponent<Tower>();
                    towerUpgrades = selectedTower.GetComponent<TowerUpgrades>();
                    if (selectedTower.gameObject.GetComponent<TowerRangeOutline>() != null)
                    {
                        selectedTower.gameObject.GetComponent<TowerRangeOutline>().outline.color = new Color(255, 255, 255, 255);
                    }
                    selectionPanel.UpdateSelectionPanel(selectedTower.GetSelectionInfo());
                    UpdateActionBarPanel(selectedTower.ActionBar);
                }
                /* Enemy Selected */
                else if (hit.transform.gameObject.GetComponent<Enemy>() != null)
                {
                    DeselectTower();
                    DeselectEnemy();
                    selectedEnemy = hit.transform.gameObject.GetComponent<Enemy>();
                    selectedEnemy.SelectionOutline.enabled = true; 
                    selectionPanel.UpdateSelectionPanel(selectedEnemy.GetSelectionInfo());
                    DefaultActionBarPanel();
                }
            }
            /* Nothing Selected */
            else
            {
                /* Deselect Tower */
                if (selectedTower != null && !EventSystem.current.IsPointerOverGameObject())
                {
                    DeselectTower();
                }
                if (selectedEnemy != null && !EventSystem.current.IsPointerOverGameObject())
                {
                    DeselectEnemy();
                } 
            }
        }
    }

    public void DeselectTower()
    {
        if(selectedTower != null)
        {
            if (selectedTower.gameObject.GetComponent<TowerRangeOutline>() != null)
            {
                selectedTower.gameObject.GetComponent<TowerRangeOutline>().outline.color = new Color(255, 255, 255, 0);
            }
        }
        selectionPanel.DisableSelectionPanel();
        DefaultActionBarPanel();
        selectedTower = null;
        
    }

    public void DeselectEnemy()
    {
        if (selectedEnemy != null)
        {
            selectedEnemy.SelectionOutline.enabled = false;
        }
        selectionPanel.DisableSelectionPanel();
        DefaultActionBarPanel();
        selectedEnemy = null;
    }

    void UpdateActionBarPanel(ActionBar actionBar)
    {
        GameManager.Instance.ActionBarManager.UpdateActionBar(actionBar);
        
    }

    void DefaultActionBarPanel()
    {
        GameManager.Instance.ActionBarManager.DefaultActionBar();
    }

    public void SellTower()
    {
        if (selectedTower == null) { return; }
        if (selectedTower.CheckTowerInfected()) { return; }
        selectedTower.RemoveTower();
        /* Decrease Counter in the StatisticsManager */
        GameManager.Instance.StatisticsManager.DecreaseTowersBuiltCount();
        GameManager.Instance.PlayerCurrency.AddPlayerNormalCurrency(selectedTower.sellPrice);
        GameManager.Instance.InfectionManager.RemoveTowerFromList(selectedTower);
        GameManager.Instance.BuildingManager.RemoveBuilding(selectedTower.gameObject);
        GameManager.Instance.BuildingManager.UpdateGraph();
        DefaultActionBarPanel();
    }

    public bool CanSellTower()
    {
        if(selectedTower == null) { return false; }
        return selectedTower.sellTimer <= 0 ? false : true;
    }
}
