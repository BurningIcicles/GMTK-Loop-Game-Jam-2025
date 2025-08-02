using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneController : MonoBehaviour
{
    private static readonly int Exit = Animator.StringToHash("Exit");
    private static readonly int Enter = Animator.StringToHash("Enter");
    private Animator _levelTransitionAnimator;
    private DontDestroyOnLoad _dontDestroyOnLoad;

    private void Start()
    {
        Animator[] animators = FindObjectsOfType<Animator>();
        Debug.Log($"Found {animators.Length} animators");
        foreach (Animator animator in animators)
        {
            Debug.Log($"Found {animator.gameObject.name} animator");
            if (animator.GetComponent<Animator>().name == "Level Transition")
            {
                _levelTransitionAnimator = animator;
                break;
            }
        }
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        _levelTransitionAnimator.SetTrigger(Exit);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        _levelTransitionAnimator.SetTrigger(Enter);
    }
}
