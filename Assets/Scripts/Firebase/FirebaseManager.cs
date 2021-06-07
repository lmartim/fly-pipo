using System.Collections;
using System.Collections.Generic;
using FirebaseWebGL.Scripts.FirebaseBridge;
using UnityEngine;
using LeaderboardData;
using SimpleJSON;

public class User
{
    public string name;
    public string company;
    public int score;

    public User()
    {
    }

    public User(string name, string company, int score)
    {
        this.name = name;
        this.company = company;
        this.score = score;
    }
}

public class FirebaseManager : MonoBehaviour
{
    public Leaderboard[] uiElements;

    private void Start()
    {
        //GetJSON();

        string playerName = StateController.playerName;
        string playerCompany = StateController.playerCompany;
        int playerScore = StateController.totalScore;

        if (playerName.Length > 0)
        {
            WriteNewUser(playerName, playerCompany, playerScore);
        } else
        {
            GetData();
        }
    }

    private void WriteNewUser(string name, string company, int score)
    {
        FirebaseDatabase.PushJSON("Leaderboard", name, company, score, gameObject.name,
            "GetData", "DisplayErrorObject");
    }

    public void GetData()
    {
        FirebaseDatabase.GetJSON("Leaderboard", gameObject.name, 
            "DisplayData", "DisplayErrorObject");
    }

    public void DisplayData(string data)
    {
        JSONNode parsedData = JSON.Parse(data);

        for (int i = 0; i < 10; i++)
        {
            if (!parsedData[i].Equals(null))
            {
                uiElements[i].line.SetActive(true);

                uiElements[i].name.text = (i + 1).ToString() + ". " + parsedData[i][1];
                uiElements[i].company.text = parsedData[i][0];
                uiElements[i].score.text = parsedData[i][2].ToString();
            }
        }
    }

    public void DisplayErrorObject(string error)
    {
        Debug.Log("DEU RUIM: " + error);
    }
}
