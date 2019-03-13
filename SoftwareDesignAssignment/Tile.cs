using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignAssignment
{
    public interface IClickable
    {
        //Implements seperate rectangle so that the object does not need to be fully clickable, 
        //that a specified region can be chosen
        Rectangle ClickBox { get; set; }

        bool ClickCheck();
    }

    public class Tile :DrawableGameComponent, IClickable
    {
        //Variables
        Color tileColor;
        Texture2D tileTexture;
        SpriteBatch spriteBatch;
        Vector2 position;
        Rectangle destination { get { return new Rectangle((int)position.X, (int)position.Y, 64, 64); } }
        public bool IsPassable { get; private set; }
        public bool displayMap = false;
        public int[] gridLocation;

        //Constructor
        public Tile(Game game, Texture2D tileTexture, Vector2 position, bool isPassable):base(game)
        {
            game.Components.Add(this);
            spriteBatch = game.Services.GetService<SpriteBatch>();
            this.tileTexture = tileTexture;
            this.position = position;
            IsPassable = isPassable;
            tileColor = Color.White;
            ClickBox = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }
        public Tile(Game game, Texture2D tileTexture, Vector2 position, bool isPassable,int[] grid) : this(game,tileTexture,position,isPassable)
        {
            gridLocation = grid;
        }

        #region IClickable
        //IClickable Interface to allow tile to be clicked
        public Rectangle ClickBox { get; set; }

        public bool ClickCheck()
        {
            if (ClickBox.Contains(Mouse.GetState().Position) && InputEngine.IsMouseLeftClick())
                return true;
            return false;
        }
        #endregion

        //Methods
        public void PassableColor()
        {
            if (IsPassable)
            {
                tileColor = Color.Blue;
            }
            else if (!IsPassable)
            {
                tileColor = Color.Red;
            }

            tileColor.A = 127;
        }

        public void ResetColor()
        {
            tileColor = Color.White;
            tileColor.A = 255;
        }

        //Overrides from drawable game component
        public override void Draw(GameTime gameTime)
        {
            if (displayMap)
            {
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);
                spriteBatch.Draw(tileTexture, destination, Color.White);
                spriteBatch.Draw(tileTexture, destination, tileColor);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (ClickCheck())
            {
                
                if(tileColor != Color.White)
                {
                    //ResetColor();
                }
                else{
                    //PassableColor();
                }
            }
            base.Update(gameTime);
        }
    }
}
