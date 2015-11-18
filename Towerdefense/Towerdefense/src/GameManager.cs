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
        public Vector2 SetCurrentFieldMouse(MouseState ms, Vector2 offset, Vector2 currentField, Boolean SetFieldWithMouse)
        {

            if (SetFieldWithMouse)
            {
                currentField.X = (int)(ms.Position.X / offset.X);
                currentField.Y = (int)(ms.Position.Y / offset.Y);
            }
            return currentField;
        }

        public Boolean TowerToMouse(MouseState ms, MouseState ps,Rectangle[] menuRectangle,Boolean drawTower) {
            if (ms.RightButton == ButtonState.Pressed && ps.RightButton == ButtonState.Released) { return false; }
            if (drawTower == true)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < menuRectangle.Length; i++)
                    if (ms.LeftButton == ButtonState.Pressed && ps.LeftButton == ButtonState.Released && menuRectangle[i].Contains(ms.Position))
                    {
                        return true;
                    }
            }

            return false;
        }

        public void drawTowerToMouse(Point ms,Boolean drawTower,SpriteBatch spriteBatch,Texture2D towerTexture,int amountOfFields,GraphicsDevice graphicsDevice) {
            float scale = (float)graphicsDevice.Viewport.Height / (amountOfFields * towerTexture.Height);
            Vector2 origin = new Vector2(towerTexture.Width/2,towerTexture.Height/2);
            Vector2 msV = new Vector2(ms.X,ms.Y);
            if (drawTower) {
                spriteBatch.Begin();
                spriteBatch.Draw(towerTexture, msV, null, Color.White, 0F, origin, scale, SpriteEffects.None, 1F);
                spriteBatch.End();
            }
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
        public Vector2 SetNewField(KeyboardState ks, KeyboardState ps, Vector2 currentField, int amountOfField)
        {

            if (ks.IsKeyDown(Keys.Right) && !ps.IsKeyDown(Keys.Right) && currentField.X < (amountOfField - 1))
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

            if (ks.IsKeyDown(Keys.Down) && !ps.IsKeyDown(Keys.Down) && currentField.Y < (amountOfField - 1))
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
                        spriteBatch.Draw(texture[(int)roadTypeRotation[i, j].X], center[i, j], null, new Color(255, 255, 0, 1F), (roadTypeRotation[i, j].Y) * rotation, textureCenter, scale, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(texture[(int)roadTypeRotation[i, j].X], center[i, j], null, Color.White, (roadTypeRotation[i, j].Y) * rotation, textureCenter, scale, SpriteEffects.None, 0);
                    }
                }
            }
            spriteBatch.End();

        }

        /*Draws the sprites at the center of each gridelement, can be used to initialize the grid with a specific texture,
          * This method is called in the LoadContent method in the GameplayScreen*/
        public Vector2[,] setRoadTypeAndRotation(KeyboardState ks, KeyboardState ps, Vector2 highlightedGridElement, Vector2[,] roadTypeRotation, Texture2D[] textureArray)
        {

            /*Sets the Road Type to the next Type when Enter is pressed*/
            if (ks.IsKeyDown(Keys.Enter) && !ps.IsKeyDown(Keys.Enter))
            {
                roadTypeRotation[(int)highlightedGridElement.X, (int)highlightedGridElement.Y].X++;
                if (roadTypeRotation[(int)highlightedGridElement.X, (int)highlightedGridElement.Y].X >= textureArray.Length) { roadTypeRotation[(int)highlightedGridElement.X, (int)highlightedGridElement.Y].X = 0; }
            }

            /*Rotates the current Road 90 degree*/
            if (ks.IsKeyDown(Keys.R) && !ps.IsKeyDown(Keys.R))
            {
                roadTypeRotation[(int)highlightedGridElement.X, (int)highlightedGridElement.Y].Y++;
                if (roadTypeRotation[(int)highlightedGridElement.X, (int)highlightedGridElement.Y].Y >= textureArray.Length) { roadTypeRotation[(int)highlightedGridElement.X, (int)highlightedGridElement.Y].Y = 0; }
            }

            return roadTypeRotation;
        }

        public Boolean LevelEditorMenu(MouseState ms, Boolean DrawMenu)
        {


            if (DrawMenu == true && ms.MiddleButton == ButtonState.Pressed) { DrawMenu = false; }

            if (ms.RightButton == ButtonState.Pressed) { DrawMenu = true; }

            return DrawMenu;
        }

        #endregion

        #region Menu

        public Vector2[] createMenuGrid(Texture2D[] MenuTextureArray, Vector2 MenuCenterPosition, Vector2 offset)
        {
            Vector2[] safeMenuPositions = new Vector2[MenuTextureArray.Length];
            Vector2 Origin;
            Vector2 centerTemp = MenuCenterPosition;

            centerTemp = new Vector2(MenuCenterPosition.X, 2 * offset.Y);
            for (int i = 0; i < MenuTextureArray.Length; i++)
            {

                Origin = new Vector2(MenuTextureArray[i].Width / 2, MenuTextureArray[i].Height / 2);

                if (i % 2 == 0)
                {
                    //safe position into safeMenuPosition
                    safeMenuPositions[i] = new Vector2(centerTemp.X + offset.X, centerTemp.Y);
                }
                else
                {
                    safeMenuPositions[i] = new Vector2(centerTemp.X - 2 * offset.X, centerTemp.Y);
                    centerTemp.Y += 2 * offset.Y;
                }

            }
            return safeMenuPositions;
        }

        public void drawMenu(SpriteBatch spriteBatch, Vector2[] menuPositionArray, Texture2D[] menuTextureArray, int highlitedMenuElement)
        {
            Vector2 Origin;

            spriteBatch.Begin();
            for (int i = 0; i < menuTextureArray.Length; i++)
            {

                Origin = new Vector2(menuTextureArray[i].Width / 2, menuTextureArray[i].Height / 2);
                if (highlitedMenuElement == i)
                {
                    spriteBatch.Draw(menuTextureArray[i], menuPositionArray[i], null, new Color(255, 255, 0, 1F), 0F, Origin, 1F, SpriteEffects.None, 1F);
                }
                else
                {
                    spriteBatch.Draw(menuTextureArray[i], menuPositionArray[i], null, Color.White, 0F, Origin, 1F, SpriteEffects.None, 1F);
                }

            }
            spriteBatch.End();
        }

        public int SetCurrentMenuField(MouseState ms, Rectangle[] menuRectangle)
        {

            for (int j = 0; j < menuRectangle.Length; j++)
            {
                if (menuRectangle[j].Contains(ms.Position.X, ms.Position.Y))
                {
                    return j;
                }
            }
            return -1;
        }
        #endregion
    }
}

