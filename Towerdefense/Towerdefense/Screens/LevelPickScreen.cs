#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace Towerdefense
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class LevelPickScreen : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public LevelPickScreen()
            : base("Pick Level")
        {
            // Create our menu entries.
            MenuEntry Level1MenuEntry = new MenuEntry("Level 1");
            MenuEntry exitMenuEntry = new MenuEntry("Back");

            // Hook up menu event handlers.
            Level1MenuEntry.Selected += Level1MenuEntrySelected;
            exitMenuEntry.Selected += OnBack;

            // Add entries to the menu.
            MenuEntries.Add(Level1MenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void Level1MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("bsp_lvl.xml"));
        }

        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        void OnBack(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new MainMenuScreen());
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }


        #endregion
    }
}
