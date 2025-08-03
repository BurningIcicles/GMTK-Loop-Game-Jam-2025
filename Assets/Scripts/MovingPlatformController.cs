using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    private GameObject _platform;
    private GameObject _startPoint;
    private GameObject _endPoint;
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
        var children = new List<Transform>();
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            children.Add(this.gameObject.transform.GetChild(i));
        }

        for (int i = 0; i < children.Count; i++)
        {
            if (children[i].gameObject.CompareTag("Start Position"))
            {
                _startPoint = children[i].gameObject;
            } else if (children[i].gameObject.CompareTag("End Position"))
            {
                _endPoint = children[i].gameObject;
            } else if (children[i].gameObject.CompareTag("Floor"))
            {
                _platform = children[i].gameObject;
            }
        }
        
        SetStartingPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = GetTarget();
        _platform.transform.position = Vector2.Lerp(_platform.transform.position, target, Time.deltaTime);
        
        float distance = Vector2.Distance(_platform.transform.position, target);
        if (_loop && distance <= 0.1f)
            _forward = !_forward;

    }

    private Vector2 GetTarget()
    {
        return _forward ? _endPoint.transform.position : _startPoint.transform.position;
    }

    private void OnDrawGizmos()
    {
        if (_startPoint != null && _endPoint != null)
        {
            if (!Mathf.Approximately(_startPoint.transform.position.y, _endPoint.transform.position.y))
            {
                Gizmos.color = new Color(1f, 0f, 0f, 1); // Yellow with custom alpha
            }
            else
            {
                Gizmos.color = new Color(0f, 1f, 0f, 1); // Yellow with custom alpha
            }
            
            Gizmos.DrawLine(_startPoint.transform.position, _endPoint.transform.position);
        }
    }

    public void SetStartingPosition()
    {
        _platform.transform.position = _startPoint.transform.position;
    }
}
