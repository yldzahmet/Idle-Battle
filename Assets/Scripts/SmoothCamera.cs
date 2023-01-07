using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public GameObject target;
    public GameObject midPoint;
    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        offset = transform.position - target.transform.position;
    }
    private void FixedUpdate()
    {
        midPoint.transform.position = target.transform.position;

        transform.position = Vector3.SmoothDamp(transform.position, midPoint.transform.position + offset, ref velocity, 0.3f, 100f, Time.deltaTime);
    }
}
