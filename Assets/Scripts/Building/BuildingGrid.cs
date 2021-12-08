using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingGrid : MonoBehaviour
{
    [Header("Grid Size")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    [SerializeField] private Vector2 origin;

    [Header("Debug")]
    public Transform gridValueParent;
    public bool drawGridValue;
    public bool drawGridLines;

    private int[,] gridArray;
    private TextMeshPro[,] debugTextArray;

    public void BuildGrid()
    {
        gridArray = new int[width, height];
        debugTextArray = new TextMeshPro[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(0); y++)
            {
                if (drawGridValue)
                {
                    string name = "(" + x + "," + y + ")";
                    debugTextArray[x,y] = Utilities.CreateWorldText(gridValueParent, name, gridArray[x, y].ToString(), GetWorldPosition(x, y) + new Vector2(GetCellRadius(), GetCellRadius()), new Vector2(cellSize, cellSize), 2, Color.white, "Foreground");
                }
                if (drawGridLines)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 1000.0f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 1000.0f);
                }
            }
        }
        if (drawGridLines)
        {
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 1000.0f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 1000.0f);
        }
    }

    private Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * cellSize + origin;
    }

    private Vector2Int GetGridPosition(Vector2 worldPosition)
    {
        Vector2Int gridWorldPosition = new Vector2Int(0, 0);
        gridWorldPosition.x = Mathf.FloorToInt((worldPosition - origin).x / cellSize);
        gridWorldPosition.y = Mathf.FloorToInt((worldPosition - origin).y / cellSize);
        return gridWorldPosition;
    }

    public Vector3 RoundToGridPosition(Vector2 worldPosition)
    {
        Vector3 gridWorldPosition = new Vector3(0, 0, 0);
        gridWorldPosition.x = Mathf.FloorToInt((worldPosition).x / cellSize) + GetCellRadius();
        gridWorldPosition.y = Mathf.FloorToInt((worldPosition).y / cellSize) + GetCellRadius();
        return gridWorldPosition;
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        Vector2Int gridPosition = GetGridPosition(worldPosition);

        if (InsideGrid(worldPosition))
        {
            gridArray[gridPosition.x, gridPosition.y] = value;
            if (drawGridValue)
            {
                debugTextArray[gridPosition.x, gridPosition.y].text = gridArray[gridPosition.x, gridPosition.y].ToString();
            }
        }
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public float GetCellRadius()
    {
        return cellSize * 0.5f;
    }

    public float GetCellDiagonal()
    {
        return Mathf.Sqrt(cellSize * cellSize + cellSize * cellSize);
    }

    public int GetValue(Vector2 worldPosition)
    {
        Vector2Int gridPositon = GetGridPosition(worldPosition);

        if (InsideGrid(worldPosition))
        {
            return gridArray[gridPositon.x, gridPositon.y];
        }
        else
        {
            return -1;
        }
    }

    private bool InsideGrid(Vector2 worldPosition)
    {
        Vector2Int gridPosition = GetGridPosition(worldPosition);

        if (gridPosition.x >= 0 && gridPosition.y >= 0 && gridPosition.x < width && gridPosition.y < height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}