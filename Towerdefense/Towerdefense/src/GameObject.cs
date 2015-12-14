using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefense
{
    abstract class GameObject
    {
        #region Fields
        private static int amountObjects = 0;
        private int ID;
        Texture2D sprite;
        #endregion

        #region Constructor
        public GameObject()
        {

        }
        public GameObject(Texture2D sprite) {

            amountObjects++;
            ID1 = amountObjects;
            Sprite = sprite;
        }
        #endregion

        #region setter and getter
        public int ID1
        {
            get
            {
                return ID;
            }

            set
            {
                ID = value;
            }
        }

        public Texture2D Sprite
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;
            }
        }

        public int getID(){return ID1; }
        public void setSprite(Texture2D sprite) { Sprite = sprite; }
        #endregion
    }
}
