using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LeaderboardData
{
    [System.Serializable]
    public class Leaderboard
    {
        public Text name;
        public Text company;
        public Text score;

        public GameObject line;
    }
}
