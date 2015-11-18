#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion


namespace Towerdefense
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont gameFont;
        GameManager gameManager = new GameManager();

        Rectangle fullscreen;

        Texture2D background;

        Texture2D nonroad;
        Texture2D road1;
        Texture2D road2;
        Texture2D road3;
        Texture2D road4;
        Texture2D[] roadArray;

        Texture2D lifeIcon;
        Texture2D tower1Icon;
        Texture2D tower2Icon;
        Texture2D tower3Icon;
        Texture2D tower4Icon;
        Texture2D moneyIcon;
        Texture2D[] menuTextureArray;
        Vector2[] menuGridCenterArray;
        Rectangle[] menuRectangle;

        Vector2 highlitedGridElement;
        Vector2 offset;
        Vector2 menuCenterPosition;

        Vector2[,] roadTypeAndRotation;

        MouseState previousMouseState;

        float pauseAlpha;
        static int amountOfField = 20;
        int highlitedMenuElement;

        Boolean drawTower;

        /*Holds the center positions of all GridElements*/
        Vector2[,] FieldCenterPosition = new Vector2[amountOfField, amountOfField];
        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            previousMouseState = Mouse.GetState();
            drawTower = false;
        }

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            #region Load Textures
            gameFont = content.Load<SpriteFont>("gamefont");
            background = content.Load<Texture2D>("background");
            nonroad = content.Load<Texture2D>("nonroad");
            road1 = content.Load<Texture2D>("road1");
            road2 = content.Load<Texture2D>("road2");
            road3 = content.Load<Texture2D>("road3");
            road4 = content.Load<Texture2D>("road4");

            lifeIcon = content.Load<Texture2D>("Menu/shieldicon");
            moneyIcon = content.Load<Texture2D>("Menu/helmicon");
            tower1Icon = content.Load<Texture2D>("Menu/tower1");
            tower2Icon = content.Load<Texture2D>("Menu/tower1");
            tower3Icon = content.Load<Texture2D>("Menu/tower1");
            tower4Icon = content.Load<Texture2D>("Menu/tower1");

            roadArray = new Texture2D[] { nonroad, road1, road2, road3, road4 };
            menuTextureArray = new Texture2D[] {  lifeIcon, moneyIcon, tower1Icon, tower2Icon,tower3Icon,tower4Icon,};
            #endregion    

            


            offset.X = ScreenManager.GraphicsDevice.Viewport.Height / amountOfField;
            offset.Y = ScreenManager.GraphicsDevice.Viewport.Height / amountOfField;

            //creates the grid
            FieldCenterPosition = gameManager.createGrid(ScreenManager.GraphicsDevice.Viewport.Height, amountOfField);
            float menuCenterWidth = (ScreenManager.GraphicsDevice.Viewport.Width - FieldCenterPosition[amountOfField - 1, amountOfField - 1].X) / 2 + FieldCenterPosition[amountOfField - 1, amountOfField - 1].X + offset.X;
            menuCenterPosition = new Vector2(menuCenterWidth, ScreenManager.GraphicsDevice.Viewport.Height / 2);

            menuGridCenterArray = gameManager.createMenuGrid(menuTextureArray, menuCenterPosition, offset);

            fullscreen = new Rectangle(0, 0, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);


            menuRectangle = new Rectangle[menuTextureArray.Length];

            for (int i = 0; i < menuTextureArray.Length; i++)
            {
                menuRectangle[i] = new Rectangle((int)(menuGridCenterArray[i].X - offset.X), (int)(menuGridCenterArray[i].Y - offset.Y), menuTextureArray[i].Width, menuTextureArray[i].Height);
            }


            roadTypeAndRotation = new Vector2[amountOfField, amountOfField];
            highlitedGridElement = new Vector2(-1, -1);

            /*Load Level in roadTypeAndRotation*/
            for (int i = 0; i < amountOfField; i++)
            {
                for (int j = 0; j < amountOfField; j++)
                {
                    roadTypeAndRotation[i, j].X = 0;
                    roadTypeAndRotation[i, j].Y = 0;
                }
            }

                    // A real game would probably have more content than this sample, so
                    // it would take longer to load. We simulate that by delaying for a
                    // while, giving you a chance to admire the beautiful loading screen.
                    Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {

                // TODO: this game isn't very fun! You could probably improve
                // it by inserting something more interesting in this space :-)
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];
            MouseState mouseState = Mouse.GetState();
            

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
              highlitedGridElement = gameManager.SetCurrentFieldMouse(mouseState,offset,highlitedGridElement,true);
              highlitedMenuElement = gameManager.SetCurrentMenuField(mouseState,menuRectangle);
              drawTower = gameManager.TowerToMouse(mouseState,previousMouseState,menuRectangle,drawTower);
             
         
               //TODO Handle Input

                previousMouseState = mouseState;
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);
         

            
            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            spriteBatch.Draw(background, fullscreen,
                               new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
            spriteBatch.End();


            /*Draws the Menu*/
           gameManager.drawMenu(spriteBatch, menuGridCenterArray, menuTextureArray,highlitedMenuElement);


            /*Draws the normal grid (has to be replaced with load level*/
            gameManager.drawGrid(roadTypeAndRotation, FieldCenterPosition, highlitedGridElement, amountOfField,roadArray, content, spriteBatch, ScreenManager.GraphicsDevice);

            /*Draws The TowerTexture to the Mouseposition when leftclicked*/
            gameManager.drawTowerToMouse(Mouse.GetState().Position, drawTower, spriteBatch, tower1Icon,amountOfField,ScreenManager.GraphicsDevice);


            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
        #endregion
    }
}
