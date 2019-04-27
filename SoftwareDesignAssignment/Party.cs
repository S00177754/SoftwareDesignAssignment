using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public List<UIStatBlock> MemberStats { get; private set; }
        public string PlayerName { get; private set; }
        public bool IsActive { get; set; } = false;

        //Constructor
        public Party(Game game,string playerName):this(game,playerName,new List<Character>(),Color.Gray)
        {   
        }
        public Party(Game game,string playerName,List<Character> partyMembers,Color partyColor)
        {
            PlayerName = playerName;
            Members = partyMembers;
            MemberStats = new List<UIStatBlock>();

            for (int i = 0; i < 4; i++)
            {
                MemberStats.Add(new UIStatBlock(game, Members[i], game.Content.Load<Texture2D>(@"Textures\WhiteSquare"), new Vector2((10 + 100 * i), game.GraphicsDevice.Viewport.Height - 50), 100, 40, partyColor));
                
            }

            DisplayUI(false);
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

        public void DisplayUI(bool value)
        {
            foreach (var statBlocks in MemberStats)
            {
                statBlocks.Visible = value;
            }
        }

        public int HasNotMoved()
        {
            int temp = 4;
            foreach (var member in Members)
            {
                if (member.HasMoved == true)
                    temp--;
            }
            return temp;
        }

        public void SetActive(bool value)
        {
            foreach (var member in Members)
            {
                member.IsActive = value;
                if (value == true)
                    member.StartTurn();
            }
        }

       
    }
}
