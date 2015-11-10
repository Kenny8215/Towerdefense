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
        #region Fields
        /*list of tower in game*/
        List<Tower> towerList;

        /*list of enemywaves*/
        SortedList<int, Wave> waveList;
         
        /*actual wave*/
        int actualWave = 0;

        /*LevelObject which contains Content loaded out of an *.xml */
        LoadLevel levelObject;
#endregion

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

        #region PlayerInput 
        
        public Vector2 SetCurrentFieldMouse(MouseState ms,Vector2 offset,Vector2 currentField,Boolean SetFieldWithMouse ){

            if (SetFieldWithMouse)
            {
                currentField.X = (int)(ms.Position.X / offset.X);
                currentField.Y = (int)(ms.Position.Y / offset.Y);
            }
            return currentField;
        }
        
        
        
        #endregion

        #region Grid and LevelEditor methods
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
                    positionArray[i, j] = new Vector2(y, x);
                    x += offset / 2;
                    //Console.WriteLine("X :" + positionArray[i, j].X + "Y :" + positionArray[i, j].Y);
                }

                y += offset / 2;
            }
            return positionArray;
        }

        /*Sets the current highlighted GridElement*/
        public Vector2 SetNewField(KeyboardState ks,KeyboardState ps, Vector2 currentField,int amountOfField) {
           
                if (ks.IsKeyDown(Keys.Right) && !ps.IsKeyDown(Keys.Right) && currentField.X < (amountOfField-1))
                {
                    currentField.X++;
                }

                if (ks.IsKeyDown(Keys.Left) && !ps.IsKeyDown(Keys.Left) && currentField.X > 0)
                {
                     currentField.X--; 
                }

                if (ks.IsKeyDown(Keys.Up) && !ps.IsKeyDown(Keys.Up) && currentField.Y > 0)
                {
                    currentField.Y--; 
                }

                if (ks.IsKeyDown(Keys.Down) && !ps.IsKeyDown(Keys.Down) && currentField.Y < (amountOfField-1))
                {
                    currentField.Y++;
                }
            
            return currentField;
        }

        /*Draws the grid textures*/
        public void drawGrid(Vector2[,] roadTypeRotation, Vector2[,] center, Vector2 highlightIndex, int amountOfFields, Texture2D[] texture, ContentManager content, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            float offset = 0.5F * ((float)graphicsDevice.Viewport.Height / amountOfFields);
            Vector2 offsetVector = new Vector2(offset, offset);
            float rotation = 1.57079633F;
            Vector2 textureCenter = new Vector2(texture[0].Width / 2, texture[0].Height / 2);
            float scale = (float)graphicsDevice.Viewport.Height / (amountOfFields * texture[0].Height);

            spriteBatch.Begin();
            for (int i = 0; i < center.GetLength(0); i++)
            {
                for (int j = 0; j < center.GetLength(1); j++)
                {
                    if (i == highlightIndex.X && j == highlightIndex.Y)
                    {
                        spriteBatch.Draw(texture[(int)roadTypeRotation[i, j].X], center[i, j], null, new Color(255, 255, 0, 0.5F), (roadTypeRotation[i, j].Y) * rotation, textureCenter, scale, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(texture[(int)roadTypeRotation[i, j].X], center[i, j], null, Color.White, (roadTypeRotation[i, j].Y) * rotation , textureCenter, scale, SpriteEffects.None, 0);
                    }
                }
            }
            spriteBatch.End();
    
        }

        /*Draws the sprites at the center of each gridelement, can be used to initialize the grid with a specific texture,
          * This method is called in the LoadContent method in the GameplayScreen*/
        public Vector2[,] setRoadTypeAndRotation(KeyboardState ks,KeyboardState ps,Vector2 highlightedGridElement,Vector2[,] roadTypeRotation, Texture2D[] textureArray) {
          
            /*Sets the Road Type to the next Type when Enter is pressed*/
            if (ks.IsKeyDown(Keys.Enter) && !ps.IsKeyDown(Keys.Enter))
            {
                roadTypeRotation[(int)highlightedGridElement.X, (int)highlightedGridElement.Y].X++;
                if (roadTypeRotation[(int)highlightedGridElement.X, (int)highlightedGridElement.Y].X >= textureArray.Length - 1) { roadTypeRotation[(int)highlightedGridElement.X, (int)highlightedGridElement.Y].X = 0; }
            }

            /*Rotates the current Road 90 degree*/
            if(ks.IsKeyDown(Keys.R) && !ps.IsKeyDown(Keys.R)){
                 roadTypeRotation[(int)highlightedGridElement.X, (int)highlightedGridElement.Y].Y++;
                if (roadTypeRotation[(int)highlightedGridElement.X, (int)highlightedGridElement.Y].Y >= textureArray.Length - 1) { roadTypeRotation[(int)highlightedGridElement.X, (int)highlightedGridElement.Y].Y = 0; }
            }

            return roadTypeRotation;
        }

        public Boolean LevelEditorMenu(MouseState ms, Boolean DrawMenu)
        {


            if (DrawMenu == true && ms.MiddleButton == ButtonState.Pressed) { DrawMenu = false; }

            if (ms.RightButton == ButtonState.Pressed) { DrawMenu = true; }

            return DrawMenu;
        }
        }
        #endregion
    }

