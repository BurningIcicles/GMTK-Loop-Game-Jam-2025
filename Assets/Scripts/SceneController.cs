using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static readonly int Exit = Animator.StringToHash("Exit");
    private static readonly int Enter = Animator.StringToHash("Enter");
    private Animator _levelTransitionAnimator;
    private DontDestroyOnLoad _dontDestroyOnLoad;

    private void Start()
    {
        Animator[] animators = FindObjectsOfType<Animator>();
        foreach (Animator animator in animators)
        {
            if (animator.GetComponent<Animator>().name == "Level Transition")
            {
                _levelTransitionAnimator = animator;
                break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("escape");
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
