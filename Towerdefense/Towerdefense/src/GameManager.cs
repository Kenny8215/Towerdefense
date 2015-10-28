using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Towerdefense
{
    class GameManager
    {
        /*list of sprites*/
        SortedList<enemyType, Texture2D> spriteList = new SortedList<enemyType, Texture2D>();

        /*list of tower in game*/
        List<Tower> towerList;

        /*list of enemywaves*/
        SortedList<int, List<Enemy>> waveList;

        /*actual wave*/
        int actualWave;

        public enum enemyType
        {
            Wolf
        };

        public GameManager() { }


        public GameManager(int waveQuantity, int[] enemyQuantityPerWave, enemyType[] enemyTypeInWave)
        {

            for (int i = 0; i < waveQuantity; i++)
            {
                List<Enemy> temp = new List<Enemy>();
                for (int j = 0; j < enemyQuantityPerWave.Length; j++)
                {
                    temp.Add(makeEnemy(enemyTypeInWave[i]));
                }
                waveList.Add(i, temp);
            }
        }

        private Enemy makeEnemy(enemyType newEnemyType)
        {
            Enemy newEnemy;
            Texture2D sprite;
            spriteList.TryGetValue(newEnemyType, out sprite);

            switch (newEnemyType)
            {
                case enemyType.Wolf:
                    newEnemy = new Enemy(sprite, 5, 5, 5.5f, "Eis", false, false);
                    break;
                default:
                    newEnemy = null;
                    break;
            }
            return newEnemy;
        }

        public void draw()
        {

        }


        public void updateEnemys()
        {

        }


        /*Calculates the center of each gridelement*/
        public Vector2[,] createGrid(float maxHeight, int amountOfFields)
        {
            Vector2[,] positionArray = new Vector2[amountOfFields, amountOfFields];
            float offset = maxHeight / amountOfFields;
            float y = 0;
            float x = 0;

            for (int i = 0; i < amountOfFields; i++)
            {
                y += offset / 2;
                x = 0;
                for (int j = 0; j < amountOfFields; j++)
                {
                    x += offset / 2;
                    positionArray[i, j] = new Vector2(x, y);
                    x += offset / 2;
                    //Console.WriteLine("X :" + positionArray[i, j].X + "Y :" + positionArray[i, j].Y);
                }

                y += offset / 2;
            }
            return positionArray;
        }


        /*Draws the sprites at the center of each gridelement, can be used to initialize the grid with a specific texture,
         * This method is called in the LoadContent method in the GameplayScreen*/
        public void DrawInitializedGrid(Vector2[,] center, int amountOfFields, Texture2D texture, ContentManager content, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            float offset = 0.5F * ((float)graphicsDevice.Viewport.Height / amountOfFields);
            Vector2 textureCenter = new Vector2(texture.Width, texture.Height);
            float scale = (float)graphicsDevice.Viewport.Height / (amountOfFields * texture.Height);
            spriteBatch.Begin();
            for (int i = 0; i < center.GetLength(0); i++)
            {
                for (int j = 0; j < center.GetLength(1); j++)
                {
                    spriteBatch.Draw(texture, center[i, j] + new Vector2(offset, offset), null, Color.White, 0, textureCenter, scale, SpriteEffects.None, 0);
                }
            }
            spriteBatch.End();
        }
    }

}
