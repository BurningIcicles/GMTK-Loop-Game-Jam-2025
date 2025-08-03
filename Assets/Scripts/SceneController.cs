using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static readonly int Exit = Animator.StringToHash("Exit");
    private static readonly int Enter = Animator.StringToHash("Enter");
    private Animator _levelTransitionAnimator;
    private DontDestroyOnLoad _dontDestroyOnLoad;
    private Canvas _mainCanvas;

    private void Start()
    {
        _mainCanvas = GameObject.FindGameObjectWithTag("Main Canvas").GetComponent<Canvas>();
       
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ShowPauseMenu();
        }
    }

    private void ShowPauseMenu()
    {
        Time.timeScale = 0;
        GameObject pauseMenu = GameObject.FindGameObjectWithTag("Pause Container").transform.GetChild(0).gameObject;
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        Time.timeScale = 1;
        GameObject pauseMenu = GameObject.FindGameObjectWithTag("Pause Container").transform.GetChild(0).gameObject;
        pauseMenu.SetActive(false);
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
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
        _levelTransitionAnimator.SetTrigger(Exit);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        _levelTransitionAnimator.SetTrigger(Enter);
    }
}
