using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRestrictions : MonoBehaviour
{
    public Vector2 xBounds;
    public Vector2 zBounds;

    private void LateUpdate()
    {
        var currentX = transform.position.x;
        var currentZ = transform.position.z;

        currentX = Mathf.Clamp(currentX, xBounds.x, xBounds.y);
        currentZ = Mathf.Clamp(currentZ, zBounds.x, zBounds.y);

        transform.position = new Vector3(currentX, transform.position.y, currentZ);
    }
}
