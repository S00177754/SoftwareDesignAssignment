using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsaaCAassessment2019.Classes
{
    public static class Scoreboard
    {
        //Variables and Collections
        public static LinkedList<PlayerScore> ScoreList { get; set; } = new LinkedList<PlayerScore>();

        //Initialisation Methods
        public static void GenerateFake()
        {
                PlayerScore[] scores = new PlayerScore[]
                {
                    new PlayerScore("Ben",310),
                    new PlayerScore("Tom",10),
                    new PlayerScore("Jerry",201),
                    new PlayerScore("Larry",184),
                    new PlayerScore("Carl",79),
                    new PlayerScore("Mary",164),
                    new PlayerScore("Corrin",286),
                    new PlayerScore("Sinead",1),
                    new PlayerScore("Betty",437),
                    new PlayerScore("Lorraine",103),
                };

                ScoreList = new LinkedList<PlayerScore>(scores);

                SortList();
        }

        //Manipulation Methods
        public static void InsertNewScore(string playerName, int score)
        {
            foreach (PlayerScore item in ScoreList)
            {
                if (item.Score >= score)
                {
                    ScoreList.AddBefore(ScoreList.Find(item), new PlayerScore(playerName, score));
                    break;
                }
            }

            SortList();
            
        }

        public static void SortList()
        {
            var ordered = ScoreList.OrderByDescending(i => i.Score);
            ScoreList = new LinkedList<PlayerScore>(ordered);
        }

    }

    public class PlayerScore
    {
        //Variables
        public string PlayerName { get; set; }
        public int Score { get; set; }

        //Constructors
        public PlayerScore(string name,int score)
        {
            PlayerName = name;
            Score = score;
        }

        //Overrides
        public override string ToString()
        {
            return string.Format("{0,-16} Score:{1}",PlayerName,Score);
        }
    }

}
