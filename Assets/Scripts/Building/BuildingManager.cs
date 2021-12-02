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

    [Header("Tower Parent")]
    [SerializeField] private Transform towerParent;

    private SpriteRenderer buildMarkerSprite;
    private GameObject towerToBuild;
    private bool placingTower;

    BuildingGrid buildingGrid;
   
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
        }
        else
        {
            buildMarkerSprite.enabled = false;
        }
    }

    private void BuildTower()
    {
        if (CanBuildTowerCheck())
        {
            buildMarker.GetComponent<SpriteRenderer>().color = canBuildColor;

            if (Input.GetMouseButtonDown(0))
            {
                buildingGrid.SetValue(Utilities.GetMouseWorldPosition(), 1);
                towerToBuild.GetComponent<SpriteRenderer>().color = Color.white;
                Vector3 gridWorldPosition = buildingGrid.RoundToGridPosition(Utilities.GetMouseWorldPosition());
                towerToBuild.GetComponent<SpriteRenderer>().sortingOrder = 0;
                towerToBuild.transform.position = gridWorldPosition;
                placingTower = false;
                towerToBuild = null;
            }
        }
        else
        {
            buildMarker.GetComponent<SpriteRenderer>().color = cantBuildColor;
        }
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
}