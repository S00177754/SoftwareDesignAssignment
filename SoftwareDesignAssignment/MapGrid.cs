using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public bool isWalkable;
        public SpriteBatch spriteBatch { get; private set; }
        public List<Tile> tilesList;
        List<Texture2D> textures;

        //Constructor
        public MapGrid(Game game,int tile_width, int tile_height, int[,] tilemap, List<Texture2D> textureList)
        {
            game.Services.AddService(this);
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
                    isWalkable = true;

                    switch (textureIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            isWalkable = true;
                            break;

                        case 5:
                        case 6:
                            isWalkable = false;
                            break;
                    }

                    tilesList.Add(new Tile(MyGame, texture, new Vector2(x * 64, y * 64), 
                        isWalkable,new int[] {x,y}));
                }
            }
        }

        public void CheckMoves(int[] playerPos, int range)
        {
            int x = playerPos[0];
            int y = playerPos[1];
            int[] moveFromHere = playerPos;
            //tilesList.Find(current => current.gridLocation[0] == x && current.gridLocation[1] == y).gridLocation;

            int deltaX = range;
            int deltaY = 0;
            //Check Movementrange
            for (int i = 0; i <= range; i++)
            {
                Tile checkThisTile = tilesList.Find(current => current.gridLocation[0] == (x + deltaX)
                                                    && current.gridLocation[1] == (y + deltaY));
                
                if (checkThisTile != null && checkThisTile.IsPassable) //&& checkThisTile.gridLocation[0] != member.gridCell[0] && checkThisTile.gridLocation[1] != member.gridCell[1]
                {
                    checkThisTile.IsWalkable = true;
                }
                deltaX--;
                deltaY++;
            }

            deltaX = range;
            deltaY = 0;
            for (int i = 0; i <= range; i++)
            {
                Tile checkThisTile = tilesList.Find(current => current.gridLocation[0] == (x + deltaX)
                                                    && current.gridLocation[1] == (y + deltaY));
                if (checkThisTile != null && checkThisTile.IsPassable)
                    checkThisTile.IsWalkable = true;
                deltaX--;
                deltaY--;
            }

            deltaX = -range;
            deltaY = 0;
            for (int i = 0; i <= range; i++)
            {
                Tile checkThisTile = tilesList.Find(current => current.gridLocation[0] == (x + deltaX)
                                                    && current.gridLocation[1] == (y + deltaY));
                if(checkThisTile != null && checkThisTile.IsPassable)
                    checkThisTile.IsWalkable = true;
                deltaX++;
                deltaY++;
            }

            deltaX = -range;
            deltaY = 0;
            for (int i = 0; i <= range; i++)
            {
                Tile checkThisTile = tilesList.Find(current => current.gridLocation[0] == (x + deltaX)
                && current.gridLocation[1] == (y + deltaY));
                if (checkThisTile != null && checkThisTile.IsPassable)
                    checkThisTile.IsWalkable = true;
                deltaX++;
                deltaY--;
            }

            foreach (Tile tile in tilesList)
            {
                tile.PassableColor();
                
            }

        }

        public void ResetWalkable()
        {
            foreach (Tile tile in tilesList)
            {
                tile.ResetColor();
                isWalkable = false;
            }
        }

        public void Display(bool value)
        {
            foreach (var tile in tilesList)
            {
                tile.displayMap = value;
            }
        }

        public void CheckAttack(int[] playerPos, int range)
        {
            int x = playerPos[0];
            int y = playerPos[1];
            int[] moveFromHere = playerPos;
            //tilesList.Find(current => current.gridLocation[0] == x && current.gridLocation[1] == y).gridLocation;

            int deltaX = range;
            int deltaY = 0;
            //Check Movementrange
            for (int i = 0; i <= range; i++)
            {
                Tile checkThisTile = tilesList.Find(current => current.gridLocation[0] == (x + deltaX)
                                                    && current.gridLocation[1] == (y + deltaY));
                if (checkThisTile != null && checkThisTile.IsPassable)
                    checkThisTile.IsAttackable = true;
                deltaX--;
                deltaY++;
            }

            deltaX = range;
            deltaY = 0;
            for (int i = 0; i <= range; i++)
            {
                Tile checkThisTile = tilesList.Find(current => current.gridLocation[0] == (x + deltaX)
                                                    && current.gridLocation[1] == (y + deltaY));
                if (checkThisTile != null && checkThisTile.IsPassable)
                    checkThisTile.IsAttackable = true;
                deltaX--;
                deltaY--;
            }

            deltaX = -range;
            deltaY = 0;
            for (int i = 0; i <= range; i++)
            {
                Tile checkThisTile = tilesList.Find(current => current.gridLocation[0] == (x + deltaX)
                                                    && current.gridLocation[1] == (y + deltaY));
                if (checkThisTile != null && checkThisTile.IsPassable)
                    checkThisTile.IsAttackable = true;
                deltaX++;
                deltaY++;
            }

            deltaX = -range;
            deltaY = 0;
            for (int i = 0; i <= range; i++)
            {
                Tile checkThisTile = tilesList.Find(current => current.gridLocation[0] == (x + deltaX)
                && current.gridLocation[1] == (y + deltaY));
                if (checkThisTile != null && checkThisTile.IsPassable)
                    checkThisTile.IsAttackable = true;
                deltaX++;
                deltaY--;
            }

            //foreach (Tile tile in tilesList)
            //{
            //    tile.PassableColor();
            //    Debug.WriteLine("Color");
            //}
        }

    }
}
