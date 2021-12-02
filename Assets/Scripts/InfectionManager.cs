using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class InfectionManager : MonoBehaviour
{
    public static InfectionManager Instance { get { return instance; } }
    private static InfectionManager instance;
    
    //[SerializeField] private List<InfectionCyst> infectionCysts = new List<InfectionCyst>();
    [Range(4,50)]
    [SerializeField] private int shapeResolution = 4;
    [SerializeField] private float width = 2;
    [SerializeField] private float height = 12;

    private SpriteShapeController spriteShapeController;
    private Spline spline;

    private float minY;
    private float maxY;
    private float spacing;

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
        spriteShapeController = GetComponentInChildren<SpriteShapeController>();
        spline = spriteShapeController.spline;

        /* Bottom Left */
        spline.SetPosition(0, new Vector3(-width * 0.5f, -height * 0.5f, 0));
        /* Top Left */
        spline.SetPosition(1, new Vector3(-width * 0.5f, height * 0.5f, 0));
        /* Top Right */
        spline.SetPosition(2, new Vector3(width * 0.5f, height * 0.5f, 0));
        /* Bottom Right */
        spline.SetPosition(3, new Vector3(width * 0.5f, -height * 0.5f, 0));

        minY = spline.GetPosition(0).y;
        maxY = spline.GetPosition(1).y;

        spacing = (maxY - minY) / shapeResolution -1;

        spline.InsertPointAt(3, spline.GetPosition(2) + new Vector3(0, -spacing));
        for (int y = 0; y < shapeResolution; y++)
        {
            //spline.InsertPointAt(3 + i, spline.GetPosition(2) + new Vector3(0, -spacing));
        }

        for (int i = 0; i < spline.GetPointCount(); i++)
        {
            
            Utilities.CreateWorldText(transform, "Spline: " + i, i.ToString(), spline.GetPosition(i), Vector2.one, 2, Color.white, "Foreground");
            Debug.Log("Pos: " + spline.GetPosition(i));
        }
        
    }

    private void Update()
    {
        //spline.SetPosition(3, spline.GetPosition(3) + new Vector3(Time.deltaTime, 0, 0));
    }
}