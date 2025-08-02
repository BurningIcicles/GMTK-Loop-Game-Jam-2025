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
    private bool _forward;
    private bool _loop;
    void Start()
    {
        _forward = true;
        _loop = false;
        SetStartingPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = GetTarget();
        platform.transform.position = Vector2.Lerp(platform.transform.position, target, Time.deltaTime);
        
        float distance = Vector2.Distance(platform.transform.position, target);
        if (_loop && distance <= 0.1f)
            _forward = !_forward;

    }

    private Vector2 GetTarget()
    {
        return _forward ? endPoint.transform.position : startPoint.transform.position;
    }

    private void OnDrawGizmos()
    {
        if (startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(startPoint.transform.position, endPoint.transform.position);
        }
    }

    public void SetStartingPosition()
    {
        platform.transform.position = startPoint.transform.position;
    }
}
