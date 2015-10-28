using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefense
{
    class Enemy : DynamicGameObject
    {
        private int walkDistance;
        private float movementSpeed;
        private string resistance;

        private Boolean isBoss;
        private Boolean isFlying;

        public Enemy(Texture2D sprite, int hitPoints, int walkDistance, float movementSpeed, string resistance, Boolean isBoss, Boolean isFlying) : base(sprite, hitPoints) {

            this.walkDistance = walkDistance;
            this.movementSpeed = movementSpeed;
            this.resistance = resistance;
            this.isBoss = isBoss;
            this.isFlying = isFlying;
        }
    }
}
