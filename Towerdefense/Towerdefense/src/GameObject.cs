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

        public GameObject(Texture2D sprite) {

            amountObjects++;
            ID = amountObjects;
            this.sprite = sprite;
        }
        public int getID(){return this.ID;}
        public void setSprite(Texture2D sprite) { this.sprite = sprite; }
  
    }
}
