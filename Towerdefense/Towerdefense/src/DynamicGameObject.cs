using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefense
{
    abstract class DynamicGameObject : GameObject
    {
        private int hitPoints;

        public int HitPoints
        {
            get
            {
                return hitPoints;
            }

            set
            {
                hitPoints = value;
            }
        }

        public int getHitPoints(){return this.HitPoints;}
        public void loseHitPoints(int dmg) { this.HitPoints -= dmg; }
        
        public DynamicGameObject()
        {

        }

        public DynamicGameObject(Texture2D sprite, int hitPoints) : base(sprite){
            this.HitPoints = hitPoints;
        }
    }
}
