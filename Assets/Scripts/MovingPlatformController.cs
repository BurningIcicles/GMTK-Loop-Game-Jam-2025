using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject platform;
    [SerializeField]
    private GameObject startPoint;
    [SerializeField]
    private GameObject endPoint;
    [SerializeField]
    private float speed;
    [SerializeField] 
    private float delay;

    // if forward is true, move platform from start to end position
    // else, move platform from end to start position
    private bool forward;
    void Start()
    {
        forward = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = GetTarget();
        platform.transform.position = Vector2.Lerp(platform.transform.position, target, Time.deltaTime);
        
        float distance = Vector2.Distance(platform.transform.position, target);
        if (distance <= 0.1f)
            forward = !forward;

    }

    private Vector2 GetTarget()
    {
        return forward ? endPoint.transform.position : startPoint.transform.position;
    }

    private void OnDrawGizmos()
    {
        if (startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(gameObject.transform.position, startPoint.transform.position);
            Gizmos.DrawLine(gameObject.transform.position, endPoint.transform.position);
        }
    }

    public void StartMoving()
    {
        platform.transform.position = startPoint.transform.position;
    }
}
