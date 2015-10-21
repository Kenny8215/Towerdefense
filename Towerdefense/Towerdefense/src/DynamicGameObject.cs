using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefense.src
{
    abstract class DynamicGameObject : GameObject
    {
        private int hitPoints;

        public int getHitPoints(){return this.hitPoints;}
        public void loseHitPoints(int dmg) { this.hitPoints -= dmg; }
        
        public DynamicGameObject(Texture2D sprite, int hitPoints) : base(sprite){
            this.hitPoints = hitPoints;
        }
    }
}
