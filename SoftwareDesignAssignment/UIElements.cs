﻿using System;
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
    public abstract class UIElement : DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        protected SpriteFont spriteFont;

        protected Texture2D texture;
        protected Rectangle destinationRectangle;
        protected Color backgroundColor;


        public UIElement(Game game,Texture2D texture,Vector2 position,int boxWidth,int boxHeight,Color backgroundColor) : base(game)
        {
            game.Components.Add(this);
            spriteBatch = game.Services.GetService<SpriteBatch>();
            spriteFont = game.Services.GetService<SpriteFont>();

            this.texture = texture;
            this.destinationRectangle = new Rectangle((int)position.X, (int)position.Y, boxWidth, boxHeight);
            this.backgroundColor = backgroundColor;
            Visible = true;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if(Visible && texture != null)
            spriteBatch.Draw(texture, destinationRectangle,backgroundColor);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }

    public class UIButton : UIElement, IClickable
    {
        string message = "";
        Vector2 positionText;

        public UIButton(Game game,string message,Texture2D texture, Vector2 position, int boxWidth, int boxHeight, Color backgroundColor) : base(game, texture, position, boxWidth, boxHeight, backgroundColor)
        {
            this.message = message;
            this.destinationRectangle = new Rectangle((int)position.X, (int)position.Y, boxWidth, boxHeight);
            ClickBox = destinationRectangle;
            positionText = destinationRectangle.Center.ToVector2() - (spriteFont.MeasureString(message) / 2);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            if (Visible)
            {
                spriteBatch.DrawString(spriteFont, message,positionText, Color.Black);
            }
            spriteBatch.End();
        }

        //Interface clickable to allow button to function
        public Rectangle ClickBox { get; set; }

        public bool ClickCheck()
        {
            if(ClickBox.Contains(InputEngine.MousePosition) && InputEngine.IsMouseLeftClick())
            {
                //Debug.WriteLine("TRUE");
                return true;
            }
            else
            {
                //Debug.WriteLine("FALSE");
                return false;
            }
        }
    }

    public class UIStatBlock : UIElement
    {
        string message = "";
        Vector2 positionText;

        public UIStatBlock(Game game, Character character, Texture2D texture, Vector2 position, int boxWidth, int boxHeight, Color backgroundColor) : base(game, texture, position, boxWidth, boxHeight, backgroundColor)
        {
            message = $"HP:{character.Health} \nMP:{character.MagicPoints}";
            this.destinationRectangle = new Rectangle((int)position.X, (int)position.Y, boxWidth, boxHeight);
            positionText = destinationRectangle.Center.ToVector2() - (spriteFont.MeasureString(message) / 2);
        }

        public override void Draw(GameTime gameTime)
        {
                base.Draw(gameTime);
            spriteBatch.Begin();
            if (Visible)
            {
                spriteBatch.DrawString(spriteFont, message, positionText, Color.White);
            }
            spriteBatch.End();
        }
    }

    public class UIImage : UIElement
    {
        public UIImage(Game game, Texture2D texture, Vector2 position, int boxWidth, int boxHeight, Color backgroundColor) : base(game, texture, position, boxWidth, boxHeight, backgroundColor)
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

    public class UITextBlock : UIElement
    {
        public List<string> TextList { get; set; }
        private Color color;
        private bool BeginPrompt = false;

        public UITextBlock(Game game,List<string> textList, Vector2 position,bool beginPrompt) : base(game, null, position, 0, 0, Color.White)
        {
            TextList = textList;
            color = Color.White;
            BeginPrompt = beginPrompt;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            for (int i = 0; i < TextList.Count; i++)
            {
                if (i == 2 && BeginPrompt)
                    color = Color.CornflowerBlue;
                else if (i == 4 && BeginPrompt)
                    color = Color.Red;
                else
                    color = Color.White;

                sb.DrawString(Game.Services.GetService<SpriteFont>(), TextList[i], destinationRectangle.Location.ToVector2() + new Vector2(0, 20 * i), color);
            }
            sb.End();

            base.Draw(gameTime);
        }
    }
}
