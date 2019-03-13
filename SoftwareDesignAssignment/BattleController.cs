using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
        public Party[] teams;

        //Constructor
        public BattleController(Party[] parties)
        {
            teams = parties;
            battleState = BattleState.Inactive;
            NextTeam();
        } 

        //Methods
        public void Update()
        {
            if (InputEngine.IsKeyPressed(Keys.P))
                NextTeam();
        }

        public void NextTeam()
        {
            foreach (var team in teams)
            {
                team.IsActive = false;
            }

            switch (battleState)
            {
                case BattleState.Inactive:
                case BattleState.PlayerOneTurn:
                    teams[0].IsActive = true;
                    teams[1].IsActive = false;
                    teams[0].DisplayUI(true);
                    teams[1].DisplayUI(false);
                    battleState = BattleState.PlayerTwoTurn;
                    break;

                case BattleState.PlayerTwoTurn:
                    teams[0].IsActive = false;
                    teams[1].IsActive = true;
                    teams[0].DisplayUI(false);
                    teams[1].DisplayUI(true);
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
