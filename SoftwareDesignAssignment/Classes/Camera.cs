using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsaaCAassessment2019.Classes
{
    public class Camera : GameComponent
    {
        #region Variables
        static Vector2 camPos = Vector2.Zero;
        static public Vector2 CamPos
        {
            get{ return camPos; }
            set { camPos = value; }
        }
        static public Vector2 WorldBound { get; private set; }
        static public Matrix CurrentCameraTranslation
        {
            get { return Matrix.CreateTranslation(new Vector3(-CamPos, 0)); }
        }
        static public Rectangle CamRect { get; private set; }
        static public Sprite FocusSprite { get; private set; }
        #endregion

        #region Constructors
        public Camera(Game game,Vector2 position, Vector2 worldBounds) : base(game)
        {
            game.Components.Add(this);
            CamPos = position;
            WorldBound = worldBounds;
            FocusSprite = null;
        }
        public Camera(Game game, Vector2 position, Vector2 worldBounds,Sprite spriteFocus) : base(game)
        {
            game.Components.Add(this);
            CamPos = position;
            WorldBound = worldBounds;
            FocusSprite = spriteFocus;
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            CamRect = new Rectangle((int)CamPos.X, (int)CamPos.Y, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Width);

            //Focus sprite allows camera to follow sprite and clamp around them in regards to world bounds. Focus sprite can be changed, see below.
            if (FocusSprite != null && FocusSprite.Visible == true)
            {
                FollowSprite(FocusSprite);
                FocusSprite.ChangePosition(Vector2.Clamp(FocusSprite.Position,
                                                         Vector2.Zero,
                                                         new Vector2(WorldBound.X - (FocusSprite.CollisionField.Width),
                                                                     WorldBound.Y - (FocusSprite.CollisionField.Height)) ));
            }

            base.Update(gameTime);
        }

        private void FollowSprite(Sprite sprite)
        {
            //Debug.WriteLine("Follow Start: " + CamPos.X + " " + CamPos.Y); 
            camPos = sprite.Position  - new Vector2(Game.GraphicsDevice.Viewport.Width / 2,
                                                    Game.GraphicsDevice.Viewport.Height / 2);
            camPos = Vector2.Clamp(camPos, Vector2.Zero, WorldBound - new Vector2(Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height));
            //Debug.WriteLine("Follow Finish: " + CamPos.X + " " + CamPos.Y);
        }

        /// <summary>
        /// Changes the focus of the camera to the desired sprite object.
        /// </summary>
        /// <param name="sprite">Any object that is a child of sprite.</param>
        static public void FocusOnSprite(Sprite sprite)
        {
            FocusSprite = sprite;
        }
    }
}
