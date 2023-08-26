using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_1_Transition : MonoBehaviour
{
    public Animator animator;
    private int nextScene;
    public int sceneToLoad;
    private GameController gameController;
    private Stage2Controller stage2Controller;
    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot Find'GameController' script");
        }

     
    }
    public void SceneTransition(int levelIndex)
    {
        nextScene = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void TransitionCompleted()
    {
        SceneManager.LoadScene(nextScene);
    }
    IEnumerator makeTransition()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(sceneToLoad);
    }
    // Update is called once per frame
    void Update()
    {
        if (gameController.returnGame && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(0);
        }

        if (gameController.ReturnBossDestroyed())
        {
            StartCoroutine(makeTransition());
        }
    }
}
