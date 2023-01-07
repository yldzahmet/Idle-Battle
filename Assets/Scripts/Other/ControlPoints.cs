using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoints: MonoBehaviour
{
    public Color color;
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
