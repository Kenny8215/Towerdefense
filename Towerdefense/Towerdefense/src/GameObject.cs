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
        private static int amountObjects = 0;
        private int ID;
        Texture2D sprite;

        public GameObject()
        {

        }

        public GameObject(Texture2D sprite) {

            amountObjects++;
            ID1 = amountObjects;
            this.Sprite = sprite;
        }

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

        public int getID(){return this.ID1;}
        public void setSprite(Texture2D sprite) { this.Sprite = sprite; }
  
    }
}
