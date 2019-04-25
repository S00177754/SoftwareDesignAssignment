using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace DsaaCAassessment2019.Classes
{
    public class Canvas : DrawableGameComponent
    {
        //Collections
        public Dictionary<string, CanvasElement> canvasElements;

        //Constructor
        public Canvas(Game game) : base(game)
        {
            game = Game;
            canvasElements = new Dictionary<string, CanvasElement>();
        }

        //Overrides
        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }

        #region Canvas Manipulation
        public void AddCanvasElement(string key, CanvasElement element)
        {
            canvasElements.Add(key, element);
        }

        public void RemoveCanvasElement(string key, CanvasElement element)
        {
            canvasElements.Remove(key);
        }

        public CanvasElement GetCanvasElement(string key)
        {
            return canvasElements[key];
        }
        #endregion
    }

    public abstract class CanvasElement : DrawableGameComponent
    {
        //Variables
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle CollisionField { get; set; }
        public Color TextColor { get; set; }
        public string Text { get; set; }

        private SpriteBatch sb;
        private SpriteFont sp;

        //Constructors
        public CanvasElement(Game game) : base(game)
        {
            game.Components.Add(this);
            sb = game.Services.GetService<SpriteBatch>();
            sp = game.Services.GetService<SpriteFont>();
        }
        public CanvasElement(Game game,Texture2D texture,Rectangle destination,Color textColor,string text) : this(game)
        {
            Texture = texture;
            CollisionField = destination;
            TextColor = textColor;
            Text = text;
            Position = CollisionField.Center.ToVector2() - (sp.MeasureString(Text)/2);
        }

        //Overrides
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            if(Texture != null)
            sb.Draw(Texture, CollisionField, Color.CadetBlue);
            sb.DrawString(sp, Text, Position, TextColor);
            sb.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }

    public class MenuButton : CanvasElement
    {
        //Variables
        Action action;

        //Constructors
        public MenuButton(Game game,Texture2D texture, Rectangle destination, Color textColor, string text) : base(game,texture,destination,textColor,text)
        {
        }
        public MenuButton(Game game,Action action, Texture2D texture, Rectangle destination, Color textColor, string text) : base(game, texture, destination, textColor, text)
        {
            this.action = action;
        }
        
        //Overrides
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if(CollisionField.Contains(InputEngine.MousePosition) && InputEngine.IsMouseLeftClick())
            {
                Debug.WriteLine("Button Clicked: " + action.ToString());
                action();
            }

            base.Update(gameTime);
        }

        //Methods
        public void SetAction(Action action)
        {
            this.action = action;
        }
    }

    public class HealthBar : CanvasElement
    {
        //Variables and Objects
        //public Player SourcePlayer { get; private set; }
        private Texture2D TXhealthBar;

        #region Constructors
        public HealthBar(Game game,Texture2D background, Rectangle destination, Color textColor, string text) : base(game, background, destination, textColor, text)
        {
           // SourcePlayer = player;
            TXhealthBar = new Texture2D(game.GraphicsDevice, 1, 1);
            TXhealthBar.SetData(new[] { Color.White });
        }
        #endregion

        #region Overrides
        public override void Draw(GameTime gameTime)
        {
            
            DrawHealthBar(CollisionField.Location.ToVector2());
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        #endregion

        //Draw method for health bar
        public void DrawHealthBar(Vector2 position)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            //Rectangle RectHealth = new Rectangle((int) position.X, (int) position.Y + 20,(int)(100/SourcePlayer.MaxHealth) * SourcePlayer.Health, 20);
            sb.Begin();
            #region Health Bar Logic
            //if (SourcePlayer.Health > (SourcePlayer.MaxHealth * 0.6))
            //    sb.Draw(TXhealthBar, RectHealth, Color.Green);

            //else if (SourcePlayer.Health > (SourcePlayer.MaxHealth * 0.3) && SourcePlayer.Health <= (SourcePlayer.MaxHealth * 0.6))
            //    sb.Draw(TXhealthBar, RectHealth, Color.Orange);

            //else if (SourcePlayer.Health > 0 && SourcePlayer.Health <= (SourcePlayer.MaxHealth * 0.3))
            //    sb.Draw(TXhealthBar, RectHealth, Color.Red);
            #endregion
            sb.End();
        }
    }

    public class CanvasImage : CanvasElement
    {
        #region Constructors
        public CanvasImage(Game game) : base(game)
        {
        }

        public CanvasImage(Game game, Texture2D texture, Rectangle destination) : base(game, texture, destination, Color.White, "")
        {
        }
        #endregion

        #region Overrides
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        #endregion
    }

    public class CanvasMessageBox<T> : CanvasElement
    {
        // Variables and Collections
        private enum MessageType { TextBlock, Scoreboard };
        private SpriteFont displayFont;
        public string Message { get; set; }
        private LinkedList<T> textList;
        private MessageType type;

        #region Constructors
        public CanvasMessageBox(Game game, Rectangle destination, Color textColor, LinkedList<T> textList) : base(game, null, destination, textColor,"")
        {
            displayFont = game.Services.GetService<SpriteFont>();
            this.textList = textList;
            type = MessageType.TextBlock;
        }
        public CanvasMessageBox(Game game, Rectangle destination, Color textColor, bool Scoreboard) : base(game, null, destination, textColor, "")
        {
            displayFont = game.Services.GetService<SpriteFont>();
            type = MessageType.Scoreboard;
        }
        #endregion

        #region Overrides
        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                switch (type)
                {
                    case MessageType.Scoreboard:
                        GenerateScoreBoard();
                        break;

                    case MessageType.TextBlock:
                        GenerateTextBox();
                        break;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            if (Message != null)
            {
                sb.DrawString(displayFont, Message, CollisionField.Location.ToVector2(), TextColor);
            }
            sb.End();
            base.Draw(gameTime);
        }
        #endregion

        #region Generation Methods
        public void GenerateTextBox()
        {
            int tempHeight = 0;
            Message = "";
            foreach (var textLine in textList)
            {
                if (tempHeight + displayFont.MeasureString(textLine.ToString()).Y < CollisionField.Height)
                {
                    Message += textLine.ToString();
                    Message += "\n";
                    tempHeight += (int)displayFont.MeasureString(textLine.ToString()).Y * 2;
                }
                else
                {
                    break;
                }
            }
        }

        public void GenerateScoreBoard()
        {
            int tempHeight = 0;
            Message = "";

            int counter = 1;
            foreach (var playerScore in Scoreboard.ScoreList)
            {
                
                if (tempHeight + displayFont.MeasureString(playerScore.ToString()).Y < CollisionField.Height)
                {
                    Message += ($"{counter}. " + playerScore.ToString());
                    Message += "\n";
                    tempHeight += (int)displayFont.MeasureString(playerScore.ToString()).Y * 2;
                    counter++;
                }
                else
                {
                    break;
                }
            }
        }
        #endregion
    }
}


