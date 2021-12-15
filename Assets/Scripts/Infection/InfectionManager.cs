using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

public class InfectionManager : MonoBehaviour
{
    public enum SpreadSetting
    {
        Constant, Intervals
    }

    [Header("Infection Size")]
    [Range(1, 100)]
    [SerializeField] public int width = 12;
    [Range(1, 100)]
    [SerializeField] public int height = 12;
    [Range(2, 50)]
    [SerializeField] private int shapeResolution = 13;

    [Header("Infection Spread")]
    [SerializeField] public SpreadSetting spreadSetting = SpreadSetting.Constant;
    [Range(0.0f, 10.0f)]
    [SerializeField] public float constantSpreadSpeed = 0.25f;
    [Range(0.0f, 5.0f)]
    [SerializeField] private float spreadSpeedRandomOffset = 2.5f;
    [SerializeField] private AnimationCurve spreadSpeedCurve;
    [Range(0.0f, 5.0f)]
    [SerializeField] private float spreadLength = 1.0f;
    [Range(0.0f, 10.0f)]
    [SerializeField] private float spreadDuration = 5.0f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float edgeSpeedFalloff = 0.5f;
    [Range(0.0f, 10.0f)]
    [SerializeField] private float edgePositionFalloff = 7.5f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float curveRoundness = 1.0f;
    [Range(0.0f, 5.0f)]
    [SerializeField] private float randomOffset = 1.0f;

    [Header("Infection Pushback Speed")]
    [SerializeField] private int temporaryInfectionPushbackTime;
    [SerializeField] private float temporarySpreadPushbackSpeed;
    [SerializeField] private int pushbackInfectionCost;

    [Header("Infection Stop")]
    [SerializeField] private int temporaryInfectionPauseTime;
    [SerializeField] private int stopInfectionCost;
    private float infectionSpreadNormalSpeed;

    [Header("On Lives Lost Infection Speed Increase")]
    [SerializeField] [Range(0, 10)] float tempSpeedToIncrease;
    [SerializeField] int tempTimeForIncrease;

    [Header("Infection Stats")]
    [SerializeField] GameObject infectionStatsUI;
    private bool infectionStatsShown;
    List<Tower> towers = new List<Tower>();

    private bool addedSpeed;
    private bool slow;
    private float tempTimer;

    [Header("Debug")]
    [SerializeField] private bool drawPoints;



    private List<InfectionPoint> infectionPoints = new List<InfectionPoint>();

    private Spline spline;
    private Vector3 centerPosition;
    private float horizontalTargetPosition;
    private float spreadCurveTime;
    private float spreadLerpTime;
    private float spacing;
    private float minX;

    private bool spreadingInfection;
    public bool constantGrowth;

    public bool SpreadingInfection { get { return spreadingInfection; } }

    [SerializeField] private SpawnPoint spawnPoint;

    private void Start()
    {
        infectionSpreadNormalSpeed = constantSpreadSpeed;
        InitializeSpriteShape();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spread();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ToggleSpread(false);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ToggleSpread(true);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            SetSpreadSpeed(constantSpreadSpeed -= 0.1f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            SetSpreadSpeed(constantSpreadSpeed += 0.1f);
        }
        ConstantGrowth();
        FastInfectionSpeedChanger();
    }

    public void StopInfection()
    {
        if(stopInfectionCost <= GameManager.Instance.PlayerCurrency.playerInfectedCurrency)
        {
            ChangeSlowInfectionSpeed(temporaryInfectionPauseTime, -1f);
            GameManager.Instance.PlayerCurrency.RemovePlayerInfectedCurrency(stopInfectionCost);
        }
    }

    public void PushBackInfection()
    {
        if(pushbackInfectionCost <= GameManager.Instance.PlayerCurrency.playerInfectedCurrency)
        {
            float speed = -temporarySpreadPushbackSpeed;
            ChangeSlowInfectionSpeed(temporaryInfectionPushbackTime, speed);
            GameManager.Instance.PlayerCurrency.RemovePlayerInfectedCurrency(pushbackInfectionCost);
        }
    }

    public void IncreaseInfectionSpeed()
    {
        tempTimer = tempTimeForIncrease;
        addedSpeed = true;
    }

    private void ChangeSlowInfectionSpeed(int duration, float speed)
    {
        StartCoroutine(SlowInfectionSpeed(duration, speed));
    }

    private void FastInfectionSpeedChanger()
    {
        if (tempTimer > 0)
        {
            tempTimer -= 1 * Time.deltaTime;
        }
        if (tempTimer <= 0)
        {
            addedSpeed = false;
        }
        if (!slow && addedSpeed)
        {
            constantSpreadSpeed = infectionSpreadNormalSpeed * (1 + tempSpeedToIncrease);
        }
        if (!slow && !addedSpeed)
        {
            SetSpreadSpeed(infectionSpreadNormalSpeed);
        }
    }

    private IEnumerator SlowInfectionSpeed(int duration, float speed)
    {
        slow = true;
        constantSpreadSpeed = infectionSpreadNormalSpeed * (1 + speed);
        yield return new WaitForSeconds(duration);
        if (!addedSpeed)
        {
            SetSpreadSpeed(infectionSpreadNormalSpeed);
        }
        slow = false;
    }

    public void SetSpreadSpeed(float value)
    {
        constantSpreadSpeed = value;
    }

    public void ToggleSpread(bool state)
    {
        if (state)
        {
            constantGrowth = true;
        }
        else
        {
            constantGrowth = false;
        }
    }

    private void ConstantGrowth()
    {
        if (constantGrowth)
        {
            for (int i = 0; i < infectionPoints.Count; i++)
            {
                infectionPoints[i].position.x += spreadSpeedCurve.Evaluate(spreadCurveTime + i * i * spreadSpeedRandomOffset) * constantSpreadSpeed * Time.deltaTime;
                spline.SetPosition(infectionPoints[i].index, infectionPoints[i].position);
                spreadCurveTime += Time.deltaTime;
            }
        }
    }

    private void InitializeSpriteShape()
    {
        UpdateSpriteShape();

        /* Create TextMesh for each point for debugging */
        if (drawPoints)
        {
            for (int i = 0; i < spline.GetPointCount(); i++)
            {
                Utilities.CreateWorldText(transform, "Spline: " + i, i.ToString(), spline.GetPosition(i), Vector2.one, 2, Color.white, "Foreground");
            }
        }
    }

    [ExecuteInEditMode]
    public void UpdateSpriteShape()
    {
        /* Clear Spline Points*/
        spline = GetComponentInChildren<SpriteShapeController>().spline;
        spline.Clear();

        /* Bottom Right */
        spline.InsertPointAt(0, new Vector3((float)(width * 0.5f), (float)(-height * 0.5f), 0));
        /* Top Right */
        spline.InsertPointAt(0, new Vector3((float)(width * 0.5f), (float)(height * 0.5f), 0));
        /* Top Left */
        spline.InsertPointAt(0, new Vector3((float)(-width * 0.5f), (float)(height * 0.5f), 0));
        /* Bottom Left */
        spline.InsertPointAt(0, new Vector3((float)(-width * 0.5f), (float)(-height * 0.5f), 0));

        /* Set Initial values */
        minX = spline.GetPosition(0).x;
        int pointsToAdd = shapeResolution - 2;
        spacing = (float)height / (shapeResolution - 1);
        centerPosition = spline.GetPosition(3) + new Vector3(0, (float)(height * 0.5f));


        /* Add Bottom Point to Spline Point List */
        infectionPoints.Add(new InfectionPoint(3 + pointsToAdd, spline.GetPosition(3)));

        /* Create Middle Points and add to Spline Point List */
        for (int y = 0; y < pointsToAdd; y++)
        {
            Vector3 pointPosition = spline.GetPosition(2) + (new Vector3(0, -spacing * (pointsToAdd - y)));
            spline.InsertPointAt(3, pointPosition);
            infectionPoints.Add(new InfectionPoint(2 + pointsToAdd - y, spline.GetPosition(3)));
            SetPointTangents(3);
        }

        /* Add Top Point to Spline Point List */
        infectionPoints.Add(new InfectionPoint(2, spline.GetPosition(2)));

        /* Sort List by Index */
        infectionPoints = infectionPoints.OrderBy(infectionPoint => infectionPoint.index).ToList();

        /* Apply Initial spreadLength so we can create the Sprite Shape */
        horizontalTargetPosition = centerPosition.x;
        horizontalTargetPosition += spreadLength;

        /* Set Spline Point positions according to inspector values */
        for (int i = 0; i < infectionPoints.Count; i++)
        {
            Vector3 newPosition = infectionPoints[i].position;
            newPosition.x = centerPosition.x;
            newPosition.x += spreadLength * (EdgeFalloffMultiplier(newPosition, edgePositionFalloff));
            newPosition = OffsetPosition(newPosition);
            infectionPoints[i].position = newPosition;
            infectionPoints[i].startPosition = newPosition;
            infectionPoints[i].targetPosition = newPosition;
        }

        /* Update centerPosition after setting Spline Point targets */
        centerPosition.x = horizontalTargetPosition;

        /* Apply Spline Point targetPositions */
        for (int j = 0; j < infectionPoints.Count; j++)
        {
            try
            {
                spline.SetPosition(infectionPoints[j].index, infectionPoints[j].targetPosition);
            }
            /* Sprite Shape not wide enough to offset Spline Points */
            catch
            {
                Debug.LogWarning("Points too close to neighbor: Try increasing Width.");
            }
        }

        /* Draw current centerPosition / previous targetPosition */
        Debug.DrawLine(transform.TransformPoint(centerPosition + (new Vector3(0, height, 0) * 0.5f)), transform.TransformPoint(centerPosition + (new Vector3(0, -height, 0) * 0.5f)), Color.red, 1.0f);
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
        float edgeFalloffMultiplier = 1 - (float)(Vector3.Distance(position, centerPosition) * falloffValue / height);
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

        Debug.DrawLine(transform.TransformPoint(new Vector3(centerPosition.x, (float)(height * 0.5f))), transform.TransformPoint(new Vector3(centerPosition.x, (float)(-height * 0.5f))), Color.yellow, 20.0f);
        while (pointsToMove.Count > 0)
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

    public void ToggleInfectionStats() //Shows/hides Speed %, Health %, Extra enemies #, tower infection score #
    {
        if (!infectionStatsShown)
        {
            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].ShowInfectionScore();
            }
            infectionStatsUI.SetActive(true);
            infectionStatsShown = true;
        }
        else if (infectionStatsShown)
        {
            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].HideInfectionScore();
            }
            infectionStatsUI.SetActive(false);
            infectionStatsShown = false;
        }
    }

    public void AddTowerToList(Tower tower)
    {
        towers.Add(tower);
        if (infectionStatsShown)
        {
            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].ShowInfectionScore();
            }
        }
    }

    public void RemoveTowerFromList(Tower tower)
    {
        towers.Remove(tower);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Tower"))
        {
            Debug.Log("aaaahh!!! da performance hiadhoaidhas");
            collision.gameObject.GetComponent<Tower>().InfectTower();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Cyst"))
        {
            spawnPoint.AddNewSpawn(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Cyst"))
        {
            spawnPoint.RemoveOldSpawn(collision.gameObject);
        }
    }
}