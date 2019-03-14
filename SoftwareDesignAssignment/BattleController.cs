using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignAssignment
{
    public enum BattleState { PlayerOneTurn,PlayerTwoTurn,Inactive}

    public class BattleController
    {
        //Variables
        public BattleState battleState;
        static public Party[] teams;
        

        //Constructor
        public BattleController(Party[] parties)
        {
            teams = parties;
            battleState = BattleState.Inactive;
            //NextTeam();
            teams[0].DisplayUI(false);
            teams[1].DisplayUI(false);
        } 

        //Methods
        public void Update()
        {
            switch (battleState)
            {
                case BattleState.PlayerOneTurn:
                    if(teams[0].HasNotMoved()<= 0)
                    {
                        NextTeam();
                    }
                    break;

                case BattleState.PlayerTwoTurn:
                    if (teams[1].HasNotMoved()<= 0)
                    {
                        NextTeam();
                    }
                    break;

                case BattleState.Inactive:
                    if (Game1.gameState == GameState.Playing)
                    {
                        NextTeam();
                    }
                    break;
            }
        }

        public void NextTeam()
        {
            Debug.WriteLine("Next Team");

            foreach (var team in teams)
            {
                team.IsActive = false;
            }

            switch (battleState) //current turn
            {
                
                case BattleState.PlayerOneTurn:
                    Debug.WriteLine("Set Team Two");
                    teams[0].DisplayUI(false);
                    teams[1].DisplayUI(true);
                    teams[0].SetActive(false);
                    teams[1].SetActive(true);
                    battleState = BattleState.PlayerTwoTurn;

                    break;

                case BattleState.Inactive:
                case BattleState.PlayerTwoTurn:
                    Debug.WriteLine("Set Team One");
                    teams[0].DisplayUI(true);
                    teams[1].DisplayUI(false);
                    teams[0].SetActive(true);
                    teams[1].SetActive(false);
                    battleState = BattleState.PlayerOneTurn;

                    break;

                default:
                    throw new Exception("Error: Invalid Team State");
            }
        }

        public int PartyMembersLeft(Party party)
        {
            int deathCount = 4;
            foreach (var team in party.Members)
            {
                if(team.Health <= 0)
                {
                    deathCount--;
                }
            }
            return deathCount;
        }

        public Party FindWinner(Party partyOne, Party partyTwo)
        {
            if(PartyMembersLeft(partyTwo) <= 0 && PartyMembersLeft(partyOne) <= 0)
            {
                return null;
            }
            else if (PartyMembersLeft(partyTwo) <= 0)
            {
                return partyOne;
            }
            else if(PartyMembersLeft(partyOne) <= 0)
            {
                return partyTwo;
            }
            else
            {
                throw new Exception("Error: Winner Not Found.");
            }

        }
        

    }
}
