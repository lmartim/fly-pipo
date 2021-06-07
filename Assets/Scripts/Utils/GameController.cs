using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class GameController : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;

    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public GameObject MobileButtons;

    private void Start()
    {
        if (IsMobile())
        {
            Debug.Log("teste");
            MobileButtons.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Pontos: " + score.ToString();
    }
}
