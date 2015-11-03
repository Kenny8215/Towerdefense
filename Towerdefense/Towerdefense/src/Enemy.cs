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
        #region Fields
        Texture2D sprite;

        private int hitPoints;
        private Vector2 position;

        private int walkDistance;
        private int rotation;
        private float movementSpeed;
        private string resistance;

        private Boolean isBoss;
        private Boolean isFlying;
        #endregion

        public Enemy(Texture2D sprite, int hitPoints, Vector2 position, int walkDistance, int rotation, float movementSpeed, string resistance, Boolean isBoss, Boolean isFlying) : base(sprite, hitPoints) {

            this.sprite = sprite;
            this.hitPoints = hitPoints;
            this.position = position;
            this.walkDistance = walkDistance;
            this.rotation = rotation;
            this.movementSpeed = movementSpeed;
            this.resistance = resistance;
            this.isBoss = isBoss;
            this.isFlying = isFlying;
        }
    }
}
