using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSphereController : MonoBehaviour
{
    private CMove move;
    private const float minX = -13;
    private const float maxX = 13;
    private const float minY = 1;
    private const float maxY = 7;
    private const float minZ = -13;
    private const float maxZ = 13;

    /////////////////
    // Start
    void Start()
    {
        move = new CMove();
    }

    private Vector3 SelectDirection()
    {
        if (transform.position.x < minX) return Vector3.right;
        if (transform.position.x > maxX) return Vector3.left;
        if (transform.position.y < minY) return Vector3.up;
        if (transform.position.y > maxY) return Vector3.down;
        if (transform.position.z < minZ) return Vector3.forward;
        if (transform.position.z > maxZ) return Vector3.back;

        Vector3 v = Random.insideUnitSphere;
        v.Normalize();
        return v;
    }

    /////////////////////////////////
    // Update
    void Update()
    {
        if (!move.IsActive())
        {
            move.SetPositions(transform.position, transform.position + SelectDirection());
            move.StartAction();
        }
        move.UpdatePosition();
        transform.position = move.GetCurrentPosition();
    }
}
