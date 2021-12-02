using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionCyst : MonoBehaviour
{
    [Header("Infection Growth")]
    [SerializeField] private float infectionGrowthSpeed = 3.0f;
    [SerializeField] private float infectionRadius = 2.0f;
    
    private CircleCollider2D infectionCollider;
    private Transform infectionTransform;
    private float infectionDiameter;

    private void Start()
    {
        infectionDiameter = infectionRadius * infectionRadius; 
        infectionCollider = GetComponent<CircleCollider2D>();
        infectionTransform = transform.GetChild(0);
        StartCoroutine(SpreadInfection());
    }

    public IEnumerator SpreadInfection()
    {
        while(infectionCollider.radius < infectionRadius - 0.001f)
        {
            infectionTransform.localScale = Vector3.Lerp(infectionTransform.localScale, new Vector3(infectionDiameter, infectionDiameter, infectionDiameter), infectionGrowthSpeed * Time.deltaTime);
            infectionCollider.radius = Mathf.Lerp(infectionCollider.radius, infectionRadius, infectionGrowthSpeed * Time.deltaTime);
            yield return null;
        }
        infectionTransform.localScale = new Vector3(infectionDiameter, infectionDiameter, infectionDiameter);
        infectionCollider.radius = infectionRadius;
    }

    public void DestroyCyst()
    {

    }

    private IEnumerator RemoveInfection()
    {
        yield return new WaitForSeconds(1.0f);
    }
}