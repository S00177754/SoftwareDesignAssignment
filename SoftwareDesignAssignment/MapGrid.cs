using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignAssignment
{
    public enum TileType { Dirt, Grass, Ground, Mud, Road, Rock, Wood};

    public class MapGrid
    {
        //Variables
        public Game MyGame { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        public int[,] TileMap { get; private set; }
        public int[] GridPosition { get; set; }
        public Vector2 WorldBounds { get; private set; }
        public SpriteBatch spriteBatch { get; private set; }
        List<Tile> tilesList;
        List<Texture2D> textures;

        //Constructor
        public MapGrid(Game game,int tile_width, int tile_height, int[,] tilemap, List<Texture2D> textureList)
        {
            spriteBatch = game.Services.GetService<SpriteBatch>();
            MyGame = game;
            TileWidth = tile_width;
            TileHeight = tile_height;
            TileMap = tilemap;
            textures = textureList;
            tilesList = new List<Tile>();

            SetMap();
        }

        //Methods
        public void SetMap()
        {
            for (int x = 0; x < TileMap.GetLength(1); x++)
            {
                for (int y = 0; y < TileMap.GetLength(0); y++)
                {
                    int textureIndex = TileMap[y, x];
                    Texture2D texture = textures[textureIndex];
                    bool passable = true;

                    switch (textureIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            passable = true;
                            break;

                        case 5:
                        case 6:
                        case 7:
                            passable = false;
                            break;
                    }

                    tilesList.Add(new Tile(MyGame, texture, new Vector2(x * 64, y * 64), 
                        passable,new int[] {x,y}));
                }
            }
        }

        public void CheckMoves(int[] playerPos, int range)
        {
            int[] moveThis;
            int x = playerPos[0];
            int y = playerPos[1];
            int[] moveFromHere = tilesList.Find(current => current.gridLocation[0] == x && current.gridLocation[1] == y).gridLocation;
            int deltaX = range, deltaY = 0;

            //Check Movementrange
            //x--, y++
            for (int i = 0; i < range; i++)
            {
                moveThis = new int[] { x, y };
                tilesList.Find(current => current.gridLocation[0] == (x + deltaX)
                                && current.gridLocation[1] == (y + deltaY)).IsWalkable = true;
                deltaX--;
                deltaY++;
            }

            //x--, y--
            deltaX = range;
            deltaY = range;
            for (int i = 0; i < range; i++)
            {
                moveThis = new int[] { x, y };
                tilesList.Find(current => current.gridLocation[0] == (x + deltaX)
                                && current.gridLocation[1] == (y + deltaY)).IsWalkable = true;
                deltaX--;
                deltaY--;
            }

            //x++, y++
            deltaX = 0;
            deltaY = 0;
            for (int i = 0; i < range; i++)
            {
                moveThis = new int[] { x, y };
                tilesList.Find(current => current.gridLocation[0] == (x + deltaX)
                                && current.gridLocation[1] == (y + deltaY)).IsWalkable = true;
                deltaX++;
                deltaY++;
            }

            //x++, y--
            deltaX = 0;
            deltaY = range;
            for (int i = 0; i < range; i++)
            {
                moveThis = new int[] { x, y };
                tilesList.Find(current => current.gridLocation[0] == (x + deltaX)
                                && current.gridLocation[1] == (y + deltaY)).IsWalkable = true;
                deltaX++;
                deltaY--;
            }

        }

    }
}
