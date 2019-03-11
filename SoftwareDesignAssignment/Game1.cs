using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace SoftwareDesignAssignment
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MapGrid mapGrid;
        int[,] tileMap;
        List<Texture2D> textureList;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            new InputEngine(this);

            tileMap = new int[,]
            {
                {0,1,2,3,4,5,6,4,4,5,6,4,1 },
                {0,1,2,3,3,5,6,4,4,5,6,4,1 },
                {1,1,2,2,3,5,6,4,4,5,6,4 ,1},
                {1,1,1,3,3,4,4,4,4,5,6,4,1 },
                {1,1,2,2,3,5,6,4,4,5,6,4,1 },
                {1,1,1,3,3,4,4,4,4,5,6,4,1 },
                {1,1,1,3,3,4,4,4,4,5,6,4,1 },
                {1,1,1,3,3,4,4,4,4,5,6,4,1 },
            };
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(spriteBatch);

            // TODO: use this.Content to load your game content here
            textureList = new List<Texture2D>()
            {
                Content.Load<Texture2D>(@"Textures\Tiles\dirt"),
                Content.Load<Texture2D>(@"Textures\Tiles\grass"),
                Content.Load<Texture2D>(@"Textures\Tiles\ground"),
                Content.Load<Texture2D>(@"Textures\Tiles\mud"),
                Content.Load<Texture2D>(@"Textures\Tiles\road"),
                Content.Load<Texture2D>(@"Textures\Tiles\rock"),
                Content.Load<Texture2D>(@"Textures\Tiles\wood")
            };
            mapGrid = new MapGrid(this, 64, 64, tileMap,textureList);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
