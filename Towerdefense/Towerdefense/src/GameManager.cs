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

        /*list of tower in game*/
        List<Tower> towerList;

        /*list of enemywaves*/
        SortedList<int, List<Enemy>> waveList;

        /*actual wave*/
        int actualWave = 0;

        /*LevelObject which contains Content loaded out of an *.xml */
        LoadLevel levelObject;

        public GameManager()
        {
            levelObject = new LoadLevel();
            levelObject.load("./level_1.xml");
            waveList = levelObject.getWaves();
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
        public void DrawInitializedGrid(Vector2[,] center,Vector2 highlightIndex, int amountOfFields, Texture2D texture, ContentManager content, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            float offset = 0.5F * ((float)graphicsDevice.Viewport.Height / amountOfFields);
            Vector2 textureCenter = new Vector2(texture.Width, texture.Height);
            float scale = (float)graphicsDevice.Viewport.Height / (amountOfFields * texture.Height);
            spriteBatch.Begin();
            for (int i = 0; i < center.GetLength(0); i++)
            {
                for (int j = 0; j < center.GetLength(1); j++){
                    if (i == highlightIndex.X && j == highlightIndex.Y){
                        spriteBatch.Draw(texture, center[i, j] + new Vector2(offset, offset),null, new Color(255,255,0,0.7F), 0, textureCenter, scale, SpriteEffects.None, 0);
                    }
                    else{

                        spriteBatch.Draw(texture, center[i, j] + new Vector2(offset, offset), null, Color.White, 0, textureCenter, scale, SpriteEffects.None, 0);
                    }

                    }
            }
            spriteBatch.End();
        }


        public Vector2 SetNewField(KeyboardState ks,KeyboardState ps, Vector2 currentField,int amountOfField) {
           
                if (ks.IsKeyDown(Keys.Right) && !ps.IsKeyDown(Keys.Right) && currentField.Y < (amountOfField-1))
                {
                    currentField.Y++;
                }

                if (ks.IsKeyDown(Keys.Left) && !ps.IsKeyDown(Keys.Left) && currentField.Y > 0)
                {
                     currentField.Y--; 
                }

                if (ks.IsKeyDown(Keys.Up) && !ps.IsKeyDown(Keys.Up) && currentField.X > 0)
                {
                    currentField.X--; 
                }

                if (ks.IsKeyDown(Keys.Down) && !ps.IsKeyDown(Keys.Down) && currentField.X < (amountOfField-1))
                {
                    currentField.X++;
                }
            
            return currentField;
        }

        public void changeSprite(){
    
        }

        public void LevelEditorMenu() {
        
        }
    }

}
