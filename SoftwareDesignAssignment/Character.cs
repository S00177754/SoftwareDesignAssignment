using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SoftwareDesignAssignment
{
    public abstract class Character : Sprite, IClickable
    {
        //Variables
        public int Health { get; private set; }
        public int MagicPoints { get; private set; }
        public int[] gridCell;
        public int MovementRange { get; private set; }
        public int AttackRange { get; private set; }
        public bool HasMoved { get; private set; } = false;
        public bool IsDead { get; private set; } = false;
        public Element ElementalType { get; private set; }
        public Rectangle ClickBox { get; set; }

        public Character(int health, int magicPoints,Element elementType, Texture2D texture, Vector2 userPosition, int frameCount, OriginType origin) : base(texture, userPosition, frameCount, origin)
        {
            Health = health;
            MagicPoints = magicPoints;
            ClickBox = CollisionField;
            ElementalType = elementType;
        }

        //Methods
        public void StartTurn()
        {
            HasMoved = false;
        }

        public void Move()
        {

        }


        //Overrides
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if(Health <= 0)
            {
                Visible = false;
                IsDead = true;
            }
            base.Update(gameTime);
        }

        public bool ClickCheck()
        {
            if(InputEngine.IsMouseLeftClick() && ClickBox.Contains(Mouse.GetState().Position) && !HasMoved)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
