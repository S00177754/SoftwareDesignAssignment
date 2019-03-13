using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignAssignment
{
    public class Sprite: DrawableGameComponent
    {
        //Visbility Variable
        private SpriteBatch spriteBatch;

        //Sprite Texture Variables
        protected Texture2D spriteTexture;
        public Texture2D SpriteTexture
        {
            get { return spriteTexture; }
            set { spriteTexture = value; }
        }
        public int spriteWidth = 0;
        public int spriteHeight = 0;
        private Rectangle sourceRectangle;
        public Rectangle SourceRectangle
        {
            get { return sourceRectangle; }
            set { sourceRectangle = value; }
        }
        public Rectangle CollisionField { get; set; }
        protected float scale = 1.0f;

        //Sprite Position Variables
        public Vector2 previousPosition;
        public Vector2 Position { get; set; }
        public Vector2 SpriteCenter { get; set; }
        public Vector2 Origin;
        public enum OriginType { TopLeft, Center, TopRight, BottomLeft, BottomRight };
        public Vector2 WorldOrigin { get { return (Position + Origin); } }
        protected float angleOfRotation;
        public int spriteDepth = 0;
        

        //Sprite Animation Variables
        private int numberOfFrames = 0;
        private int currentFrame = 0;
        private int millisecondsBetweenFrames = 100;
        private float timer = 0f;

        //Constructor 
        /// <param name="texture">Texture2D for this sprite. Texture strips are usable.</param>
        /// <param name="userPosition">Starting position of this player.</param>
        /// <param name="frameCount">Amount of frames in sprite texture strip.</param>
        /// <param name="origin">Rotation point of player.</param>
        public Sprite(Game game,Texture2D texture, Vector2 userPosition, int frameCount, OriginType origin):base(game)
        {
            //Set variables
            game.Components.Add(this);
            spriteBatch = game.Services.GetService<SpriteBatch>();
            spriteTexture = texture;
            Position = userPosition;
            numberOfFrames = frameCount;
            Visible = true;

            //Sprite Texture Variable Calculations
            spriteHeight = spriteTexture.Height;
            spriteWidth = spriteTexture.Width / frameCount;
            SpriteCenter = new Vector2(spriteWidth / 2, spriteHeight / 2);
            CollisionField = new Rectangle(Position.ToPoint(), new Point(spriteWidth, spriteHeight));
            switch (origin)
            {
                case OriginType.Center:
                    Origin = new Vector2(spriteWidth / 2, spriteHeight / 2);
                    break;

                case OriginType.TopLeft:
                    Origin = Vector2.Zero;
                    break;

                case OriginType.BottomLeft:
                    Origin = new Vector2(0, spriteHeight);
                    break;

                case OriginType.BottomRight:
                    Origin = new Vector2(spriteWidth, spriteHeight);
                    break;

                case OriginType.TopRight:
                    Origin = new Vector2(spriteWidth, 0);
                    break;
            }
            angleOfRotation = 0;
        }

        //Overridable Methods
        public override void Update(GameTime gameTime)
        {
            //Animation Logic
            timer += (float)gameTime.ElapsedGameTime.Milliseconds;
            if (timer > millisecondsBetweenFrames)
            {
                currentFrame++;
                if (currentFrame > (numberOfFrames - 1))
                {
                    currentFrame = 0;
                }
                timer = 0f;

            }
            sourceRectangle = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            CollisionField = new Rectangle(Position.ToPoint(), new Point(spriteWidth, spriteHeight));

            base.Update(gameTime);

        }

        //Draw Method
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (Visible)
                spriteBatch.Draw(spriteTexture, new Rectangle((int)Position.X, (int)Position.Y, 64, 64),SourceRectangle, Color.White, angleOfRotation,Origin,SpriteEffects.None,spriteDepth);

            //spriteBatch.Draw(spriteTexture,
            //            Position, sourceRectangle,
            //            Color.White, angleOfRotation, Origin,
            //            scale, SpriteEffects.None, spriteDepth);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        //Methods
        /// <summary>
        /// Checks for collision against passed in sprite.
        /// </summary>
        /// <param name="other">Sprite object being checked against.</param>
        /// <returns>Returns true if collision is true.</returns>
        public bool CollisionDetect(Sprite other)
        {
            Rectangle myBounds = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.spriteWidth, this.spriteHeight);
            Rectangle otherBounds = new Rectangle((int)other.Position.X, (int)other.Position.Y, other.spriteWidth, other.spriteHeight);
            if (myBounds.Intersects(otherBounds))
                return true;
            return false;
        }



        public void ToggleVisibility()
        {
            Visible = !Visible;
        }

        /// <summary>
        /// Method to return desired float rotation and turn to angle.
        /// </summary>
        /// <param name="position">This sprites position.</param>
        /// <param name="faceThis">Position of sprite that you want to point towards.</param>
        /// <param name="currentAngle">The current angle of this sprite.</param>
        /// <param name="turnSpeed">Speed at which the sprite will rotate to desired angle.</param>
        /// <returns></returns>
        protected static float TurnToFace(Vector2 position, Vector2 faceThis, float currentAngle, float turnSpeed)
        {
            // The difference in the two points is 
            float x = faceThis.X - position.X;
            float y = faceThis.Y - position.Y;
            // ArcTan calculates the angle of rotation 
            // relative to a point (the gun turret position)
            // in the positive x plane and 
            float desiredAngle = (float)Math.Atan2(y, x);
            float difference = WrapAngle(desiredAngle - currentAngle);
            difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);
            return WrapAngle(currentAngle + difference);
        }

        /// <summary>
        /// Method to return desired float rotation.
        /// </summary>
        /// <param name="position">This sprites position.</param>
        /// <param name="faceThis">Position of sprite you want to face.</param>
        /// <returns></returns>
        protected static float DesiredAngle(Vector2 position, Vector2 faceThis)
        {
            // The difference in the two points is 
            float x = faceThis.X - position.X;
            float y = faceThis.Y - position.Y;
            // ArcTan calculates the angle of rotation 
            // relative to a point (the gun turret position)
            // in the positive x plane and 
            float desiredAngle = (float)Math.Atan2(y, x);
            return desiredAngle;
        }

        /// <summary>
        /// Returns the angle expressed in radians between -Pi and Pi.
        /// Angle is always positive
        /// </summary>
        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }


    }
}
