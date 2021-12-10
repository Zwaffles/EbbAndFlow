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
    [SerializeField] private LayerMask buildObstacleLayer;

    [Header("Tower Parent")]
    [SerializeField] private Transform towerParent;

    private List<TowerBuilder> towerBuilders = new List<TowerBuilder>();
    private List<BuildMarker> buildMarkers = new List<BuildMarker>();

    public enum DragDirection { Vertical, Horizontal, Diagonal };
    private DragDirection dragDirection = DragDirection.Horizontal;

    private TowerBuilder currentTowerBuilder;
    private BuildingGrid buildingGrid;
    private int activeBuildMarkers;
    private int buildCostTotal;
    private bool building;
                                    /* Left ->       Right ->       Top ->      Down          Top-Left ->               Bottom-Left ->             Top-Right ->             Bottom-Right */
    private Vector3[] directions = new Vector3[8] { Vector3.left, Vector3.right, Vector3.up, Vector3.down, new Vector3(-1.0f, 1.0f), new Vector3(-1.0f, -1.0f), new Vector3(1.0f, 1.0f), new Vector3(1.0f, -1.0f) };
    private Vector3 startDragPosition;
    private Vector3 currentDragPosition;
    private Vector3 currentGridPosition;


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
        buildingGrid = GetComponent<BuildingGrid>();
        buildingGrid.BuildGrid();

        InstantiateBuildMarkers();

        foreach (TowerBuilder towerBuilder in FindObjectsOfType<TowerBuilder>())
        {
            towerBuilders.Add(towerBuilder);
        }
        towerBuilders.Reverse();
    }

    private void Update()
    {
        PlacingTower();
    }
    public void PlaceTower(TowerBuilder towerBuilder)
    {
        UpdateBuildMarkers(towerBuilder);
        building = true;
    }

    public void RemoveBuilding(GameObject building)
    {
        buildingGrid.SetValue(building.transform.position, 0);
        Destroy(building);
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

    private void UpdateBuildMarkers(TowerBuilder towerBuilder)
    {
        /* Update all BuildMarkers with new TowerPreview AnimationController */
        currentTowerBuilder = towerBuilder;
        for (int i = 0; i < buildMarkers.Count; i++)
        {
            buildMarkers[i].UpdateBuildMarker(towerBuilder.TowerPreviewAC);
        }
    }

    private void PlacingTower()
    {
        if (building)
        {
            if (currentTowerBuilder != null)
            {
                
                Building();
                Cancel();
                GridUpdate();
            }
        }
    }

    private void GridUpdate()
    {
        if(currentGridPosition != buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition()))
        {
            CheatDetection.Instance.CheckForObstacles(buildMarkerParent.gameObject);
        }
        currentGridPosition = buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition());
    }

    private void Building()
    {
        /* Shift Placement */
        if (Input.GetKey(KeyCode.LeftShift))
        {
            /* Shift Click */
            if (Input.GetMouseButtonDown(0))
            {
                startDragPosition = buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition());
                currentDragPosition = startDragPosition;
            }
            
            /* Dragging */
            if (Input.GetMouseButton(0))
            {
                currentDragPosition = Utilities.GetMouseWorldPosition();
            }
            
            RegulateBuildMarkers();
            PositionBuildMarkers();
            BuildMarkerScan();

            /* Mouse Button Released */
            if (Input.GetMouseButtonUp(0))
            {
                BuildMultipleTowers();
            }
            /* Reset dragPositions for Shift-Click single Tower placement */
            else if (!Input.GetMouseButton(0))
            {
                startDragPosition = buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition());
                currentDragPosition = startDragPosition;
            }
            //CheatDetection.Instance.CheckForObstacles(buildMarkerParent.gameObject);
        }
        /* Normal Placement */
        else
        {
            RegulateBuildMarkers(); 
            PositionBuildMarker();
            BuildMarkerScan();

            if (Input.GetMouseButtonDown(0))
            {
                BuildSingleTower();
            }
        }
        /* Shift Click Released */
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            CancelDrag();
        }
    }

    private void BuildSingleTower()
    {
        /* Reset BuildCostTotal so we can recalculate it before Instantiating a new Tower */
        buildCostTotal = 0;

        if (BuildMarkerCheck(buildMarkers[0]))
        {
            /* Instantiate Tower */
            GameObject towerInstance = Instantiate(currentTowerBuilder.TowerPrefab, towerParent);
            towerInstance.transform.position = buildingGrid.RoundToGridPosition(buildMarkers[0].transform.position);

            /* Remove currency from Player */
            PlayerCurrency.Instance.RemovePlayerNormalCurrency(buildCostTotal);

            /* Mark Grid Cell as occupied */
            buildingGrid.SetValue(buildMarkers[0].transform.position, 1);

            /* Cancel Building */
            building = false;
            currentTowerBuilder = null;

            ScanGraph();
            DeactivateBuildMarkers(); 
        }
    }

    private void BuildMultipleTowers()
    {
        /* Reset BuildCostTotal so we can recalculate it before Instantiating new Towers */
        buildCostTotal = 0;
        for (int i = 0; i < activeBuildMarkers; i++)
        {
            if (BuildMarkerCheck(buildMarkers[i]))
            {
                /* Instantiate Tower */
                GameObject towerInstance = Instantiate(currentTowerBuilder.TowerPrefab, towerParent);
                towerInstance.transform.position = buildingGrid.RoundToGridPosition(buildMarkers[i].transform.position);

                /* Mark Grid Cell as occupied */
                buildingGrid.SetValue(buildMarkers[i].transform.position, 1);
            }
        }

        /* Remove currency from Player */
        PlayerCurrency.Instance.RemovePlayerNormalCurrency(buildCostTotal);

        /* Only stop building if we are drag building while Shift is held down */
        if (activeBuildMarkers > 1)
        {
            /* Cancel Building */
            building = false;
            currentTowerBuilder = null;
            
            /* Deactivate All BuildMarkers & Cancel Drag action */
            DeactivateBuildMarkers();
            CancelDrag();
        }

        ScanGraph();
        DeactivateBuildMarkers();
    }

    private void ScanGraph()
    {
        /*
        for (int i = 0; i < buildMarkers.Count; i++)
        {
            CheatDetection.Instance.CheckForObstacles(buildMarkerParent.gameObject);
        }
        */

        /* Rescan A* Graph */
        var graphToScan = AstarPath.active.data.gridGraph;
        AstarPath.active.Scan(graphToScan);
    }

    private void BuildMarkerScan()
    {
        buildCostTotal = 0;

        /* Scan all active BuildMarkers to check if they are still valid */
        for (int i = 0; i < activeBuildMarkers; i++)
        {
            BuildMarkerCheck(buildMarkers[i]);
        }
    }

    private bool BuildMarkerCheck(BuildMarker buildMarker)
    {
        /* Increase buildCostTotal with TowerCost */
        buildCostTotal += currentTowerBuilder.TowerCost;

        /* BoxCast over BuildMarker Grid Cell */
        RaycastHit2D boxCast = Physics2D.BoxCast(buildMarker.transform.position, new Vector2(buildingGrid.GetCellSize() * 0.5f, buildingGrid.GetCellSize() * 0.5f), 0.0f, Vector3.zero, 1.0f, buildObstacleLayer);

        /*  No Obstacle                 No Tower Occupying Grid Cell Position                         Not Hovering over any UI-element                  Can afford buildCostTotal                         Nothing blocking path */
        if (boxCast.collider == null && buildingGrid.GetValue(buildMarker.transform.position) == 0 && !EventSystem.current.IsPointerOverGameObject() && PlayerCurrency.Instance.CanBuy(buildCostTotal) && CheatDetection.Instance.CheckForObstacles(buildMarkerParent.gameObject))
        {
            buildMarker.CanBuild();
            return true;
        }
        /* Failed Build Check */
        else
        {
            /* Remove TowerCost if BuildCheck at BuildMarker failed */
            buildCostTotal -= currentTowerBuilder.TowerCost;
            buildMarker.CantBuild();
            return false;
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
        /* Deactive BuildMarker if we have too many */
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
        //CheatDetection.Instance.CheckForObstacles(buildMarkerParent.gameObject);
    }

    private void PositionBuildMarkers()
    {
        /* Loop through Active BuildMarkers */
        for (int i = 0; i < activeBuildMarkers; i++)
        {
            /* Position according to Drag Values */
            buildMarkers[i].transform.position = startDragPosition + (i * buildingGrid.GetCellSize()) * GetDragDirection();
            //CheatDetection.Instance.CheckForObstacles(buildMarkerParent.gameObject);
        }
    }

    private int GetDragCellAmount()
    {
        int cellAmount; 

        /* Diagonal Drag */
        if (dragDirection == DragDirection.Diagonal)
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
        Vector3 mouseDirection = Utilities.GetMouseWorldPosition() - (Vector2)startDragPosition;
        Vector3 closestDirection = Vector3.zero; 
        float maxDotProduct = -Mathf.Infinity;
        
        for (int i = 0; i < directions.Length; i++)
        {
            float dot = Vector3.Dot(mouseDirection, directions[i].normalized);
            if(dot > maxDotProduct)
            {
                closestDirection = directions[i];
                maxDotProduct = dot;
            }
        }

        /* Horizontal Drag */
        if(closestDirection == directions[0] || closestDirection == directions[1])
        {
            dragDirection = DragDirection.Horizontal;
        }
        /* Vertical Drag */
        else if (closestDirection == directions[2] || closestDirection == directions[3])
        {
            dragDirection = DragDirection.Vertical;
        }
        /* Diagonal Drag */
        else
        {
            dragDirection = DragDirection.Diagonal;
        }

        return closestDirection;
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
        building = false;
        currentTowerBuilder = null;

        CancelDrag();
        DeactivateBuildMarkers();
    }

    private void CancelDrag()
    {
        /* Reset Drag Positions */
        startDragPosition = buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition());
        currentDragPosition = startDragPosition;
    }

    private void Cancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(1))
        {
            CancelPlacement();
        }
    }
  
    private void HotkeyManager(int key)
    {
        if(key > 0 && key <= towerBuilders.Count)
        {
            PlaceTower(towerBuilders[key - 1]);
        }
    }

    private void OnGUI()
    {
        if (Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            HotkeyManager((int)Event.current.keyCode - 48);
        }
    }

    private void OnDrawGizmos()
    {
        if (building)
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