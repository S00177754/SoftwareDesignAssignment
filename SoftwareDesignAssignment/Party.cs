using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignAssignment
{
    public class Party
    {
        //Variables
        public List<Character> Members { get; private set; }
        public string PlayerName { get; private set; }

        //Constructor
        public Party(string playerName):this(playerName,new List<Character>())
        {   
        }
        public Party(string playerName,List<Character> partyMembers)
        {
            PlayerName = playerName;
            Members = partyMembers;
        }

        //Methods
        public void AddMember(Character character)
        {
            Members.Add(character);
        }

        public void RemoveMember(Character character)
        {
            Members.Remove(character);
        }

        public void SwapMembers(Character newCharacter,Character oldCharacter)
        {
            RemoveMember(oldCharacter);
            AddMember(newCharacter);
        }
    }
}
