using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public bool IsSelected { get; set; } = false;
        public bool HasMoved { get; private set; } = false;
        public bool IsDead { get; private set; } = false;
        public Element ElementalType { get; private set; }
        public Rectangle ClickBox { get; set; }
        public MapGrid grid;

        public Character(Game game,int health, int magicPoints,Element elementType, Texture2D texture, Vector2 userPosition, int frameCount, OriginType origin) : base(game,texture, userPosition, frameCount, origin)
        {
            grid = game.Services.GetService<MapGrid>();
            Health = health;
            MagicPoints = magicPoints;
            ClickBox = CollisionField;
            ElementalType = elementType;
            gridCell = new int[] {(int) userPosition.X / 64 , (int)userPosition.Y / 64 };
            MovementRange = 3;
            AttackRange = 1;
            Visible = true;
        }

        //Methods
        public void StartTurn()
        {
            HasMoved = false;
        }

        public void Move(MapGrid grid)
        {
            foreach (var tile in grid.tilesList)
            {
                if (tile.ClickCheck() && tile.IsWalkable)
                {
                    Debug.WriteLine("Move");
                    gridCell = tile.gridLocation;
                    Position = new Vector2(gridCell[0] * 64, gridCell[1] * 64);
                    grid.ResetWalkable();
                    //IsSelected = false;
                }
                
            }
        }


        //Overrides
        

        public override void Update(GameTime gameTime)
        {

           

            if(Health <= 0)
            {
                Visible = false;
                IsDead = true;
            }

            ClickCheck();

            if (IsSelected)
            {
                //IsSelected = !IsSelected;
                if (ClickCheck())
                {
                    for (int i = MovementRange; i > 0; i--)
                    {
                        grid.CheckMoves(gridCell, i);
                    }
                    grid.CheckAttack(gridCell, MovementRange + AttackRange);
                    Debug.WriteLine("Violent Yelling");
                }
                else
                    grid.ResetWalkable();

                Debug.WriteLine("Selected");
                Move(grid);
            }




            base.Update(gameTime);
        }

        public bool ClickCheck()
        {
            //if(InputEngine.IsMouseLeftClick() && ClickBox.Contains(Mouse.GetState().Position) && !HasMoved)
            if (InputEngine.IsMouseLeftClick() && ClickBox.Contains(Mouse.GetState().Position))
            {
                IsSelected = true;
                return true;
            }
            else
            {
                IsSelected = false;
                return false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if(Game1.gameState == GameState.Playing)
            base.Draw(gameTime);
        }
    }

    public class PlayerCharacter : Character
    {
        public PlayerCharacter(Game game,int health, int magicPoints, Element elementType, Texture2D texture, Vector2 userPosition, int frameCount, OriginType origin) : base(game,health, magicPoints, elementType, texture, userPosition, frameCount, origin)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
