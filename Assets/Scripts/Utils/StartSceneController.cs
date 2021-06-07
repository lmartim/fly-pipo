using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class StartSceneController : MonoBehaviour
{
    public GameObject Transitioner;
    public GameObject howToPlayContainer;
    public GameObject playerDataContainer;
    public Button playButton;
    public Button howToPlayButton;

    public GameObject playerNameInput;
    public GameObject playerCompanyInput;

    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public void btnPressed()
    {
        if (IsMobile())
        {
            Transitioner.GetComponent<SceneController>().GoToSceneOnClick("Main Scene");
            return;
        }

        playerDataContainer.SetActive(true);

        if (playButton)
            playButton.gameObject.SetActive(false);

        if (howToPlayButton)
            howToPlayButton.gameObject.SetActive(false);

        if (howToPlayContainer)
            howToPlayContainer.SetActive(false);
    }

    public void hideContainer()
    {
        playerDataContainer.SetActive(false);

        if (playButton)
            playButton.gameObject.SetActive(true);

        if (howToPlayButton)
            howToPlayButton.gameObject.SetActive(true);

        if (howToPlayContainer)
            howToPlayContainer.SetActive(true);
    }

    public void playGame()
    {
        StateController.playerName = playerNameInput.GetComponent<InputField>().text;
        StateController.playerCompany = playerCompanyInput.GetComponent<InputField>().text;
        Transitioner.GetComponent<SceneController>().GoToSceneOnClick("Main Scene");
    }
}
