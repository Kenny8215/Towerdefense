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
        int ID = 0;
        Texture2D Sprite;

        public int getID()
        {
            return this.ID;
        }
        public void setSprite(Texture2D Sprite)
        {
            this.Sprite = Sprite;
        }
  
    }
}
