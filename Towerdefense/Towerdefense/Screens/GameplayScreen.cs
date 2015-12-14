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
using System.Collections.Generic;
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
        GameManager gameManager;

        Player player = new Player(null, 20, 200, 0);

        Rectangle fullscreen;

        Texture2D background;

        Texture2D nonroad;
        Texture2D nonroad1;
        Texture2D nonroad2;
        Texture2D nonroad3;
        Texture2D nonroad4;
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
        Texture2D enemy1;
        Texture2D rangeCircle;
        Texture2D upgrade;
        Texture2D projectile;

        SpriteFont arial;

        Texture2D[] menuTextureArray;
        Vector2[] menuGridCenterArray;
        Vector2[] currentEnemyPosition = new Vector2[2];
        Rectangle[] menuRectangle;

        Vector2 highlightedGridElement;
        Vector2 offset;
        Vector2 menuCenterPosition;

        Vector2[,] roadTypeAndRotation;

        List<Tower> placedTowerList;
        List<Tower> placeableTower;
        List<Projectile> PList;
        List<Enemy> removedEnemies;

        MouseState previousMouseState;

        float pauseAlpha;
        float menuCenterWidth;
        int amountOfField;
        int highlitedMenuElement;
        int towerAmount;

        Boolean drawTower;
        Boolean selectTower;

        /*Holds the center positions of all GridElements*/
        Vector2[,] FieldCenterPosition;
        private List<Wave> waveList;
        private List<Field> grid;


        int currentWave;
        List<Enemy> toDraw;
        int currentEnemy;


        double timeLaps;


        bool levelDone = false;
        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        #region Constructors
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            previousMouseState = Mouse.GetState();
            drawTower = false;
            placedTowerList = new List<Tower>();
        }

        public GameplayScreen(string v)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            gameManager = new GameManager(v);

            previousMouseState = Mouse.GetState();
            drawTower = false;
            placedTowerList = gameManager.PlacedTowerList;
            placeableTower = gameManager.PlaceableTower;
            waveList = gameManager.WaveList;
            grid = gameManager.Grid;
            amountOfField = gameManager.getGridCount();
            FieldCenterPosition = new Vector2[amountOfField, amountOfField];
            currentWave = 0;
            currentEnemy = 0;
            toDraw = new List<Enemy>();
            timeLaps = 0;
            removedEnemies = new List<Enemy>();

        }
        #endregion
        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            #region Load Textures
            foreach (Wave w in waveList)
            {
                w.Enemy.Sprite = content.Load<Texture2D>(w.Enemy.SpritePath);
                w.Enemy.HealthBar = content.Load<Texture2D>("enemies/health");
            }

            foreach (Tower t in placeableTower)
            {
                t.Sprite = content.Load<Texture2D>(t.SpritePath);
                t.Weapon = content.Load<Texture2D>(t.WeaponPath);
                t.RangeCircle = content.Load<Texture2D>(t.RangeCirclePath);
                t.Upgrade = content.Load<Texture2D>(t.UpdatePath);
                t.ProjectileSprite = content.Load<Texture2D>("projectileTMP");
            }
       
          
            gameFont = content.Load<SpriteFont>("gamefont");
            background = content.Load<Texture2D>("background");
            nonroad = content.Load<Texture2D>("tiles/noRoad1");
            nonroad1 = content.Load<Texture2D>("tiles/noRoad1");
            nonroad2 = content.Load<Texture2D>("tiles/noRoad1");
            nonroad3 = content.Load<Texture2D>("tiles/noRoad1");
            nonroad4 = content.Load<Texture2D>("tiles/noRoad1");
            road1 = content.Load<Texture2D>("tiles/road");
            road2 = content.Load<Texture2D>("tiles/road1");
            road3 = content.Load<Texture2D>("tiles/road2");
            road4 = content.Load<Texture2D>("tiles/road3");

            lifeIcon = content.Load<Texture2D>("Menu/shieldicon");
            moneyIcon = content.Load<Texture2D>("Menu/helmicon");
            tower1Icon = content.Load<Texture2D>("Menu/tower1");
            tower2Icon = content.Load<Texture2D>("Menu/tower2");
       /*     tower3Icon = content.Load<Texture2D>("Menu/tower1");
            tower4Icon = content.Load<Texture2D>("Menu/tower1");*/
            rangeCircle = content.Load<Texture2D>("rangeCircle");

            upgrade = content.Load < Texture2D>("tower/upgradeGreen");

            roadArray = new Texture2D[] { nonroad, nonroad1, road1, road2, road3, road4,nonroad2,nonroad3,nonroad4 };
            menuTextureArray = new Texture2D[] { lifeIcon, moneyIcon, tower1Icon, tower2Icon};

            foreach(Wave w in waveList)
            {
                foreach(Enemy e in w.getEnemys())
                {
                    e.setSprite(content.Load<Texture2D>(e.SpritePath));
                    e.HealthBar = content.Load<Texture2D>("enemies/health");
                }
            }
            #endregion    

            #region Fonts
            arial = content.Load<SpriteFont>("Arial");
            #endregion

            #region Preparing the Grid
            //sidelength of each field
            offset.X = ScreenManager.GraphicsDevice.Viewport.Height / amountOfField;
            offset.Y = ScreenManager.GraphicsDevice.Viewport.Height / amountOfField;

            //creates the grid and saves the Centerpositions of each gridelement in FieldCenterPosition
            FieldCenterPosition = gameManager.createGrid(ScreenManager.GraphicsDevice.Viewport.Height, amountOfField);

            roadTypeAndRotation = new Vector2[amountOfField, amountOfField];
            highlightedGridElement = new Vector2(-1, -1);
            #endregion

            #region Menu
            menuCenterWidth = (ScreenManager.GraphicsDevice.Viewport.Width - FieldCenterPosition[amountOfField - 1, amountOfField - 1].X) / 2 + FieldCenterPosition[amountOfField - 1, amountOfField - 1].X;
            menuCenterPosition = new Vector2(menuCenterWidth, ScreenManager.GraphicsDevice.Viewport.Height / 2);
            menuGridCenterArray = gameManager.createMenuGrid(menuTextureArray, menuCenterPosition, new Vector2(30, 30));
            menuRectangle = new Rectangle[menuTextureArray.Length];

            for (int i = 0; i < menuTextureArray.Length; i++)
            {
                menuRectangle[i] = new Rectangle((int)(menuGridCenterArray[i].X - offset.X), (int)(menuGridCenterArray[i].Y - offset.Y), menuTextureArray[i].Width, menuTextureArray[i].Height);
            }

            #endregion

            #region Tower
            placedTowerList = gameManager.PlacedTowerList;
            #endregion

            #region fullscreen
            fullscreen = new Rectangle(0, 0, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);
            #endregion

            #region Loadlevel
            /*Load Level in roadTypeAndRotation*/
            for (int i = 0; i < amountOfField; i++)
            {
                for (int j = 0; j < amountOfField; j++)
                {
                    roadTypeAndRotation[i, j].X = 0;
                    roadTypeAndRotation[i, j].Y = 0;
                }
            }

            foreach (Field f in grid)
            {
                roadTypeAndRotation[f.X, f.Y].X = f.Type;
                roadTypeAndRotation[f.X, f.Y].Y = f.Rotation;
            }

            roadTypeAndRotation = gameManager.randomNonRoads(roadTypeAndRotation, amountOfField);
            #endregion

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

            timeLaps += gameTime.ElapsedGameTime.TotalSeconds;

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                placedTowerList = gameManager.PlacedTowerList;
                if (timeLaps >= 1)
                {
                    timeLaps = 0;
                    if (!levelDone)
                    {
                        toDraw.Add(waveList[currentWave].getEnemys()[currentEnemy]);
                        try
                        {
                            if (waveList[currentWave].getEnemys()[currentEnemy + 1] != null)
                            {
                                currentEnemy += 1;
                            }
                            else
                            {

                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            currentWave += 1;
                            try
                            {
                                if (waveList[currentWave + 1] != null)
                                {
                                    currentWave += 1;
                                }
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                levelDone = true;
                            }

                        }
                    }
                    else
                    {
                        if (toDraw.Count == 0)
                        {
                            ScreenManager.AddScreen(new WinScreen(), ControllingPlayer);
                        }
                    }      
                    
                }
                foreach(Enemy e in toDraw)
                {
                    e.Position = gameManager.updateEnemies(e, roadTypeAndRotation, offset, amountOfField, FieldCenterPosition);
                        if (e.Position.X <= 0 || e.Position.Y <= 0 || e.Position.X >= amountOfField*offset.X || e.Position.Y >= amountOfField*offset.Y) {
                        removedEnemies.Add(e);
                    }
                }


                foreach (Enemy e in removedEnemies)
                {
                    player.loseHitPoints(e.Dmg);
                    toDraw.Remove(e);
                }

                gameManager.CurrentEnemys = toDraw;
                gameManager.towerShoot(placedTowerList, toDraw);
                gameManager.moveProjectiles(player);

                removedEnemies = new List<Enemy>();
                // TODO: this game isn't very fun! You could probably improve
                // it by inserting something more interesting in this space :-)

                for (int i = 0; i < placedTowerList.Count; i++)
                {
                    placedTowerList[i].SearchClosestEnemy(toDraw);
                }

                if(player.HitPoints <= 0) {

                    ScreenManager.AddScreen(new LoseScreen(), ControllingPlayer);
                }
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
                highlightedGridElement = gameManager.SetCurrentFieldMouse(mouseState, offset, highlightedGridElement, true);
                highlitedMenuElement = gameManager.SetCurrentMenuField(mouseState, menuRectangle);
                drawTower = gameManager.TowerToMouse(mouseState, previousMouseState, menuRectangle, drawTower,menuTextureArray,highlitedMenuElement,offset);

                towerAmount = placedTowerList.Count;
                placedTowerList = gameManager.addPlacedTowerToList(mouseState, previousMouseState, drawTower, placedTowerList, highlightedGridElement, tower1Icon, FieldCenterPosition, amountOfField, roadTypeAndRotation, highlightedGridElement, player, rangeCircle, upgrade,offset);
         
                if (towerAmount != placedTowerList.Count)
                {
                    roadTypeAndRotation[(int)highlightedGridElement.X, (int)highlightedGridElement.Y].X = 1;
                }

            gameManager.towerSelected(placedTowerList, highlightedGridElement);
            gameManager.towerUpgraded(placedTowerList,mouseState,previousMouseState,player);
                // roadTypeAndRotation[ towerList[towerAmount].Position.X] , towerList[towerAmount].Position.Y ] = 1;  }
            drawTower = gameManager.placeTower(mouseState, previousMouseState, drawTower, placedTowerList, highlightedGridElement, tower1Icon, FieldCenterPosition, amountOfField);

            


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

            /*Draws the Menu*/
            gameManager.drawMenu(spriteBatch, menuGridCenterArray, menuTextureArray, highlitedMenuElement, arial, player);


            /*Draws the normal grid (has to be replaced with load level*/
            gameManager.drawGrid(roadTypeAndRotation, FieldCenterPosition, highlightedGridElement, amountOfField, roadArray, content, spriteBatch, ScreenManager.GraphicsDevice);

            /*Draws All towers in the grid*/
            gameManager.drawTowers(placedTowerList, spriteBatch, ScreenManager.GraphicsDevice, amountOfField);

            gameManager.drawProjectile(spriteBatch);

            /*Draws The TowerTexture to the Mouseposition when leftclicked*/
            gameManager.drawTowerToMouse(drawTower, spriteBatch,ScreenManager.GraphicsDevice,amountOfField);

            

            gameManager.drawEnemies(toDraw, spriteBatch);
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
