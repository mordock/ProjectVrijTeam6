using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPoint : MonoBehaviour
{
    public List<WalkPoint> surroundingPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public WalkPoint GiveRandomNextPoint() {
        int random = Random.Range(0, surroundingPoints.Count);

        return surroundingPoints[random];
    }
}
