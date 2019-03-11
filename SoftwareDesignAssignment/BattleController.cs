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
        } 

        //Methods
        public void NextTeam()
        {

        }
        

    }
}
