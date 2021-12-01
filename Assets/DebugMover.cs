using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }





    public class Tower : ScriptableObject
    {

        public int cost;     
    }

    [CreateAssetMenu(menuName = "Tower/RangedTower")]
    public class RangedTower : Tower
    {
        public float range;
    }

    [CreateAssetMenu(menuName = "Tower/Blockade")]
    public class Blockade : Tower
    {

    }
}
