using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

public class InfectionManager : MonoBehaviour
{
    public static InfectionManager Instance { get { return instance; } }
    private static InfectionManager instance;
    
    //[SerializeField] private List<InfectionCyst> infectionCysts = new List<InfectionCyst>();
    
    [Header("Infection Size")]
    [SerializeField] private float width = 2;
    [SerializeField] private float height = 12;
    [Range(2, 50)]
    [SerializeField] private int shapeResolution = 13;

    [Header("Infection Spread")]
    [Range(0.0f, 5.0f)]
    [SerializeField] private float spreadLength = 2.0f;
    [Range(0.0f, 10.0f)]
    [SerializeField] private float spreadDuration = 5.0f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float edgeSpeedFalloff = 0.5f;
    [Range(0.0f, 10.0f)]
    [SerializeField] private float edgePositionFalloff = 5.0f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float curveRoundness = 1.0f;
    [Range(0.0f, 5.0f)]
    [SerializeField] private float randomOffset = 1.0f;

    [Header("Debug")]
    [SerializeField] private bool drawPoints;

    private List<InfectionPoint> infectionPoints = new List<InfectionPoint>();
    
    private Spline spline;
    private Vector3 centerPosition;
    private float horizontalTargetPosition; 
    private float spreadLerpTime;
    private float spacing;
    private float minX;
    private bool spreadingInfection;

    public bool SpreadingInfection { get { return spreadingInfection; } }

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
        InitializeSpriteShape();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spread();
        }
    }

    private void InitializeSpriteShape()
    {
        spline = GetComponentInChildren<SpriteShapeController>().spline;
        horizontalTargetPosition = width * 0.5f;

        /* Bottom Left */
        spline.SetPosition(0, new Vector3(-width * 0.5f, -height * 0.5f, 0));
        /* Top Left */
        spline.SetPosition(1, new Vector3(-width * 0.5f, height * 0.5f, 0));
        /* Top Right */
        spline.SetPosition(2, new Vector3(width * 0.5f, height * 0.5f, 0));
        /* Bottom Right */
        spline.SetPosition(3, new Vector3(width * 0.5f, -height * 0.5f, 0));

        minX = spline.GetPosition(0).x;
        int pointsToAdd = shapeResolution - 2;
        spacing = height / (shapeResolution - 1);
        centerPosition = spline.GetPosition(3) + new Vector3(0, height * 0.5f);

        /* Bottom Point */
        infectionPoints.Add(new InfectionPoint(3 + pointsToAdd, spline.GetPosition(3)));

        /* Middle Points */
        for (int y = 0; y < pointsToAdd; y++)
        {
            Vector3 pointPosition = spline.GetPosition(2) + (new Vector3(0, -spacing * (pointsToAdd - y)));
            pointPosition = OffsetPosition(pointPosition);
            spline.InsertPointAt(3, pointPosition);
            infectionPoints.Add(new InfectionPoint(2 + pointsToAdd - y, spline.GetPosition(3)));
            SetPointTangents(3);
        }

        /* Top Point */
        infectionPoints.Add(new InfectionPoint(2, spline.GetPosition(2)));

        /* Sort List by Index */
        infectionPoints = infectionPoints.OrderBy(infectionPoint => infectionPoint.index).ToList();

        /* Create TextMesh for each point for debugging */
        if (drawPoints)
        {
            for (int i = 0; i < spline.GetPointCount(); i++)
            {
                Utilities.CreateWorldText(transform, "Spline: " + i, i.ToString(), spline.GetPosition(i), Vector2.one, 2, Color.white, "Foreground");
            }
        }
    }

    private void SetPointTangents(int index)
    {
        float maxTangentValue = spacing * 0.5f;
        spline.SetTangentMode(index, ShapeTangentMode.Continuous);
        spline.SetLeftTangent(index, Vector3.up * maxTangentValue * curveRoundness);
        spline.SetRightTangent(index, Vector3.down * maxTangentValue * curveRoundness);
    }

    private void UpdateHorizontalTargetPosition()
    {
        float increment = horizontalTargetPosition - centerPosition.x;
        for (int i = 0; i < infectionPoints.Count; i++)
        {
            Vector3 newPosition = infectionPoints[i].position;
            newPosition.x = centerPosition.x;
            newPosition.x += increment * (EdgeFalloffMultiplier(newPosition, edgePositionFalloff));
            newPosition = OffsetPosition(newPosition);
            newPosition.x = Mathf.Clamp(newPosition.x, minX + 0.1f, Mathf.Infinity);
            infectionPoints[i].targetPosition = newPosition;
            infectionPoints[i].startPosition = infectionPoints[i].position;
            
        }
        centerPosition.x = horizontalTargetPosition;
    }

    private Vector3 OffsetPosition(Vector3 position)
    {
        Vector3 offsetPosition = position;
        offsetPosition.x += Random.Range(0, -randomOffset);
        return offsetPosition;
    }

    private float EdgeFalloffMultiplier(Vector3 position, float falloffValue)
    {
        float edgeFalloffMultiplier = 1 - (Vector3.Distance(position, centerPosition) * falloffValue / height);
        return edgeFalloffMultiplier;
    }

    public void Spread()
    {
        if (!spreadingInfection)
        {
            StartCoroutine(SpreadInfection());
        }
    }

    private IEnumerator SpreadInfection()
    {
        spreadingInfection = true;
        horizontalTargetPosition += spreadLength;
        UpdateHorizontalTargetPosition();
        spreadLerpTime = 0;

        List<InfectionPoint> pointsToMove = new List<InfectionPoint>(infectionPoints);
        
        while(pointsToMove.Count > 0)
        {
            spreadLerpTime += Time.deltaTime / spreadDuration;

            for (int i = 0; i < pointsToMove.Count; i++)
            {
                /* Lerp SpriteShapePoint Towards targetPosition */
                float edgeFalloffMultiplier = EdgeFalloffMultiplier(pointsToMove[i].position, edgeSpeedFalloff);
                pointsToMove[i].position = Vector3.Lerp(pointsToMove[i].startPosition, pointsToMove[i].targetPosition, spreadLerpTime * edgeFalloffMultiplier);
                spline.SetPosition(pointsToMove[i].index, pointsToMove[i].position);

                /* Remove Point if TargetReached */
                if (pointsToMove[i].TargetReached())
                {
                    pointsToMove.RemoveAt(i);
                }
            }
            yield return null;
        }
        spreadingInfection = false;
    }
}