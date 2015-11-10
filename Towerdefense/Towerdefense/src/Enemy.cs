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

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public int WalkDistance
        {
            get
            {
                return walkDistance;
            }

            set
            {
                walkDistance = value;
            }
        }

        public int Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
            }
        }

        public float MovementSpeed
        {
            get
            {
                return movementSpeed;
            }

            set
            {
                movementSpeed = value;
            }
        }

        public string Resistance
        {
            get
            {
                return resistance;
            }

            set
            {
                resistance = value;
            }
        }

        public bool IsBoss
        {
            get
            {
                return isBoss;
            }

            set
            {
                isBoss = value;
            }
        }

        public bool IsFlying
        {
            get
            {
                return isFlying;
            }

            set
            {
                isFlying = value;
            }
        }
        #endregion

        public Enemy():base()
        {

        }

        public Enemy(Texture2D sprite, int hitPoints, Vector2 position, int walkDistance, int rotation, float movementSpeed, string resistance, Boolean isBoss, Boolean isFlying) : base(sprite, hitPoints) {

            this.sprite = sprite;
            this.HitPoints = hitPoints;
            this.Position = position;
            this.WalkDistance = walkDistance;
            this.Rotation = rotation;
            this.MovementSpeed = movementSpeed;
            this.Resistance = resistance;
            this.IsBoss = isBoss;
            this.IsFlying = isFlying;
        }
    }
}
