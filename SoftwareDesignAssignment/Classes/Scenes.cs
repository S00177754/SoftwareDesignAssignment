using DsaaCAassessment2019.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SoftwareDesignAssignment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SoftwareDesignAssignment.Sprite;

namespace DsaaCAassessment2019
{
    public enum SceneType { Fixed, Stretch, Relative }

    public class Scene : DrawableGameComponent
    {
        #region Variables and Collections
            private Game myGame;
            private SpriteBatch spriteBatch;

            private Texture2D Background { get; set; }
            public Dictionary<string,GameObject> sceneElements;
            public Dictionary<string,Canvas> canvasList;
            public bool Active { get; set; }
            public SceneType Type { get; set; }
        #endregion

        #region Constructor
        public Scene(Game game, Texture2D background) : base(game)
        {
            game.Components.Add(this);
            myGame = game;

            spriteBatch = game.Services.GetService<SpriteBatch>();
            sceneElements = new Dictionary<string,GameObject>();
            canvasList = new Dictionary<string, Canvas>();
            Active = false;
            ActivateAll();
            Background = background;
        }
        public Scene(Game game, Texture2D background, SceneType type) : base(game)
        {
            game.Components.Add(this);
            myGame = game;

            spriteBatch = game.Services.GetService<SpriteBatch>();
            sceneElements = new Dictionary<string, GameObject>();
            canvasList = new Dictionary<string, Canvas>();
            Active = false;
            ActivateAll();
            Background = background;
            Type = type;
        }
        #endregion

        #region  Overrides
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Camera.CurrentCameraTranslation);

            switch (Type)
            {
                case SceneType.Fixed:
                    spriteBatch.Draw(Background, myGame.GraphicsDevice.Viewport.Bounds, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    break;

                case SceneType.Relative:
                    spriteBatch.Draw(Background, new Rectangle(Camera.CamRect.X, Camera.CamRect.Y, (int)Game.GraphicsDevice.Viewport.Width, (int)Game.GraphicsDevice.Viewport.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);

                    break;

                case SceneType.Stretch:
                    spriteBatch.Draw(Background,new Rectangle(0,0,(int)Camera.WorldBound.X,(int)Camera.WorldBound.Y), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {

                base.Update(gameTime);
        }
        #endregion

        public void ActivateAll()
        {
            foreach (var canvas in canvasList)
            {
                canvas.Value.Enabled = Active;
                canvas.Value.Visible = Active;
            }

            foreach (var gameObject in sceneElements.Values)
            {
                gameObject.Enabled = Active;
                gameObject.Visible = Active;
            }
        }

        #region Canvas Access Methods
        public void AddCanvas(string key,Canvas canvas)
        {
            canvasList.Add(key, canvas);
        }

        public void RemoveCanvas(string key)
        {
            canvasList.Remove(key);
        }

        public Canvas GetCanvas(string key)
        {
            return canvasList[key];
        }


        public void AddCanvasElement(string canvasKey,string elementKey,CanvasElement element)
        {
            canvasList[canvasKey].canvasElements.Add(elementKey, element);
        }

        public void RemoveCanvasElement(string canvasKey, string elementKey)
        {
            canvasList[canvasKey].canvasElements.Remove(elementKey);
        }

        public CanvasElement GetCanvasElement(string canvasKey, string elementKey)
        {
            return canvasList[canvasKey].canvasElements[elementKey];
        }
        #endregion

        #region Access GameObject Methods
        public GameObject GetGameObject(string objectName)
        {
            return sceneElements[objectName];
        }

        public void AddGameObject(string objectName,GameObject gameObject)
        {
            sceneElements.Add(objectName, gameObject);
        }

        public void AddGameObjectList(List<string> objectNames, List<GameObject> gameObjects)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                sceneElements.Add(objectNames[i], gameObjects[i]);
            }
        }
        

        public void RemoveGameObject(string objectName, GameObject gameObject)
        {
            sceneElements.Remove(objectName);
        }
        #endregion
    }

    public class SceneManager:GameComponent
    {
        public Dictionary<string, Scene> SceneList { get; private set; }
        public Stack<Scene> ActiveScenes { get; set; }
        public Scene _activeScene;

        public SceneManager(Game game):this(game, new Dictionary<string, Scene>())
        {

        }
        public SceneManager(Game game,Dictionary<string, Scene> sceneList):base(game)
        {
            game.Components.Add(this);
            SceneList = sceneList;
            ActiveScenes = new Stack<Scene>();
        }

        private bool gameCompleted = false;

        public override void Update(GameTime gameTime)
        {
            if (Classes.InputEngine.IsKeyPressed(Keys.Escape))
            {
                GoOutOfScene();
            }

            if (_activeScene.Equals(SceneList["LevelOne"]))
            {
                LevelOneLogic();                  
            }
            
            
            
            base.Update(gameTime);
        }

        #region Scene Manipulation
        public void AddScene(string key,Scene scene)
        {
            SceneList.Add(key, scene);
        }

        public void ChangeScene(string key)
        {
            foreach (var scene in SceneList.Values)
            {
                scene.Visible = false;
                foreach (var canvas in scene.canvasList.Values)
                {
                    foreach (var element in canvas.canvasElements.Values)
                    {
                        element.Enabled = false;
                        element.Visible = false;
                    }
                    canvas.Enabled = false;
                    canvas.Visible = false;
                }

                foreach (var gameObject in scene.sceneElements.Values)
                {
                    gameObject.Enabled = false;
                    gameObject.Visible = false;
                }
            }

            _activeScene = SceneList[key];
            _activeScene.Visible = true;
            _activeScene.Active = true;
            _activeScene.ActivateAll();

            foreach (var canvas in _activeScene.canvasList.Values)
            {
                foreach (var element in canvas.canvasElements.Values)
                {
                    element.Enabled = true;
                    element.Visible = true;
                }

                canvas.Enabled = true;
                canvas.Visible = true;
            }

            foreach (var gameObject in _activeScene.sceneElements.Values)
            {
                gameObject.Enabled = true;
                gameObject.Visible = true;
            }
        }

        public void ChangeScene(Scene sceneSwap)
        {
            foreach (var scene in SceneList.Values)
            {
                scene.Visible = false;
                foreach (var canvas in scene.canvasList.Values)
                {
                    foreach (var element in canvas.canvasElements.Values)
                    {
                        element.Enabled = false;
                        element.Visible = false;
                    }

                    canvas.Enabled = false;
                    canvas.Visible = false;

                    foreach (var gameObject in scene.sceneElements.Values)
                    {
                        gameObject.Enabled = false;
                        gameObject.Visible = false;
                    }
                }

            }

            _activeScene = sceneSwap;
            _activeScene.Visible = true;
            _activeScene.Active = true;
            _activeScene.ActivateAll();

            foreach (var canvas in _activeScene.canvasList.Values)
            {
                foreach (var element in canvas.canvasElements.Values)
                {
                    element.Enabled = true;
                    element.Visible = true;
                }

                canvas.Enabled = true;
                canvas.Visible = true;

            }
            foreach (var gameObject in _activeScene.sceneElements.Values)
            {
                gameObject.Enabled = true;
                gameObject.Visible = true;
            }
        }

        public void GoIntoScene(string key)
        {   
            
            ActiveScenes.Push(_activeScene);
            ChangeScene(key);
        }

        public void GoOutOfScene()
        {
            Scene Temp = ActiveScenes.Pop();
            ActiveScenes.Push(_activeScene);
            ChangeScene(Temp);
        }

        public void ResumeGameScene()
        {
            ActiveScenes.Pop();
            Scene Temp = SceneList["LevelOne"];
            ActiveScenes.Push(_activeScene);
            ChangeScene(Temp);
        }

        public Scene GetScene(string key)
        {
            return SceneList[key];
        }
        #endregion


        #region ChangeLevels
        public void MainMenu()
        {
            GoIntoScene("MainMenu");
        }

        public void LevelOne()
        {
            GoIntoScene("LevelOne");
        }

        public void Scoreboards()
        {
            GoIntoScene("Scoreboard");
        }
        #endregion

        #region Level Logic
        public void LevelOneLogic()
        {
            
        }
        #endregion


        #region Level Initialisation

        public void Setup()
        {
            AddScene("MainMenu", new Scene(Game, Game.Content.Load<Texture2D>(@"Textures\Background"), SceneType.Relative));
            GetScene("MainMenu").AddCanvas("Menu", new Canvas(Game));
            MainMenuInitialise();


            AddScene("LevelOne", new Scene(Game, Game.Content.Load<Texture2D>(@"Textures\space"),SceneType.Stretch));
            GetScene("LevelOne").AddCanvas("GUI", new Canvas(Game));
            LevelOneInitialise();
            
        }

        public void MainMenuInitialise()
        {
            GetScene("MainMenu").GetCanvas("Menu")
                        .AddCanvasElement("Logo",new CanvasImage(Game, Game.Content.Load<Texture2D>(@"Textures\starBattles"), new Rectangle((int) Game.GraphicsDevice.Viewport.Bounds.Center.ToVector2().X - 200, (int) Game.GraphicsDevice.Viewport.Bounds.Center.ToVector2().Y - 145, 400, 100)));

            GetScene("MainMenu").GetCanvas("Menu")
                        .AddCanvasElement("Play", new MenuButton(Game,new Action(NewGame),Game.Content.Load<Texture2D>(@"Textures\WoodPanel"),new Rectangle((int) Game.GraphicsDevice.Viewport.Bounds.Center.ToVector2().X - 100, (int) Game.GraphicsDevice.Viewport.Bounds.Center.ToVector2().Y - 35, 200,50),Color.White,"Play"));

            GetScene("MainMenu").GetCanvas("Menu")
                        .AddCanvasElement("Exit", new MenuButton(Game, new Action(Game.Exit), Game.Content.Load<Texture2D>(@"Textures\WoodPanel"), new Rectangle((int) Game.GraphicsDevice.Viewport.Bounds.Center.ToVector2().X - 100, (int) Game.GraphicsDevice.Viewport.Bounds.Center.ToVector2().Y + 85, 200, 50), Color.White, "Exit"));
        }


        public void LevelOneInitialise()
        {
            GetScene("LevelOne").AddGameObject("BattleController",
        new BattleController(Game, new Party[] { new Party(Game,"PlayerOne", new List<Character>()
            {
                new PlayerCharacter(Game, 34, 77,1, new Element(SIGN.Spock), Game.Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(0,0), 1, OriginType.TopLeft),
                new PlayerCharacter(Game,63, 77,1, new Element(SIGN.Spock), Game.Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(64,0), 1, OriginType.TopLeft),
                new PlayerCharacter(Game,25, 77,1, new Element(SIGN.Spock), Game.Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(128,0), 1, OriginType.TopLeft),
                new PlayerCharacter(Game,11, 77,1, new Element(SIGN.Spock), Game.Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(0,64), 1, OriginType.TopLeft)
            }),
            new Party(Game,"PlayerTwo", new List<Character>()
            {
                new PlayerCharacter(Game, 14, 77,2, new Element(SIGN.Spock), Game.Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(384,0), 1, OriginType.TopLeft),
                new PlayerCharacter(Game,32, 77,2, new Element(SIGN.Spock), Game.Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(320,0), 1, OriginType.TopLeft),
                new PlayerCharacter(Game,40, 77,2, new Element(SIGN.Spock), Game.Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(320,64), 1, OriginType.TopLeft),
                new PlayerCharacter(Game,34, 77,2, new Element(SIGN.Spock), Game.Content.Load<Texture2D>(@"Textures\Characters\testCharacterSprite"), new Vector2(320,128), 1, OriginType.TopLeft)
            })
                }));
        }

        #endregion
        
        public void ResetLevelOne()
        {
            //SceneList["LevelOne"] = new Scene(Game, Game.Content.Load<Texture2D>(@"Textures\space"), SceneType.Stretch);
            //SceneList["LevelOne"].AddCanvas("GUI", new Canvas(Game));
            //LevelOneInitialise();
            //Camera.FocusOnSprite(SceneList["LevelOne"].GetGameObject("Player") as Player);
        }

        public void NewGame()
        {
            gameCompleted = false;
            ResetLevelOne();
            LevelOne();
        }
    }
}
