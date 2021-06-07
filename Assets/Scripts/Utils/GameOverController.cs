using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = StateController.totalScore.ToString() + " pontos";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Main Scene");
    }
}
