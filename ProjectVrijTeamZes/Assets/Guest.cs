using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    public List<Sprite> images;
    public float moveSpeed;

    public GameObject previousPoint, nextPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPoint.transform.position, moveSpeed * Time.deltaTime);
    }

    public void GetNextPoint() {
        nextPoint = previousPoint.GetComponent<WalkPoint>().GiveRandomNextPoint().gameObject;
    }
}
