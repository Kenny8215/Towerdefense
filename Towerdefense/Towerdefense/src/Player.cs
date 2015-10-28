using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefense
{
    class Player : DynamicGameObject
    {
        private int goldAmount;
        private int score;

        public void setGold(int goldAmount) { this.goldAmount = goldAmount; }
        public int getGold() { return this.goldAmount; }

        public Player(Texture2D sprite, int hitPoints, int goldAmount, int score) : base(sprite,hitPoints) {

            this.goldAmount = goldAmount;
            this.score = score;
        
        }

    }
}
