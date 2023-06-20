using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    public List<Material> images;
    public float moveSpeed;

    public GameObject previousPoint, nextPoint;
    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0, images.Count);
        transform.GetChild(0).GetComponent<MeshRenderer>().material = images[random];
        GetNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPoint.transform.position, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, nextPoint.transform.position) < 0.1f) {
            //arrived
            previousPoint = nextPoint;
            GetNextPoint();
        }
    }

    public void GetNextPoint() {
        nextPoint = previousPoint.GetComponent<WalkPoint>().GiveRandomNextPoint().gameObject;
    }
}
