using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefense.src
{
    class Player
    {
        private int goldAmount;
        private int score;

        public void setGold(int goldAmount) { this.goldAmount = goldAmount; }
        public int getGold() { return this.goldAmount; }

    }
}
