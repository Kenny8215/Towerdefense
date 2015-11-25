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
    class LevelEditorScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont gameFont;
        GameManager gameManager = new GameManager();

        Rectangle fullscreen;

        Texture2D background;

        Texture2D menu;
        Texture2D nonroad;
        Texture2D road1;
        Texture2D road2;
        Texture2D road3;
        Texture2D road4;
        Texture2D[] textures;

        MouseState prevMouseState;

        float pauseAlpha;
        static int amountOfField = 20;
        Boolean hasDrawnGrid = false;
        Boolean DrawMenu = false;
        Boolean setWithMouse = false;
        Vector2 highlitedGridElement = new Vector2(0,0);
        Vector2 mousePosition;
        Vector2 offset;

        Vector2[,] roadTypeAndRotation;
        Vector2[,] FieldCenterPosition = new Vector2[amountOfField,amountOfField];


        SaveLevel saveLevel = new SaveLevel();
        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public LevelEditorScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>("gamefont");

            background = content.Load<Texture2D>("background");


            //menu = content.Load<Texture2D>("shortcuts");
            nonroad = content.Load<Texture2D>("tiles/noRoad1");
            road1 = content.Load<Texture2D>("tiles/road");
            road2 = content.Load<Texture2D>("tiles/road1");
            road3 = content.Load<Texture2D>("tiles/road2");
            road4 = content.Load<Texture2D>("tiles/road3");
            roadTypeAndRotation = new Vector2[amountOfField, amountOfField];
            for (int i = 0; i < amountOfField; i++) {
                for (int j = 0; j < amountOfField; j++)
                {
                    roadTypeAndRotation[i, j].X = 0;
                    roadTypeAndRotation[i,j].Y = 0;
                }
            }
                textures = new Texture2D[] { nonroad, road1, road2, road3, road4 };

                offset.X = ScreenManager.GraphicsDevice.Viewport.Height / amountOfField;
                offset.Y = ScreenManager.GraphicsDevice.Viewport.Height / amountOfField;

                fullscreen = new Rectangle(0, 0, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);

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
            KeyboardState lastKeyboardState = input.LastKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];
            MouseState ms = Mouse.GetState();

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
                //creates the grid
                if (hasDrawnGrid == false)
                {
                    FieldCenterPosition = gameManager.createGrid(ScreenManager.GraphicsDevice.Viewport.Height, amountOfField); hasDrawnGrid = true;
                }

                gameManager.SetCurrentFieldMouse(ms, offset, highlitedGridElement, setWithMouse);

                // Sets the new field if the user pressed left,right,up or down
                highlitedGridElement = gameManager.SetNewField(keyboardState, lastKeyboardState, highlitedGridElement, amountOfField);

                //Sets the new roadType when the user Presses Enter also Rotates the texture 90 degree when R is pressed
                roadTypeAndRotation = gameManager.setRoadTypeAndRotation(keyboardState, lastKeyboardState, highlitedGridElement, roadTypeAndRotation, textures);

                /*Menu*/

                DrawMenu = gameManager.LevelEditorMenu(Mouse.GetState(), DrawMenu);
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                }
                prevMouseState = Mouse.GetState();

                if (keyboardState.IsKeyDown(Keys.S)&&!lastKeyboardState.IsKeyDown(Keys.S))
                {
                    saveLevel.save(roadTypeAndRotation);
                }
                lastKeyboardState = keyboardState;
            }
        }

                
        

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            spriteBatch.Draw(background, fullscreen,
                               new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
            gameManager.drawGrid(roadTypeAndRotation, FieldCenterPosition, highlitedGridElement, amountOfField, textures, content, spriteBatch, ScreenManager.GraphicsDevice);

            if (DrawMenu)
            {
                spriteBatch.Draw(menu, mousePosition, null, Color.White, 0F, new Vector2(menu.Width / 2, menu.Height / 2), 1F, SpriteEffects.None, 1);
            }
            spriteBatch.End();
            

            
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
