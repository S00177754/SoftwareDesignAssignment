using DsaaCAassessment2019.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsaaCAassessment2019
{
    public abstract class GameObject : DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;

        //Constructor
        public GameObject(Game game) : base(game)
        {
            game.Components.Add(this);
            spriteBatch = game.Services.GetService<SpriteBatch>();
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
                base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {

            if (Enabled)
                base.Update(gameTime);
        }

        public virtual void Reset()
        {
            Visible = true;
        }
    }

    //#################### SPRITE CLASSES ###########################
    public enum SpriteOrigin { TopLeft, Center, TopRight, BottomLeft, BottomRight };

    public class Sprite : GameObject
    {
        #region Variables and Properties

        public Vector2 Position { get; protected set; }
        public Vector2 OriginalPosition { get; protected set; }
        public Texture2D Texture { get; private set; }
        public Rectangle CollisionField { get;  set; }


        public Vector2 Origin { get; private set; }
        public Vector2 WorldOrigin { get { return (Position + Origin); } }

        public float Layer { get; private set; }
        public float Rotation { get; private set; }
        public float Scale { get; private set; }

        public SpriteEffects Effect { get; private set; }
        public Color TextureColor { get; private set; } = Color.White;

        private int spriteWidth = 0;
        private int spriteHeight = 0;

        //Sprite Animation Variables
        public Rectangle SourceRectangle { get; private set; }
        private int numberOfFrames = 0;
        private int currentFrame = 0;
        private int millisecondsBetweenFrames = 100;
        private float timer = 0f;

        #endregion

        #region Constructors
        public Sprite(Game game, Texture2D texture, Vector2 position, SpriteOrigin origin) : this(game,texture,position,origin,texture.Width,texture.Height)
        {
        }
        public Sprite(Game game, Texture2D texture, Vector2 position, SpriteOrigin origin, int width, int height) : base(game)
        {
            Position = position;
            OriginalPosition = position;
            Texture = texture;

            spriteWidth = width;
            spriteHeight = height;

            CollisionField = new Rectangle(Position.ToPoint(), new Point(spriteWidth, spriteHeight));
            Rotation = 0;
            Scale = 1;
            Layer = 1;
            numberOfFrames = 1;
            Effect = SpriteEffects.None;
            Origin = SetOrigin(origin);

        }
        public Sprite(Game game, Texture2D texture, Vector2 position, SpriteOrigin origin, float scale, int frameCount) : base(game)
        {
            Position = position;
            OriginalPosition = position;
            Texture = texture;

            spriteWidth = Texture.Width;
            spriteHeight = Texture.Height;

            CollisionField = new Rectangle(Position.ToPoint(), new Point(spriteWidth, spriteHeight));
            Rotation = 0;
            Scale = scale;
            Layer = 1;
            numberOfFrames = frameCount;
            Effect = SpriteEffects.None;
            Origin = SetOrigin(origin);

        }

        #endregion

        public override void Draw(GameTime gameTime)
        {
            //Destination Rectangle in draw due to strange issue with origin when done in update.
            Rectangle destination = new Rectangle((int)CollisionField.X + (int)Origin.X, (int)CollisionField.Y + (int)Origin.Y, CollisionField.Width, CollisionField.Height);

            spriteBatch.Begin(SpriteSortMode.FrontToBack,null,null,null,null,null,Camera.CurrentCameraTranslation);
            spriteBatch.Draw(Texture, destination, null, TextureColor, Rotation, Origin, Effect, Layer);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            
            CollisionField = new Rectangle((int)(Position.X), (int)(Position.Y), spriteWidth, spriteHeight);
            base.Update(gameTime);
        }

        public override void Reset()
        {
            Position = OriginalPosition;
            base.Reset();
        }

        #region Setup Methods
        private Vector2 SetOrigin(SpriteOrigin origin)
        {
            switch (origin)
            {
                case SpriteOrigin.Center:
                    return new Vector2(spriteWidth / 2, spriteHeight / 2);

                case SpriteOrigin.TopLeft:
                    return Vector2.Zero;

                case SpriteOrigin.BottomLeft:
                    return new Vector2(0, spriteHeight);

                case SpriteOrigin.BottomRight:
                    return new Vector2(spriteWidth, spriteHeight);

                case SpriteOrigin.TopRight:
                    return new Vector2(spriteWidth, 0);

                default:
                    return Vector2.Zero;
            }
        }
        #endregion

        public void ChangePosition(Vector2 position)
        {
            Position = position;
        }
    }


}
