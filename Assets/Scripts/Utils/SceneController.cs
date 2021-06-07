using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private Animator anim;

    public float transitionTime = 1f;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        GetComponentInChildren<CanvasGroup>().alpha = 1f;
    }

    public void GoToSceneOnClick(string nextScene)
    {
        StartCoroutine(goToScene(nextScene));
    }

    public IEnumerator goToScene(string nextScene)
    {
        anim.SetTrigger("Fading");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(nextScene);
    }
}
