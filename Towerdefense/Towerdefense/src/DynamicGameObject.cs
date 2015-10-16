using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefense.src
{
    class DynamicGameObject
    {
        private int hitPoints;

        public int getHitPoints(){return this.hitPoints;}
        public void loseHitPoints(int dmg) { this.hitPoints -= dmg; }
    }
}
