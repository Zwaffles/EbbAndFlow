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
    [SerializeField] private int placementSortingOrder = 1;
    [SerializeField] private Color towerPlacementColor = Color.white;
    [SerializeField] private Color canBuildColor = Color.green;
    [SerializeField] private Color cantBuildColor = Color.red;
    [SerializeField] private LayerMask groundLayer;

    [Header("Build Marker")]
    [SerializeField] private Transform buildMarker;
    [SerializeField] private GameObject buildMarkerPrefab;

    [Header("Tower Parent")]
    [SerializeField] private Transform towerParent;

    [SerializeField] private List<GameObject> towerPrefabs = new List<GameObject>();

    private SpriteRenderer buildMarkerSprite;
    private BuildingGrid buildingGrid;
    private GameObject towerToBuild;
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
        buildMarkerSprite = buildMarker.GetComponent<SpriteRenderer>();
        buildingGrid = GetComponent<BuildingGrid>();
        buildingGrid.BuildGrid();
        foreach(TowerBuilder _towerBuilder in FindObjectsOfType<TowerBuilder>())
        {
            towerPrefabs.Add(_towerBuilder.towerPrefab);
        }
        towerPrefabs.Reverse();
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
        BuildMarker();
        PlacingTower();
    }

    private void PlacingTower()
    {
        if (placingTower)
        {
            if (towerToBuild != null)
            {
                Cancel(); 
                BuildTower();
                FollowCursor();
            }
        }
    }    

    public void PlaceTower(GameObject tower)
    {
        CancelPlacement();
        towerToBuild = Instantiate(tower, Utilities.GetMouseWorldPosition(), Quaternion.identity);
        towerToBuild.GetComponent<SpriteRenderer>().sortingOrder = placementSortingOrder; 
        towerToBuild.GetComponent<SpriteRenderer>().color = towerPlacementColor;
        towerToBuild.transform.SetParent(towerParent);
        placingTower = true;
    }

    private void FollowCursor()
    {
        if(towerToBuild != null)
        {
            Vector3 gridWorldPosition = buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition());
            towerToBuild.transform.position = gridWorldPosition;
        }
    }

    private void BuildMarker()
    {
        if (placingTower)
        {
            buildMarkerSprite.enabled = true;
            buildMarker.transform.position = buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition());
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

    private void BuildTower()
    {
        if (CanBuildTowerCheck() && CheatDetection.Instance.CheckForObstacles() && GameManager.Instance.CanBuy(towerToBuild.GetComponent<Tower>().baseCost))
        {
            buildMarker.GetComponent<SpriteRenderer>().color = canBuildColor;

            /* Shift Actions */
            if (Input.GetKey(KeyCode.LeftShift))
            {
                /* Shift Click */
                if (Input.GetMouseButtonDown(0))
                {
                    startDragPosition = Utilities.GetMouseWorldPosition();
                }
                /* Shift Click Released */
                if (Input.GetMouseButtonUp(0))
                {

                }
                /* Dragging */
                if (Input.GetMouseButton(0))
                {
                    currentDragPosition = Utilities.GetMouseWorldPosition();
                    Debug.DrawLine(startDragPosition, currentDragPosition, Color.blue);
                    Debug.Log("Drag Length: " + GetDragLength());
                }  
            }
            /* Normal Actions */
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    buildingGrid.SetValue(Utilities.GetMouseWorldPosition(), 1);
                    towerToBuild.GetComponent<SpriteRenderer>().color = Color.white;
                    towerToBuild.GetComponent<Collider2D>().enabled = true;
                    towerToBuild.GetComponent<Tower>().enabled = true;
                    GameManager.Instance.GetComponent<PlayerCurrency>().RemovePlayerNormalCurrency(towerToBuild.GetComponent<Tower>().baseCost);
                    Vector3 gridWorldPosition = buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition());
                    towerToBuild.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    towerToBuild.transform.position = gridWorldPosition;
                    placingTower = false;
                    towerToBuild = null;
                    var graphToScan = AstarPath.active.data.gridGraph;
                    AstarPath.active.Scan(graphToScan);
                    SelectionManager.Instance.UnselectTower();
                }
            }
        }
        else
        {
            CheatDetection.Instance.CheckForObstacles();
            buildMarker.GetComponent<SpriteRenderer>().color = cantBuildColor;
        }
    }

    private float GetDragLength()
    {
        return Vector3.Distance(startDragPosition, currentDragPosition);
    }

    private Vector3 GetDragDirection()
    {
        return Vector3.right;
    }

    public void RemoveTower(GameObject tower)
    {
        buildingGrid.SetValue(tower.transform.position, 0);
        Destroy(tower);
    }

    private void CancelPlacement()
    {
        placingTower = false;
        if(towerToBuild != null)
        {
            Destroy(towerToBuild);
        }
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
        if(key > 0 && key <= towerPrefabs.Count)
        {
            PlaceTower(towerPrefabs[key - 1]);
        }
    }
}