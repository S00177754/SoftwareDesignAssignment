using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace SoftwareDesignAssignment
{

    public enum GameState { StartScreen,Playing,Paused};
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static GameState gameState;
        public List<UIElement> StartUI;

        #region Map Objects
        MapGrid mapGrid;
        int[,] tileMap;
        List<Texture2D> textureList;
        #endregion

        //Party playerOneParty;
        //Party playerTwoParty;
        BattleController battleController;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            new InputEngine(this);
            
            gameState = GameState.StartScreen;
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
            this.Services.AddService(Content.Load<SpriteFont>(@"Fonts\spriteFont"));
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

            //testCharacter = new PlayerCharacter(this,34, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), Vector2.Zero, 1, Sprite.OriginType.TopLeft);
            //testCharacter.spriteDepth = 1;
            battleController = new BattleController(new Party[] { new Party(this,"PlayerTwo", new List<Character>()
            {
                new PlayerCharacter(this, 34, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(384,0), 1, Sprite.OriginType.TopLeft),
                new PlayerCharacter(this,63, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(320,0), 1, Sprite.OriginType.TopLeft),
                new PlayerCharacter(this,25, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(320,64), 1, Sprite.OriginType.TopLeft),
                new PlayerCharacter(this,11, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(320,128), 1, Sprite.OriginType.TopLeft)
            }),
            new Party(this,"PlayerTwo", new List<Character>()
            {
                new PlayerCharacter(this, 14, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(384,0), 1, Sprite.OriginType.TopLeft),
                new PlayerCharacter(this,32, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(320,0), 1, Sprite.OriginType.TopLeft),
                new PlayerCharacter(this,40, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(320,64), 1, Sprite.OriginType.TopLeft),
                new PlayerCharacter(this,34, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(320,128), 1, Sprite.OriginType.TopLeft)
            })
                });

            //playerOneParty = new Party(this,"PlayerOne", new List<Character>()
            //{
            //    new PlayerCharacter(this, 34, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), Vector2.Zero, 1, Sprite.OriginType.TopLeft),
            //    new PlayerCharacter(this,34, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(64,0), 1, Sprite.OriginType.TopLeft),
            //    new PlayerCharacter(this,34, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(64,64), 1, Sprite.OriginType.TopLeft),
            //    new PlayerCharacter(this,34, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(0,64), 1, Sprite.OriginType.TopLeft)
            //});
            //playerTwoParty = new Party(this,"PlayerTwo", new List<Character>()
            //{
            //    new PlayerCharacter(this, 34, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(384,0), 1, Sprite.OriginType.TopLeft),
            //    new PlayerCharacter(this,34, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(320,0), 1, Sprite.OriginType.TopLeft),
            //    new PlayerCharacter(this,34, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(320,64), 1, Sprite.OriginType.TopLeft),
            //    new PlayerCharacter(this,34, 77, new Element(SIGN.Spock), Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(320,128), 1, Sprite.OriginType.TopLeft)
            //});

            StartUI = new List<UIElement>()
            {
                new UIButton(this,"Start Game",Content.Load<Texture2D>(@"Textures\WhiteSquare"),(GraphicsDevice.Viewport.Bounds.Center.ToVector2() - new Vector2(100,100)),200,100,Color.Orange),
                new UIButton(this,"Exit Game",Content.Load<Texture2D>(@"Textures\WhiteSquare"),(GraphicsDevice.Viewport.Bounds.Center.ToVector2() - new Vector2(100,-100)),200,100,Color.Orange)
            };

            gameState = GameState.StartScreen;
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.E))
                Exit();

                switch (gameState)
                {
                    case GameState.StartScreen:
                    ((UIButton)StartUI[0]).Visible = true;
                    ((UIButton)StartUI[1]).Visible = true;
                    mapGrid.Display(false);

                    if (((UIButton)StartUI[0]).ClickCheck() )
                    {
                            gameState = GameState.Playing;
                        battleController.NextTeam();
                    }
                       
                    if (((UIButton)StartUI[1]).ClickCheck())
                    {
                        Exit();
                    }
                    break;

                case GameState.Playing:

                    ((UIButton)StartUI[0]).Visible = false;
                    ((UIButton)StartUI[1]).Visible = false;
                    mapGrid.Display(true);
                    battleController.Update();
                    break;

                case GameState.Paused:
                    break;
                }
            

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
