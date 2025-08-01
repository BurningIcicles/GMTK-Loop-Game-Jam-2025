using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopController : MonoBehaviour
{
    private PlayerController _player;

    public void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    public void OnClick()
    {
        MovingPlatformController[] movingPlatforms = GameObject.FindObjectsOfType<MovingPlatformController>();
        foreach (MovingPlatformController movingPlatform in movingPlatforms)
        {
            movingPlatform.SetStartingPosition();
        }
        
        _player.Float();
    }
}
