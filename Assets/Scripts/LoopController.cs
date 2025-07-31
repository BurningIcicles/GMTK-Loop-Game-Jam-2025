using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopController : MonoBehaviour
{
    public void OnClick()
    {
        MovingPlatformController[] movingPlatforms = GameObject.FindObjectsOfType<MovingPlatformController>();
        foreach (MovingPlatformController movingPlatform in movingPlatforms)
        {
            movingPlatform.SetStartingPosition();
        }
    }
}
