using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get { return instance; } }
    private static SelectionManager instance;

    [SerializeField] Tower selectedTower;
    [SerializeField] private LayerMask towerLayer;
    [SerializeField] GameObject towerUI;
    PlayerCurrency playerCurrency;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }

    private void Start()
    {
        playerCurrency = FindObjectOfType<PlayerCurrency>();
    }
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
                    selectedTower = hit.transform.gameObject.GetComponent<Tower>();
                    OpenTowerUI();
                }
            }
            else
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    UnselectTower();
                }
            }
        }
    }

    public void UnselectTower()
    {
        selectedTower = null;
        CloseTowerUI();
    }

    void OpenTowerUI()
    {
        towerUI.gameObject.SetActive(true);
    }

    void CloseTowerUI()
    {
        towerUI.gameObject.SetActive(false);
    }

    public void SellTower()
    {
        if (selectedTower != null)
        {
            playerCurrency.AddPlayerNormalCurrency(selectedTower.sellPrice);
            BuildingManager.Instance.RemoveBuilding(selectedTower.gameObject);
            CloseTowerUI();
        }
    }
}
