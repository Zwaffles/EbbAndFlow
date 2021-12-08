using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get { return instance; } }
    private static BuildingManager instance;

    [Header("Build Settings")]
    [SerializeField] private int maxBuildLength = 10;
    [SerializeField] private LayerMask groundLayer;

    [Header("Build Marker")]
    [SerializeField] private Transform buildMarkerParent;
    [SerializeField] private GameObject buildMarkerPrefab;

    [Header("Tower Parent")]
    [SerializeField] private Transform towerParent;

    private List<TowerBuilder> towerBuilders = new List<TowerBuilder>();
    private List<BuildMarker> buildMarkers = new List<BuildMarker>();

    [SerializeField] private TowerBuilder currentTowerBuilder;
    private BuildingGrid buildingGrid;
    [SerializeField] private int activeBuildMarkers; 
    private bool placingTower;
 
    private Vector3 startDragPosition;
    private Vector3 currentDragPosition;

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
        //buildMarkerSprite = buildMarker.GetComponent<SpriteRenderer>();
        
        buildingGrid = GetComponent<BuildingGrid>();
        buildingGrid.BuildGrid();

        InstantiateBuildMarkers();

        foreach (TowerBuilder towerBuilder in FindObjectsOfType<TowerBuilder>())
        {
            towerBuilders.Add(towerBuilder);
        }
        towerBuilders.Reverse();
    }

    void OnGUI()
    {
        if (Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            HotkeyManager((int)Event.current.keyCode - 48);
        }
    }

    private void Update()
    {
        //BuildMarker();
        PlacingTower();
    }
    private void InstantiateBuildMarkers()
    {
        /* Instantiate BuildMarkers */
        for (int i = 0; i < maxBuildLength; i++)
        {
            GameObject buildMarkerInstance = Instantiate(buildMarkerPrefab, buildMarkerParent);
            buildMarkers.Add(buildMarkerInstance.GetComponent<BuildMarker>());
        }
    }
    public void PlaceTower(TowerBuilder towerBuilder)
    {
        //CancelPlacement();

        UpdateBuildMarkers(towerBuilder);
        placingTower = true;

        //towerToBuild = Instantiate(tower, Utilities.GetMouseWorldPosition(), Quaternion.identity);
        //towerToBuild.GetComponent<SpriteRenderer>().sortingOrder = placementSortingOrder; 
        //towerToBuild.GetComponent<SpriteRenderer>().color = towerPlacementColor; /* remove? */
        //towerToBuild.transform.SetParent(towerParent);

    }

    private void UpdateBuildMarkers(TowerBuilder towerBuilder)
    {
        currentTowerBuilder = towerBuilder;
        for (int i = 0; i < buildMarkers.Count; i++)
        {
            buildMarkers[i].UpdateBuildMarker(towerBuilder.TowerPreviewAC);
        }
    }

    private void PlacingTower()
    {
        if (placingTower)
        {
            if (currentTowerBuilder != null)
            {
                
                BuildTower();
                FollowCursor();
                Cancel();
            }
        }
    }    

    private void FollowCursor()
    {
        if (currentTowerBuilder != null)
        {
            Vector3 gridWorldPosition = buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition());
            //towerToBuild.transform.position = gridWorldPosition;
        }
    }

    /*
    private void BuildMarker()
    {
        if (placingTower)
        {
            buildMarkerSprite.enabled = true;
            //buildMarker.transform.position = buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition());
            //if (buildMarker.hasChanged)
            //{
            //    var graphToScan = AstarPath.active.data.graphs;
            //    AstarPath.active.Scan(graphToScan);
            //    buildMarker.hasChanged = false;
            //}
        }
        else
        {
            buildMarkerSprite.enabled = false;
        }
    }

    */
    private void BuildTower()
    {
        /* Can Build Tower*/
        //if (CanBuildTowerCheck() && CheatDetection.Instance.CheckForObstacles() && GameManager.Instance.CanBuy(currentTowerBuilder.TowerCost))
        if (CanBuildTowerCheck() && GameManager.Instance.CanBuy(currentTowerBuilder.TowerCost))
        {
            /* Shift Actions */
            if (Input.GetKey(KeyCode.LeftShift))
            {
                /* Shift Click */
                RegulateBuildMarkers(); 
                if (Input.GetMouseButtonDown(0))
                {
                    startDragPosition = buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition());
                }
                /* Dragging */
                if (Input.GetMouseButton(0))
                {
                    currentDragPosition = buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition());
                    PositionBuildMarkers();
                    //BuildMarkerCheck();
                }
                /* Mouse Button Released */
                if (Input.GetMouseButtonUp(0))
                {
                    CancelDrag();
                    DeactivateBuildMarkers();
                    PositionBuildMarker();
                }
            }
            /* Normal Actions */
            else
            {
                PositionBuildMarker();
                RegulateBuildMarkers();
                if (Input.GetMouseButtonDown(0))
                {
                    GameManager.Instance.GetComponent<PlayerCurrency>().RemovePlayerNormalCurrency(currentTowerBuilder.TowerCost);
                    buildingGrid.SetValue(Utilities.GetMouseWorldPosition(), 1);
                    //towerToBuild.GetComponent<SpriteRenderer>().color = Color.white;
                    //towerToBuild.GetComponent<Collider2D>().enabled = true;
                    //towerToBuild.GetComponent<Tower>().enabled = true;
                    
                    Vector3 gridWorldPosition = buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition());
                    //towerToBuild.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    //towerToBuild.transform.position = gridWorldPosition;
                    placingTower = false;
                    //towerToBuild = null;
                    currentTowerBuilder = null;
                    var graphToScan = AstarPath.active.data.gridGraph;
                    AstarPath.active.Scan(graphToScan);
                    
                    SelectionManager.Instance.UnselectTower();
                }
            }
        }
        /* Cant Build Tower */
        else
        {
            CheatDetection.Instance.CheckForObstacles();
            //buildMarker.GetComponent<SpriteRenderer>().color = cantBuildColor;
        }
        /* Shift Click Released */
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            CancelDrag();
            DeactivateBuildMarkers();
        }
    }

    private void RegulateBuildMarkers()
    {
        /* Activate BuildMarker to fill Drag Area */
        if (activeBuildMarkers < GetDragCellAmount())
        {
            int index = Mathf.Clamp(activeBuildMarkers, 0, maxBuildLength - 1);
            buildMarkers[index].SetActive(true);
            activeBuildMarkers++;
        }
        /* Deactive BuildMarker if we have to many */
        else if(activeBuildMarkers > GetDragCellAmount())
        {
            int index = Mathf.Clamp(activeBuildMarkers -1, 0, maxBuildLength - 1);
            buildMarkers[index].SetActive(false);
            activeBuildMarkers--;
        }
    }

    private void PositionBuildMarker()
    {
        buildMarkers[0].transform.position = buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition());
    }

    private void PositionBuildMarkers()
    {
        /* Loop through Active BuildMarkers */
        for (int i = 0; i < activeBuildMarkers; i++)
        {
            /* Position according to Drag Values */
            buildMarkers[i].transform.position = startDragPosition + (i * buildingGrid.GetCellSize()) * GetDragDirection();
        }
    }

    private void BuildMarkerCheck()
    {
        /* Loop through Active BuildMarkers */
        for (int i = 0; i < activeBuildMarkers; i++)
        {
            /* Can Build at Specific BuildMarker Location *//*
            if (buildingGrid.GetValue(buildMarker.transform.position) == 0)
            {
                //buildMarkers[i].GetComponent<SpriteRenderer>().color = cantBuildColor;
            }
            /* Cant Build at Specific BuildMarker Location *//*
            else
            {
                //buildMarkers[i].GetComponent<SpriteRenderer>().color = canBuildColor;
            }*/
        }
    }

    private float GetDragLength()
    {
        return Vector3.Distance(startDragPosition, currentDragPosition);
    }

    private int GetDragCellAmount()
    {
        int cellAmount; 
        Vector3 normalizedDirection = (currentDragPosition - startDragPosition).normalized;

        /* Diagonal Drag */
        if (Mathf.Abs(normalizedDirection.x) == Mathf.Abs(normalizedDirection.y))
        {
            cellAmount = Mathf.RoundToInt(Vector3.Distance(startDragPosition, currentDragPosition) / buildingGrid.GetCellDiagonal()) + 1;
            return Mathf.Clamp(cellAmount, 1, maxBuildLength);
        }
        /* Horizontal/Vertical Drag */
        else
        {
            cellAmount = Mathf.RoundToInt(Vector3.Distance(startDragPosition, currentDragPosition) / buildingGrid.GetCellSize()) + 1;
            return Mathf.Clamp(cellAmount, 1, maxBuildLength);
        }  
    }

    private Vector3 GetDragDirection()
    {
        Vector3 normalizedDirection = (currentDragPosition - startDragPosition).normalized;

        /* Horizontal Drag */
        if (Mathf.Abs(normalizedDirection.x) > Mathf.Abs(normalizedDirection.y))
        {
            
            normalizedDirection.y = 0;
            normalizedDirection.x = 1 * GetDirectionMultiplier(normalizedDirection.x);
        }
        /* Vertical Drag */
        else if (Mathf.Abs(normalizedDirection.y) > Mathf.Abs(normalizedDirection.x))
        {
            normalizedDirection.x = 0; 
            normalizedDirection.y = 1 * GetDirectionMultiplier(normalizedDirection.y); 
        }
        /* Diagonal Drag */
        else if (Mathf.Abs(normalizedDirection.x) == Mathf.Abs(normalizedDirection.y))
        {
            normalizedDirection.x = 1 * GetDirectionMultiplier(normalizedDirection.x);
            normalizedDirection.y = 1 * GetDirectionMultiplier(normalizedDirection.y);
        }

        return normalizedDirection;
    }

    private float GetDirectionMultiplier(float axis)
    {
        if(axis > 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public void RemoveTower(GameObject tower)
    {
        buildingGrid.SetValue(tower.transform.position, 0);
        Destroy(tower);
    }

    private void CancelDrag()
    {
        /* Reset Drag Positions */
        startDragPosition = buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition());
        currentDragPosition = startDragPosition;
    }

    private void DeactivateBuildMarkers()
    {
        /* Loop through BuildMarkers */
        for (int i = 0; i < buildMarkers.Count; i++)
        {
            /* Deactivate BuildMarker and reset activeBuildMarkers value */
            buildMarkers[i].SetActive(false);
        }
        activeBuildMarkers = 0;
    }

    private void CancelPlacement()
    {
        placingTower = false;
        currentTowerBuilder = null;

        CancelDrag();
        DeactivateBuildMarkers();
    }

    private void Cancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(1))
        {
            CancelPlacement();
        }
    }

    private bool CanBuildTowerCheck()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && buildingGrid.GetValue(Utilities.GetMouseWorldPosition()) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
  
    private bool InfectionCheck()
    {

        return true;        
    }

    private void HotkeyManager(int key)
    {
        if(key > 0 && key <= towerBuilders.Count)
        {
            PlaceTower(towerBuilders[key - 1]);
        }
    }

    private void OnDrawGizmos()
    {
        if (placingTower)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Gizmos.color = Color.blue;

                if (Input.GetMouseButton(0))
                {
                    Gizmos.DrawWireSphere(startDragPosition, buildingGrid.GetCellRadius());
                    Gizmos.DrawWireSphere(currentDragPosition, buildingGrid.GetCellRadius());
                    Gizmos.DrawLine(startDragPosition, currentDragPosition);
                }
                
            }
        }
    }
}